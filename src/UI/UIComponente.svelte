<script>
    import { BackendFactory } from "../lib/Backend";
    import { Tabs, Tab, TabList, TabPanel } from "svelte-tabs";
    import EingabeFelderTable from "./EingabeFelderTable.svelte";

    let selectedUIDef;
    let selectedUIDefName;
    let backend;

    $: {
    }

    const promise = BackendFactory.create()
        .then((b) => (backend = b))
        .then((b) => backend.uidef.getAllNamen());

    async function selectionChange() {
        console.log("asdasd");
        if (backend) {
            selectedUIDef = await backend.uidef.getByName(selectedUIDefName);
        }
    }
</script>

{#await promise}
    Lade Daten...
{:then VariantenNamen}
    <select
        bind:value={selectedUIDefName}
        class="border-2 w-auto"
        on:change={selectionChange}
    >
        <option>SP1/2</option>
        {#each VariantenNamen as v}
            <option>{v}</option>
        {/each}
    </select>

    <Tabs>
        <TabList>
            <Tab>Eingabe-Felder</Tab>
            <Tab>Konfigurator-Felder</Tab>
        </TabList>

        <TabPanel>
            {#if selectedUIDef}
                <EingabeFelderTable {selectedUIDef} />
            {/if}
        </TabPanel>

        <TabPanel>asdasdasdasdas</TabPanel>
    </Tabs>
{/await}
