import * as React from 'react';
import { AppProps } from 'next/app';
import Head from 'next/head';

import '../styles/globals.css';

export default function MyApp({Component, pageProps}: AppProps): JSX.Element
{
    return (
        <>
            <Head>
                <title>Shorten your URL - NoTrack : For a private, ad and tracking free web</title>
                <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                <link rel="icon" type="image/png" href="/favicon.png" />
            </Head>
            <Component {...pageProps} />
        </>
    );
}