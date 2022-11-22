<script>
    import { BackendFactory } from "../lib/Backend";
    import KonfigSatzTable from "./KonfigSatzTable.svelte";

    let selectedVariante;
    let selectedVariantenName;
    let backend;

    $: {
        if (backend && selectedVariantenName) setSelectedVariante();
    }

    async function setSelectedVariante() {
        selectedVariante = await backend.varianten.getByName(
            selectedVariantenName
        );
    }

    const promise = BackendFactory.create()
        .then((b) => (backend = b))
        .then((b) => backend.varianten.getAllNamen());

    async function onclick() {
        await backend.idas.SyncAllWertelistenFromIDAS();
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

    {#if selectedVariante}
        <KonfigSatzTable selectedKonfigSatz={selectedVariante.konfigSatz} />
    {/if}
{/await}
