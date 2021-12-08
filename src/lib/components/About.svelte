<script>
	import BuyButton from './design/BuyButton.svelte';
	import SubTitle from './design/SubTitle.svelte';
	import Title from './design/Title.svelte';
</script>

<div class="container w-11/12 lg:w-6/12 mx-auto m-6 p-3 rounded border border-secondary">
	<Title>The NoTrack project</Title>
	The No-Track project is a set of tools that can be used :
	<ul class="list-disc list-inside pt-2 pb-2">
		<li>For free</li>
		<li>Without any ads</li>
		<li>Without any tracking</li>
	</ul>
	The only limitation is a rate limit to avoid spam.<br />
	I hope you will use it with respect of the work being done.

	<SubTitle>What is the Shortener ?</SubTitle>
	It's a very simple tool that converts the long URL into a short one, with an id instead of the loooooooooong path, it looks better in documents and presentations.

	<SubTitle>What is tracked ?</SubTitle>
	Short answer is nothing. The long answer is, of course there are some technical logs on the servers, but without any user informations.<br />
	Also, the number of use of each link is stored in database.<br />
	Nothing else :)<br />
	If you want to be sure of that, you can check by yourself on
	<a href="https://github.com/simonbaudart/Prism.NoTrack.Shortener">this repository</a>.

	<SubTitle>Free to use, but not to run</SubTitle>
	Of course, these tools are not free to run. I make these because I really like the "Old Fashion" web, where everything was free and without ads.

	<div class="pt-2">
		If you want to support this project, you are welcome to :

		<BuyButton href="https://buy.stripe.com/fZe29I2zJ1co9peaEE" icon="coffee">Buy me a coffee (2.5€)</BuyButton>
		<BuyButton href="https://buy.stripe.com/fZebKi7U38EQ9peeUV" icon="sports_bar">Buy me a beer (5.0€)</BuyButton>
		<BuyButton href="https://buy.stripe.com/14k7u23DNcV69pe28a" icon="liquor">Buy me a rhum (10.0€)</BuyButton>
	</div>

	<SubTitle>Another way to support ?</SubTitle>
	Yes, of course ! This is an open source project, you can fork it and add the feature you want. This is stored in GitHub :
	<a href="https://github.com/simonbaudart/Prism.NoTrack.Shortener">Prism.NoTrack.Shortener</a>.

	<SubTitle>Bugs ? Support ?</SubTitle>
	This project is made on personal time, with the use of beta version of
	<a href="https://kit.svelte.dev/">SvelteKit</a>. there can be bugs, I can take time to fix them.
	<br />
	So, you use this tool as is, without any guarantee. You can always open an issue in the GitHub below.
</div>