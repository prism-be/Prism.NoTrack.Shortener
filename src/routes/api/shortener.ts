import type { Response, Request } from '@sveltejs/kit';
import type { LongUrl, ShortUrl } from '$lib/types';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import { nanoid } from 'nanoid';
import storage from 'azure-storage';

import dotenv from 'dotenv';

dotenv.config();

export const post = async (request: Request): Promise<Response> => {

    let headers: ResponseHeaders = {

    }

    if (request.body === null) {
        return {
            status: 400,
            headers
        };
    }

    var data: any = request.body;

    if (!data.url.startsWith('https://' || !data.url.startsWith('http://')))
    {
        return {
            status: 400,
            headers
        };
    }

    const connectionString = process.env['COSMOSDB']?.toString();

    if (connectionString == null)
    {
        return {
            status: 500,
            body: 'The CosmosDB connection string is not found',
            headers
        };;
    }

    const storageClient = storage.createTableService(connectionString);

    var id = nanoid();
    var partition = id.substring(0, 5);
    var redirection = {
        PartitionKey: partition,
        RowKey: id,
        LongUrl: data.url,
        Views: 0
    };

    storageClient.insertEntity("redirections", redirection, function (error, result, response) {
        if (error) {
            console.log(error);
            return;
        }

        console.log("   insertOrMergeEntity succeeded.");
    });

    return {
        status: 200,
        body: JSON.stringify(getShortUrl(redirection)),
        headers: headers
    }
}

export const get = async (request: Request): Promise<Response> => {
    console.log(request.body);

    var json = JSON.stringify({
        "hello": "world"
    });

    let headers: ResponseHeaders = {

    }

    return {
        status: 200,
        body: json,
        headers: headers
    }
}

function getShortUrl(redirection: { PartitionKey: string; RowKey: string; LongUrl: any; }): ShortUrl {
    return {
        url: redirection.RowKey
    };
}
