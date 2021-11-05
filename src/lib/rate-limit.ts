import type { RequestHeaders } from '@sveltejs/kit/types/helper';
import { RateLimiterMemory } from 'rate-limiter-flexible'

const opts = {
    points: 5,
    duration: 10,
};

export const rateLimiter = new RateLimiterMemory(opts);

export const getUserIp = (headers: RequestHeaders) => {
    return headers['x-forwarded-for'] ||
    headers['x-forwarded'] ||
    headers['forwarded'] ||
    headers['forwarded-for'] ||
    headers['x-client-ip'] ||
    headers['x-real-ip'] ||
    headers['cf-connecting-ip'] ||
    headers['fastly-client-ip'] ||
    headers['true-client-ip'] ||
    headers['x-cluster-client-ip'] ||
    'unknown';
}