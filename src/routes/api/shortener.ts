import type { Response, Request } from '@sveltejs/kit';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import type { LongUrl, ShortUrl } from '$lib/types';
import { nanoid } from 'nanoid';
import storage from 'azure-storage';
import { getServerConfiguration, IServerConfiguration } from '$lib/config';

export const post = async (request: Request): Promise<Response> => {
    let headers: ResponseHeaders = {
        'Content-type': 'application/json; charset=UTF-8'
    };

    if (request.body === null) {
        return {
            status: 400,
            headers
        };
    }

    var data: any = request.body;

    if (!data.url.startsWith('https://') && !data.url.startsWith('http://')) {
        return {
            status: 400,
            headers
        };
    }

    var serverConfiguration = getServerConfiguration();

    const storageClient = storage.createTableService(serverConfiguration.cosmosDbConnectionString);

    var id = nanoid();
    var partition = id.substring(0, 5);
    var redirection = {
        PartitionKey: partition,
        RowKey: id,
        LongUrl: data.url,
        Views: 0
    };

    storageClient.insertEntity("redirections", redirection, (error, result, response) => {
        if (error) {
            console.log(error);
            return;
        }
    });

    return {
        status: 200,
        body: JSON.stringify(getShortUrl(redirection, serverConfiguration)),
        headers: headers
    }
}

function getShortUrl(redirection: { PartitionKey: string; RowKey: string; LongUrl: any; }, serverConfiguration: IServerConfiguration): ShortUrl {
    return {
        url: `${serverConfiguration.shortDomain}/r/${redirection.RowKey}`
    };
}
