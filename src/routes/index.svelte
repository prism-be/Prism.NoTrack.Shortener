<script context="module">
    export const prerender = true;
</script>

<script lang="ts">
    import { Input } from '@smui/textfield';
    import Paper from '@smui/paper';
    import Fab from '@smui/fab';
    import { Icon } from '@smui/common';
    import type { LongUrl } from '$lib/types';

    let value = '';

    function doShortener()
    {
        const longUrl: LongUrl = {
            url: value
        };

        fetch('/api/shortener', {
            method: "POST",
            body: JSON.stringify(longUrl),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        })
        .then((response) => {
            console.log(response);
        });
    }

    function handleKeyDown(event)
    {
        if (event.key === 'Enter')
        {
            doShortener();
        }
    }
</script>

<svelte:head>
    <title>Shorten your URL - NoTrack : For a private, add and tracking free web.</title>
</svelte:head>

<div class="solo-demo-container solo-container">
    <Paper class="solo-paper" elevation={6}>
        <Icon class="material-icons">link</Icon>
        <Input bind:value on:keydown={handleKeyDown} placeholder="Paste your long URL here" class="solo-input"/>
    </Paper>
    <Fab on:click={doShortener} disabled={value === ''} color="primary" mini class="solo-fab">
        <Icon class="material-icons">arrow_forward</Icon>
    </Fab>
</div>

<pre class="status">Value: {value}</pre>


<style>
    .solo-demo-container
    {
        padding: 36px 18px;
        background-color: #f8f8f8;
        border: 1px solid rgba(0, 0, 0, 0.1);
    }

    .solo-container
    {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    * :global(.solo-paper)
    {
        display: flex;
        align-items: center;
        flex-grow: 1;
        max-width: 600px;
        margin: 0 12px;
        padding: 0 12px;
        height: 48px;
    }

    * :global(.solo-paper > *)
    {
        display: inline-block;
        margin: 0 12px;
    }

    * :global(.solo-input)
    {
        flex-grow: 1;
    }

    * :global(.solo-input::placeholder)
    {
        opacity: 0.6;
    }

    * :global(.solo-fab)
    {
        flex-shrink: 0;
    }
</style>