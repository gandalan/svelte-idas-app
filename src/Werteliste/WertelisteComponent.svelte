<script>
    import { BackendFactory } from "../lib/Backend";
    import WertelisteTable from "./WertelisteTable.svelte";

    let selectedWerteliste;
    let selectedWertelistenName;
    let backend;

    $: {
        if (backend && selectedWertelistenName) setSelectedWerteliste();
    }

    async function setSelectedWerteliste() {
        selectedWerteliste = await backend.wertelisten.getByName(
            selectedWertelistenName
        );
        console.log(selectedWerteliste);
    }
    const promise = BackendFactory.create()
        .then((b) => (backend = b))
        .then((b) => backend.wertelisten.getAllNamen());
</script>

{#await promise}
    Lade Daten...
{:then WertelistenNamen}
    <select bind:value={selectedWertelistenName} class="border-2 w-auto">
        {#each WertelistenNamen as wl}
            <option>{wl}</option>
        {/each}
    </select>
    <br />
    <br />
    {#if selectedWerteliste}
        <WertelisteTable {selectedWerteliste} />
    {/if}
{/await}
