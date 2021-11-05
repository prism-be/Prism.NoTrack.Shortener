<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import { Input } from '@smui/textfield';
	import Paper, { Title, Content } from '@smui/paper';
	import Fab from '@smui/fab';
	import { Icon } from '@smui/common';
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
            })
		});
	}

	function handleKeyDown(event: any) {
		if (event.key === 'Enter') {
			doShortener();
		}
	}
</script>

<svelte:head>
	<title>Shorten your URL - NoTrack : For a private, add and tracking free web.</title>
</svelte:head>

<div class="solo-input-container solo-container">
	<Paper class="solo-paper" elevation={6}>
		<Icon class="material-icons">link</Icon>
		<Input
			bind:value
			on:keydown={handleKeyDown}
			placeholder="Paste your long URL here"
			class="solo-input"
		/>
	</Paper>
	<Fab on:click={doShortener} disabled={value === ''} color="primary" mini class="solo-fab">
		<Icon class="material-icons">arrow_forward</Icon>
	</Fab>
</div>

<div class="solo-result-container solo-container">
	<Paper class="solo-result-paper" elevation={6}>
		<Title>Here is your shortened url !</Title>
        <Content><a href={shortened} target="_blank">{shortened}</a></Content>
	</Paper>
</div>

<style>
	.solo-input-container {
		padding: 36px 18px;
		background-color: #f8f8f8;
		border: 1px solid rgba(0, 0, 0, 0.1);
	}

    .solo-result-container {
		margin-top: 25px;
        padding: 36px 18px;
		border: 1px solid rgba(0, 0, 0, 0.1);
	}

	.solo-container {
		display: flex;
		justify-content: center;
		align-items: center;
	}

	* :global(.solo-paper) {
		display: flex;
		align-items: center;
		flex-grow: 1;
		max-width: 600px;
		margin: 0 12px;
		padding: 0 12px;
		height: 48px;
	}

    * :global(.solo-result-paper) {
		max-width: 640px;
        width:100%;
        padding: 12px;
	}

	* :global(.solo-paper > *) {
		display: inline-block;
		margin: 0 12px;
	}

	* :global(.solo-input) {
		flex-grow: 1;
	}

	* :global(.solo-input::placeholder) {
		opacity: 0.6;
	}

	* :global(.solo-fab) {
		flex-shrink: 0;
	}
</style>
