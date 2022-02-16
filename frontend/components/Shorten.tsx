import { FormEvent, useState } from 'react';
import { LongUrl, ShortUrl } from '../services/types';
import { schemaLongUrl } from '../services/validations';

export function Shorten()
{
    const [longUrl, setLongUrl] = useState('');
    const [shortUrl, setShortUrl] = useState('');
    const [validUrl, setValidUrl] = useState(true);

    async function doShortener(e: FormEvent)
    {
        e.preventDefault();

        setShortUrl('');

        const data: LongUrl = {
            url: longUrl,
        };

        const valid = await schemaLongUrl.isValid(data);

        setValidUrl(valid);

        if (valid)
        {
            fetch('/api/shorten', {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                },
            }).then((response) =>
            {
                response.json().then((response: ShortUrl) =>
                {
                    setShortUrl(response.url);
                });
            });
        }
    }

    return <div className="container w-11/12 lg:w-8/12 mx-auto m-6 p-3 rounded border border-secondary bg-gray-100">
        <div className="w-8/12 mx-auto">
            <form action="" className="flex justify-center bg-white rounded border border-primary overflow-hidden" onSubmit={(e) => doShortener(e)}>
                <input type="search" placeholder="Paste your long URL here" className="block rounded-md border-0 focus:outline-none focus:ring-0 focus:border-blue-500 flex-grow p-2" value={longUrl} onChange={e => setLongUrl(e.currentTarget.value)}/>
                <button type="submit">
                    <span className="material-icons text-primary pr-2 align-middle">arrow_forward</span>
                </button>
            </form>
            {validUrl || <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mt-5 mb-2 p-5" role="alert">
                You url seems to be invalid.<br />
                The url must starts with http:// or https:// and be no more long than 80000 chars.
            </div>}
        </div>

        {shortUrl !== '' &&
            <div className="w-8/12 mx-auto border rounded bg-white mt-5 mb-2 p-5">
                <h2 className="text-2xl pb-5">Here is your shortened url!</h2>
                <div>
                    <a href={shortUrl} className="shortened-url" target="_blank" rel="noreferrer">{shortUrl}</a>
                </div>
            </div>
        }
    </div>;
}