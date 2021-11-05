import type { Response, Request } from '@sveltejs/kit';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import { getServerConfiguration } from '$lib/config';
import storage from 'azure-storage';

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

    const storageClient = storage.createTableService(serverConfiguration.cosmosDbConnectionString);
    storageClient.retrieveEntity('redirections', partition, slug, (error, result: any, response) => {
        if (!error) {
            console.log(result);
            result.Views = result.Views++;
            var longUrl = result.LongUrl.toString();
            console.log(longUrl);

            storageClient.replaceEntity('redirections', result, (error, result, response) => {
                if (!error) {
                    console.log(result);
                }
            });
        }
    });

console.log('plop');

    return {
        status: 200,
        headers: headers
    }
}