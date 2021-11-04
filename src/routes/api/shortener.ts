import type { Response, Request } from '@sveltejs/kit';
import type { LongUrl, ShortUrl } from '$lib/types';
import type { ResponseHeaders } from '@sveltejs/kit/types/helper';
import { nanoid } from 'nanoid';

export const post = async (request: Request) : Promise<Response> => {
    console.log(request.body);

    var json = JSON.stringify( {
        "hello": nanoid(16)
    });

    let headers: ResponseHeaders = {

    }

    return {
        status: 200,
        body: json,
        headers: headers
    }
}

export const get = async (request: Request) : Promise<Response> => {
    console.log(request.body);

    var json = JSON.stringify( {
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