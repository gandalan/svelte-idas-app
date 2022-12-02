<script>
     import { Backend, Selection } from "../stores";
    import KonfigSatzTable from "./KonfigSatzTable.svelte";

    let selectedVariantenName;

    $: {
        if ($Backend && selectedVariantenName) setSelectedVariante();
    }

    async function setSelectedVariante() {
        $Selection.SelectedVariante = await $Backend.varianten.getByName(
            selectedVariantenName
        );
    }

    const promise = $Backend.varianten.getAllNamen();

    async function onclick() {
        await $Backend.idas.SyncAllWertelistenFromIDAS();
        console.log("fertig");
    }
</script>

{#await promise}
    Lade Daten...
{:then VariantenNamen}
    <select bind:value={selectedVariantenName} class="border-2 w-auto">
        {#each VariantenNamen as v}
            <option>{v}</option>
        {/each}
    </select>
    <button on:click={onclick}>click</button>
    <br />
    <br />

    {#if $Selection.SelectedVariante}
        <KonfigSatzTable />
    {/if}
{/await}
