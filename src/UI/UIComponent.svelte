<script>
    // @ts-nocheck

    import { Tabs, Tab, TabList, TabPanel } from 'svelte-tabs';
    import EingabeFelderTable from './EingabeFelderTable.svelte';
    import KonfiguratorFelderTable from './KonfiguratorFelderTable.svelte';
    import { Backend, Selection } from '../stores';
    import Modal from '../Modal.svelte';

    let SelectedUIDefinitionName;
    let showCreateUIDefDialog = false;
    let createNewUI = true;

    let CreateNewUIByUIDefName;
    let NewUIDefName;
    let VariantenNamen;

    async function SetVariantenListe() {
        VariantenNamen = await $Backend.UIDefinition.GetAllNamen();
    }

    const promise = SetVariantenListe();

    $: {
        if ($Backend && SelectedUIDefinitionName) SetSelectedUIDefinition();
    }

    $: {
        if ($Selection?.SelectedUIDefinition?.EingabeFelder) {
            FormatDatesToString($Selection?.SelectedUIDefinition?.EingabeFelder);
        }
        if ($Selection?.SelectedUIDefinition?.KonfiguratorFelder) {
            FormatDatesToString($Selection?.SelectedUIDefinition?.KonfiguratorFelder);
        }
    }

    $: {
        if (SelectedUIDefinitionName) NewUIDefName = SelectedUIDefinitionName + '_Kopie';
        if (CreateNewUIByUIDefName) NewUIDefName = CreateNewUIByUIDefName + '_Kopie';
    }

    $: {
        if($Selection.SelectedUIDefinition)
        {
            UpdateSelectedUIDefinitionName();
            UpdateCreateNewUIByUIDefName();
        }
    }

    async function SetSelectedUIDefinition(uiDefinition) {
        if (uiDefinition) {
            $Selection.SelectedUIDefinition = uiDefinition;
            $Selection.DBUIDefinition = uiDefinition;
        } else {
            $Selection.SelectedUIDefinition = await $Backend.UIDefinition.GetByName(SelectedUIDefinitionName);
            $Selection.DBUIDefinition = await $Backend.UIDefinition.GetByName(SelectedUIDefinitionName);
        }
       
        
    }

    async function UpdateSelectedUIDefinitionName(){
        SelectedUIDefinitionName = $Selection?.SelectedUIDefinition?.BezeichnungKurz;
    }

    async function UpdateCreateNewUIByUIDefName(){
        CreateNewUIByUIDefName = $Selection?.SelectedUIDefinition?.BezeichnungKurz;
    }

    async function Save() {
        FormatDatesFromString($Selection?.SelectedUIDefinition?.EingabeFelder);
        FormatDatesFromString($Selection?.SelectedUIDefinition?.KonfiguratorFelder);
        var result = await $Backend.UIDefinition.Save($Selection.SelectedUIDefinition);
        SetSelectedUIDefinition(result.data);
    }

    function ShowUIDef() {
        console.log($Selection);
    }

    function ShowCreateNewUIDefDialog() {
        showCreateUIDefDialog = true;
        NewUIDefName = $Selection.SelectedUIDefinition.BezeichnungKurz + '_Kopie';
    }

    async function createNewUIDef() {
        if (NewUIDefName) {
            if (createNewUI) CreateNewUIByUIDefName = null;
            var result = await $Backend.UIDefinition.Create(NewUIDefName, CreateNewUIByUIDefName);
            await SetVariantenListe();
            await SetSelectedUIDefinition(result.data);
        }
        abortDialog();
    }

    async function abortDialog() {
        showCreateUIDefDialog = false;
        createNewUI = true;
    }



    function FormatDatesToString(list) {
        for (let feld of list) {
            if (feld.GueltigAb) feld.GueltigAb = Format(new Date(feld.GueltigAb));
            if (feld.GueltigBis) feld.GueltigBis = Format(new Date(feld.GueltigBis));
        }
    }

    function FormatDatesFromString(list) {
        for (let feld of list) {
            if (feld.GueltigAb) feld.GueltigAb = new Date(feld.GueltigAb);
            if (feld.GueltigBis) feld.GueltigBis = new Date(feld.GueltigBis);
        }
    }

    function Format(inputDate) {
        let date, month, year;

        date = inputDate.getDate();
        month = inputDate.getMonth() + 1;
        year = inputDate.getFullYear();

        date = date.toString().padStart(2, '0');

        month = month.toString().padStart(2, '0');

        return `${year}-${month}-${date}`;
    }
</script>

{#await promise}
    Lade Daten...
{:then}
    <Modal bind:isOpen={showCreateUIDefDialog} style="width: 500px; overflow: auto;">
        <b>Neue UI-Definition erstellen</b>
        <br />
        <br />
        <input type="text" class="border-2 w-auto" bind:value={NewUIDefName} />
        <br />
        <label>
            <input type="radio" bind:group={createNewUI} name="scoops" value={true} on:change={() => (CreateNewUIByUIDefName = null)} />

            Neue UI-Definition erstellen
        </label>
        <br />
        <label>
            <input type="radio" bind:group={createNewUI} name="scoops" value={false} />
            Aus vorhandener UI-Definition kopieren
        </label>

        {#if !createNewUI}
            <select bind:value={CreateNewUIByUIDefName} class="border-2 w-auto">
                {#each VariantenNamen as v}
                    <option>{v}</option>
                {/each}
            </select>
        {/if}

        <div class="flex mt-7 mr-2 justify-end">
            <button class="bg-gray-500 text-white p-2 pl-4 pr-4 rounded-full disabled:bg-slate-300 text-sm mr-2" on:click={abortDialog}>Abbrechen</button>
            <button class="bg-blue-600 text-white p-2 pl-4 pr-4 rounded-full disabled:bg-slate-300 text-sm" on:click={createNewUIDef}>Übernehmen</button>
        </div>
    </Modal>

    <select bind:value={SelectedUIDefinitionName} class="border-2 w-auto">
        {#each VariantenNamen as v}
            <option>{v}</option>
        {/each}
    </select>
    <button on:click={ShowUIDef}>Show both UIDef</button>
    <button on:click={ShowCreateNewUIDefDialog}>Neu erstellen</button>
    <button on:click={Save}>Speichern</button>
    <br />
    <br />

    <Tabs>
        <TabList>
            <Tab>Eingabe-Felder</Tab>
            <Tab>Konfigurator-Felder</Tab>
        </TabList>

        <TabPanel>
            {#if $Selection.SelectedUIDefinition}
                <EingabeFelderTable {VariantenNamen} />
            {/if}
        </TabPanel>

        <TabPanel
            >{#if $Selection.SelectedUIDefinition}
                <KonfiguratorFelderTable />
            {/if}</TabPanel
        >
    </Tabs>
{/await}

<style>
    button {
        border: 2px solid lightgrey;
        padding: 3px;
    }

    .hasError {
        border-color: red;
    }
</style>
