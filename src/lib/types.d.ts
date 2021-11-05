export interface LongUrl {
    url: string;
}

export interface ShortUrl {
    url: string;
}

export interface Redirection {
    id: string;
    partition: string;
    longUrl: string;
    views: number;
}