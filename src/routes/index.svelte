<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import About from '$lib/components/About.svelte';
import Shorten from '$lib/components/Shorten.svelte';

	import type { LongUrl, ShortUrl } from '$lib/types';

	let value = '';
	let shortened = '';

	function doShortener() {
		const longUrl: LongUrl = {
			url: value
		};

		fetch('/api/shortener', {
			method: 'POST',
			body: JSON.stringify(longUrl),
			headers: {
				'Content-type': 'application/json; charset=UTF-8'
			}
		}).then((response) => {
			response.json().then((shortUrl: ShortUrl) => {
				shortened = shortUrl.url;
			});
		});
	}

	function handleKeyDown(event: any) {
		if (event.key === 'Enter') {
			doShortener();
		}
	}
</script>

<svelte:head>
	<title>Shorten your URL - NoTrack : For a private, ad and tracking free web</title>
</svelte:head>

<Shorten />
<About />
