import dotenv from 'dotenv';
dotenv.config();

export interface IServerConfiguration {
    cosmosDbConnectionString: string;
    shortDomain: string;
}

export function getServerConfiguration(): IServerConfiguration {
    var cosmosDbConnectionString = process.env['COSMOSDB']?.toString();

    if (cosmosDbConnectionString == null || cosmosDbConnectionString === '') {
        throw new Error("The environment variable 'COSMOSDB' has not been set.");
    }

    var shortDomain = process.env['SHORT_DOMAIN']?.toString();

    if (shortDomain == null || shortDomain === '') {
        throw new Error("The environment variable 'SHORT_DOMAIN' has not been set.");
    }

    return {
        cosmosDbConnectionString,
        shortDomain
    };
}