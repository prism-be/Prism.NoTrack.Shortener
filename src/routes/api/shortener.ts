import type { Response, Request } from '@sveltejs/kit';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import type { Redirection, ShortUrl } from '$lib/types';
import { nanoid } from 'nanoid';
import { getServerConfiguration, IServerConfiguration } from '$lib/config';
import { CosmosClient } from '@azure/cosmos';

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

    const cosmosClient = new CosmosClient(serverConfiguration.cosmosDbConnectionString)
    const database = cosmosClient.database('shortener');
    const container = database.container('redirections');
    
    const id = nanoid();
    const redirection: Redirection = {
        id,
        partition: id.substring(0,5),
        longUrl: data.url,
        views: 0
    };

    const result = await container.items.create(redirection);

    console.log(result);

    return {
        status: 200,
        body: JSON.stringify(getShortUrl(redirection, serverConfiguration)),
        headers: headers
    }
}

function getShortUrl(redirection: { id: string }, serverConfiguration: IServerConfiguration): ShortUrl {
    return {
        url: `${serverConfiguration.shortDomain}/r/${redirection.id}`
    };
}
