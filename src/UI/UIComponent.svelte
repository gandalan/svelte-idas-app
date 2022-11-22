<script>
    // @ts-nocheck

    import { BackendFactory } from "../lib/Backend";
    import { Tabs, Tab, TabList, TabPanel } from "svelte-tabs";
    import EingabeFelderTable from "./EingabeFelderTable.svelte";
    import KonfiguratorFelderTable from "./KonfiguratorFelderTable.svelte";

    let selectedUIDef;
    let dbUIDef;
    let selectedUIDefName;
    let backend;

    const promise = BackendFactory.create()
        .then((b) => (backend = b))
        .then((b) => backend.uidef.getAllNamen());

    $: {
        if (backend && selectedUIDefName) setSelectedUIDef();
    }

    async function setSelectedUIDef() {
        selectedUIDef = await backend.uidef.getByName(selectedUIDefName);
        dbUIDef = await backend.uidef.getByName(selectedUIDefName);
    }

    async function save() {
        console.log(selectedUIDef.uiDefinitionGuid);
        var result = backend.uidef.save(selectedUIDef.uiDefinitionGuid, selectedUIDef);
        selectedUIDef = result;
        console.log(result);
    }

    function showUIDef() {
        console.log(selectedUIDef);
        console.log(dbUIDef);
    }
</script>

{#await promise}
    Lade Daten...
{:then VariantenNamen}
    <select bind:value={selectedUIDefName} class="border-2 w-auto">
        {#each VariantenNamen as v}
            <option>{v}</option>
        {/each}
    </select>
    <button on:click={showUIDef}>Show both UIDef</button>
    <button on:click={save}>Speichern</button>
    <br />
    <br />

    <Tabs>
        <TabList>
            <Tab>Eingabe-Felder</Tab>
            <Tab>Konfigurator-Felder</Tab>
        </TabList>

        <TabPanel>
            {#if selectedUIDef}
                <EingabeFelderTable {selectedUIDef} {dbUIDef} />
            {/if}
        </TabPanel>

        <TabPanel
            >{#if selectedUIDef}
                <KonfiguratorFelderTable {selectedUIDef} {dbUIDef} />
            {/if}</TabPanel
        >
    </Tabs>
{/await}
