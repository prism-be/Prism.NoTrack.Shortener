<script context="module" lang="ts">
    export const prerender = true;
</script>

<script lang="ts">
    import About from '$lib/components/About.svelte';
    import Shorten from '$lib/components/Shorten.svelte';
</script>

<svelte:head>
    <title>Shorten your URL - NoTrack : For a private, ad and tracking free web</title>
</svelte:head>

<Shorten/>
<About/>
