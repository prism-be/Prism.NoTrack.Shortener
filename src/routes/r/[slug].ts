import type { Response, Request } from '@sveltejs/kit';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import { getServerConfiguration } from '$lib/config';
import { CosmosClient } from '@azure/cosmos';
import type { Redirection } from '$lib/types';

export const get = async (request: Request): Promise<Response> => {
    const { slug } = request.params;

    let headers: ResponseHeaders = {
    };

    if (slug == null || slug === '') {
        return {
            status: 400,
            headers: headers
        }
    }

    var partition = slug.substring(0, 5);

    var serverConfiguration = getServerConfiguration();

    const cosmosClient = new CosmosClient(serverConfiguration.cosmosDbConnectionString)
    const database = cosmosClient.database('shortener');
    const container = database.container('redirections');
    var item = container.item(slug, partition);
    var redirection = await item.read();

    if (redirection.statusCode == 404)
    {
        return {
            status: 404,
            headers: headers
        }
    }

    var redirectionContent : Redirection = {
        id: redirection.resource.id,
        longUrl: redirection.resource.longUrl,
        partition: redirection.resource.partition,
        views: ++redirection.resource.views
    };

    await item.replace(redirectionContent);

    headers = {
        'Location' : redirectionContent.longUrl
    };

    return {
        status: 302,
        headers: headers
    }
}