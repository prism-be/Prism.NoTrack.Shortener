import dotenv from 'dotenv';
dotenv.config();

export interface IServerConfiguration {
    cosmosDbConnectionString: string;
    shortDomain: string;
}

export function getServerConfiguration(): IServerConfiguration {
    return {
        cosmosDbConnectionString: getEnv('COSMOSDB_CONNECTIONSTRING'),
        shortDomain: getEnv('SHORT_DOMAIN'),
    };
}

function getEnv(envKey: string): string {
    const value = process.env[envKey]?.toString();

    if (value == null || value === '') {
        throw new Error(`The environment variable '${envKey}' has not been set.`);
    }

    return value;
}
