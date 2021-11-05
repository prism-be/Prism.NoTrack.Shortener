
import type { Handle } from '@sveltejs/kit';
import { getUserIp, rateLimiter } from '$lib/rate-limit';

export const handle: Handle = async ({ request, resolve }) => {

    const userIp = getUserIp(request.headers);

    const response = await rateLimiter.consume(userIp, 1).then(async () => {
        return await resolve(request);
    })
    .catch((rateLimit) => {
        return {
            status: 429,
            headers: {
                'X-RateLimit-remainingPoints' : rateLimit.remainingPoints,
                'X-RateLimit-msBeforeNext' : rateLimit.msBeforeNext,
                'X-RateLimit-consumedPoints' : rateLimit.consumedPoints,
                'X-RateLimit-isFirstInDuration' : rateLimit.isFirstInDuration,
                'X-RateLimit-userIp': userIp
            }
        }
    })
    

    return {
        ...response,
        headers: {
            ...response.headers,
        }
    };
}