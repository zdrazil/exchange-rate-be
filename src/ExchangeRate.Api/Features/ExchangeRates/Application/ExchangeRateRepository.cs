using Dapper;
using ExchangeRate.Api.Features.ExchangeRateModels;
using Movies.Application.Database;

namespace ExchangeRate.Api.Features.ExchangeRateApplication.Repository;

public interface IExchangeRateRepository
{
    Task<bool> CreateAsync(ExchangeRateDto exchangeRate, CancellationToken token = default);

    Task<IEnumerable<ExchangeRateDto>> GetAllAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );

    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ExchangeRateRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(
        ExchangeRateDto exchangeRate,
        CancellationToken token = default
    )
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        // TODO: Find a solution that allows bulk insert.
        using var transaction = connection.BeginTransaction();

        await connection.ExecuteAsync(
            new CommandDefinition(
                @"
                insert into currency
                (currencyCode, name)
                values
                (@CurrencyCode, @name)
                on conflict (currencyCode) do nothing
                ",
                new { CurrencyCode = exchangeRate.Code, Name = exchangeRate.Currency },
                transaction: transaction,
                cancellationToken: token
            )
        );

        await connection.ExecuteAsync(
            new CommandDefinition(
                @"
                insert into country
                (countryCode, name)
                values
                (@CountryCode, @name)
                on conflict (countryCode) do nothing
                ",
                new { CountryCode = exchangeRate.Country, Name = exchangeRate.Country },
                transaction: transaction,
                cancellationToken: token
            )
        );

        await connection.ExecuteAsync(
            new CommandDefinition(
                @"
                insert into currency_country
                (currencyCode, countryCode)
                values
                (@CurrencyCode, @CountryCode)
                on conflict (currencyCode, countryCode) do nothing
                ",
                new { CurrencyCode = exchangeRate.Code, CountryCode = exchangeRate.Country },
                transaction: transaction,
                cancellationToken: token
            )
        );

        var validFor = DateTime.UtcNow.Date;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(
                @"
                insert into exchange_rate
                (currencyCode, countryCode, rate, validFor, amount)
                values
                (@CurrencyCode, @CountryCode, @Rate, @ValidFor, @Amount)
                on conflict (currencyCode, countryCode, validFor) do update
                set rate = excluded.rate,
                    amount = excluded.amount
                ",
                new
                {
                    CurrencyCode = exchangeRate.Code,
                    CountryCode = exchangeRate.Country,
                    exchangeRate.Rate,
                    ValidFor = validFor.ToString("yyyy-MM-dd"),
                    Amount = exchangeRate.Quantity
                },
                transaction: transaction,
                cancellationToken: token
            )
        );

        transaction.Commit();

        return result > 0;
    }

    public Task<int> GetCountAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public async Task<IEnumerable<ExchangeRateDto>> GetAllAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    )
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var def = new CommandDefinition(
            @"
                    select
                        exchange_rate.currencyCode,
                        exchange_rate.countryCode,
                        exchange_rate.rate,
                        exchange_rate.validFor,
                        exchange_rate.amount,
                        currency.name as currencyName,
                        country.name as countryName
                    from exchange_rate
                    join currency on exchange_rate.currencyCode = currency.currencyCode
                    join country on exchange_rate.countryCode = country.countryCode
                    ",
            new { options.Date },
            cancellationToken: cancellationToken
        );

        var result = await connection.QueryAsync(
            new CommandDefinition(
                @"
                    select
                        exchange_rate.currencyCode,
                        exchange_rate.countryCode,
                        exchange_rate.rate,
                        exchange_rate.validFor,
                        exchange_rate.amount,
                        currency.name as currencyName,
                        country.name as countryName
                    from exchange_rate
                    join currency on exchange_rate.currencyCode = currency.currencyCode
                    join country on exchange_rate.countryCode = country.countryCode
                    where
                        validFor = @Date
                    ",
                new { options.Date },
                cancellationToken: cancellationToken
            )
        );

        return result.Select(r => new ExchangeRateDto
        {
            Code = r.currencyCode,
            Country = r.countryName,
            Currency = r.currencyName,
            Rate = r.rate,
            Quantity = r.amount
        });
    }
}
