using Dapper;

namespace Movies.Application.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        /*
         * Don't limit things with CHAR - https://wiki.postgresql.org/wiki/Don%27t_Do_This#Don.27t_use_char.28n.29_even_for_fixed-length_identifiers
         */
        await connection.ExecuteAsync(
            @"
            create table if not exists currency (
                currencyCode TEXT primary key,
                name TEXT not null);
            "
        );

        await connection.ExecuteAsync(
            @"
            create table if not exists country (
                countryCode TEXT primary key,
                name TEXT not null);
            "
        );

        await connection.ExecuteAsync(
            @"
            create table if not exists currency_country (
                currencyCode TEXT not null,
                countryCode TEXT not null,
                primary key (currencyCode, countryCode),
                foreign key (currencyCode) references currency(currencyCode),
                foreign key (countryCode) references country(countryCode))
            "
        );

        await connection.ExecuteAsync(
            @"
            create table if not exists exchange_rate (
                rate decimal not null,
                validFor date not null,
                amount decimal not null,
                currencyCode TEXT not null,
                countryCode TEXT not null,
                primary key (currencyCode, countryCode, validFor),
                foreign key (currencyCode) references currency(currencyCode),
                foreign key (countryCode) references country(countryCode));
            "
        );

        await connection.ExecuteAsync(
            @"
            create index if not exists idx_exchange_rate_validFor ON exchange_rate(validFor);
            "
        );
    }
}
