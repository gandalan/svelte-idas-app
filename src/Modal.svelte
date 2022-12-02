<script>
    import {fade} from 'svelte/transition';
    import { createEventDispatcher } from "svelte";

    const dispatch = createEventDispatcher();
    export let isOpen = true;
</script>

{#if isOpen}
<div class="fixed top-0 left-0 w-screen h-screen z-50" transition:fade={{ duration: 200 }}>
    <div class="fixed top-0 left-0 w-screen h-screen bg-black opacity-50" on:click={() => { isOpen = false; dispatch("closed"); } }></div>
    <div {...$$restProps} class="fixed top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-white border-2 border-white rounded-xl w-5/6 max-h-[75%] overflow-hidden p-2">
        <slot />
    </div>
</div>
{/if}
    