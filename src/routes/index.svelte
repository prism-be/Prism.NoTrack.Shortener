<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import { Input } from '@smui/textfield';
	import Paper, { Title, Subtitle, Content } from '@smui/paper';
	import Fab from '@smui/fab';
	import { Icon, Label } from '@smui/common';
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

{#if shortened !== ''}
	<div class="solo-result-container solo-container">
		<Paper class="solo-result-paper" elevation={6}>
			<Title>Here is your shortened url !</Title>
			<Content><a href={shortened} target="_blank">{shortened}</a></Content>
		</Paper>
	</div>
{/if}

<div class="solo-result-container solo-container">
	<Paper class="solo-result-paper" elevation={6}>
		<Title>The NoTrack project</Title>
		<Content>
			The No-Track project is a set of tools that can be used :
			<ul>
				<li>For free</li>
				<li>Without any ads</li>
				<li>Without any tracking</li>
			</ul>
			The only limitation is a rate limit to avoid spam.<br />
			I hope you will use it with respect of the work being done.

			<Subtitle class="paper-subtitle">What is the <u>Shortener</u> ?</Subtitle>
			It's a very simple tool that converts the long URL into a short one, with an id instead of the loooooooooong path, it looks better in documents and presentations.
			
			<Subtitle class="paper-subtitle">What is tracked ?</Subtitle>
			Short answer is nothing. The long answer is, of course there are some technical logs on the servers, but without any user informations.<br />
			Also, the number of use of each link is stored in database.<br />
			Nothing else :)<br />
			If you want to be sure of that, you can check by yourself on <a href="https://s.notrack.cloud/r/zeUL5Bears5ME7-NJl7aQ">this repository</a>.

			<Subtitle class="paper-subtitle">Free to use, but not to run</Subtitle>
			Of course, these tools are not free to run. I make these because I really like the
			<i>old web</i>, with no ad, no track and simple content.

			<p>If you want to support this project, you are welcome to :</p>
			<p />

			<p class="action-button">
				<Fab
					on:click={() => (window.location.href = 'https://buy.stripe.com/fZe29I2zJ1co9peaEE')}
					extended
				>
					<Icon class="material-icons">coffee</Icon>
					<Label class="action-button-fab">Buy me a coffee (2.5€)</Label>
				</Fab>
			</p>

			<p class="action-button">
				<Fab
					on:click={() => (window.location.href = 'https://buy.stripe.com/fZebKi7U38EQ9peeUV')}
					extended
				>
					<Icon class="material-icons">sports_bar</Icon>
					<Label class="action-button-fab">Buy me a beer (5.0€)</Label>
				</Fab>
			</p>

			<p class="action-button">
				<Fab
					on:click={() => (window.location.href = 'https://buy.stripe.com/14k7u23DNcV69pe28a')}
					extended
				>
					<Icon class="material-icons">liquor</Icon>
					<Label class="action-button-fab">Buy me a rhum (10.0€)</Label>
				</Fab>
			</p>

			<Subtitle class="paper-subtitle">Another way to support ?</Subtitle>
			Yes, of course ! This is an open source project, you can fork it and add the feature you want.
			This is stored in GitHub : <a href="https://s.notrack.cloud/r/zeUL5Bears5ME7-NJl7aQ">Prism.NoTrack.Shortener</a>.

			<Subtitle class="paper-subtitle">Bugs ? Support ?</Subtitle>
			This project is made on personal time, with the use of beta version of <a href="https://kit.svelte.dev/">SvelteKit</a>. there can be bugs, I can take time to fix them. <br />
			So, you use this tool as is, without any guarantee. You can always open an issue in the GitHub below.
		</Content>
	</Paper>
</div>

<style>
	.action-button {
		text-align: center;
	}

	* :global(.action-button-fab)
	{
		width: 220px;
	}

	.solo-input-container {
		padding: 36px 18px;
		background-color: #f8f8f8;
		border: 1px solid rgba(0, 0, 0, 0.1);
		max-width: 800px;
		margin: auto;
	}

	.solo-result-container {
		padding: 36px 18px;
		border: 1px solid rgba(0, 0, 0, 0.1);
		max-width: 800px;
		margin: 25px auto;
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
		width: 100%;
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

	* :global(.paper-subtitle) {
		padding-top: 12px;
		font-weight: bold;
	}
</style>
