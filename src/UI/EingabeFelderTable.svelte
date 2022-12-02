<script>
    import { debug } from 'svelte/internal';
    import Modal from '../Modal.svelte';
    import { Selection, Backend } from '../stores';
    import CopyFelderDialog from './CopyFelderDialog.svelte';

    export let VariantenNamen;

    let localCheckBoxStore = [];

    let showCopyFelderToUIDef = false;

    function feldChanged(item, prop) {
        //check for tag and caption, bc can be multiple eingabefelder with the same tag and if u change tag
        var dbFeld = $Selection.DBUIDefinition?.EingabeFelder?.find((i) => i.Tag == item.Tag && i.Caption == item.Caption);
        item.isDirty = dbFeld == null || dbFeld[prop] != item[prop];
    }

    function setSelectedUIFeld(e, item) {
        if (e.target.checked) {
            $Selection.SelectedUIFelder.push(item);
            localCheckBoxStore.push(e.target);
        } else {
            var index = $Selection.SelectedUIFelder.indexOf(item);
            $Selection.SelectedUIFelder.splice(index, 1);
            index = localCheckBoxStore.indexOf(e);
            localCheckBoxStore.splice(index, 1);
        }
        $Selection = $Selection;
    }

    async function SetSelectedUIDefinition(uiDefinition) {
        $Selection.SelectedUIDefinition = uiDefinition;
        $Selection.DBUIDefinition = uiDefinition;
    }

    async function AddNewFeld() {
        var result = await $Backend.UIDefinition.EingabeFelder.Create($Selection.SelectedUIDefinition.UIDefinitionGuid);
        SetSelectedUIDefinition(result.data);
        console.log(result);
    }

    async function DeleteSelectedFelder() {
        if (confirm('Wollen Sie alle ausgewählten Felder endgültig löschen?')) {
            for (let feld of $Selection.SelectedUIFelder) {
                var result = await $Backend.UIDefinition.EingabeFelder.Delete(feld.UIEingabeFeldGuid);
                SetSelectedUIDefinition(result.data);
                $Selection.SelectedUIFelder = [];
                $Selection = $Selection;
                resetAllCheckbox();
            }
        } else {
            alert('Es wurde kein Feld ausgewählt.');
        }
    }

    function resetAllCheckbox() {
        for (var item of localCheckBoxStore) {
            item.checked = false;
        }
    }

    //delta-erkennung string |
</script>

{#if showCopyFelderToUIDef}
    <CopyFelderDialog {VariantenNamen} bind:showCopyFelderToUIDef />
{/if}

<button on:click={AddNewFeld}>Neues Feld</button>
<button on:click={() => (showCopyFelderToUIDef = true)} disabled={$Selection?.SelectedUIFelder.length < 1} class:disabled={$Selection?.SelectedUIFelder.length < 1}>Felder kopieren</button>
<button on:click={DeleteSelectedFelder} disabled={$Selection?.SelectedUIFelder.length < 1} class:disabled={$Selection?.SelectedUIFelder.length < 1} >Löschen</button>
<table class="border-2 mt-4 border-collapse">
    <tr>
        <th class="text-left w-100"><div style="resize: none" /></th>
        <th class="text-left w-2">
            <div title="Reihenfolge" style="width: 30px;">Reihenfolge</div>
        </th>
        <th class="text-left w-2"><div title="Eingabelevel" style="width: 20px;">Eingabelevel</div></th>
        <th class="text-left w-2"><div style="width: 20px;">IstKonfiguratorfeld</div></th>
        <th class="text-left w-2"><div style="width: 40px;">GruppenId</div></th>
        <th class="text-left w-2"><div style="width: 40px;">GehoertZuGruppeId</div></th>
        <th class="text-left w-2"><div style="width: 170px;">Caption</div></th>
        <th class="text-left w-2"><div style="width: 170px;">Tag</div></th>
        <th class="text-left w-2"><div style="width: 200px;">WertelistenName</div></th>
        <th class="text-left w-2"><div style="width: 60px;">Regel</div></th>
        <th class="text-left w-2"><div style="width: 80px;">Vorgabewert</div></th>
        <th class="text-left w-2"><div style="width: 150px;">BelegblattText</div></th>
        <th class="text-left w-2"><div style="width: 150px;">AngebotsblattText</div></th>
        <th class="text-left w-2"><div style="width: 40px;">MinWert</div></th>
        <th class="text-left w-2"><div style="width: 40px;">MaxWert</div></th>
        <th class="text-left w-2"><div style="width: 130px;">GueltigAb</div></th>
        <th class="text-left w-2"><div style="width: 130px;">GueltigBis</div></th>
        <th class="text-left w-2"><div style="width: 30px;">PreisfeldAnzeigen</div></th>
        <th class="text-left w-2"><div style="width: 30px;">MindestBreite</div></th>
        <th class="text-left w-2"><div style="width: 100px;">HilfeText</div></th>
        <th class="text-left w-2"><div style="width: 100px;">FehlerText</div></th>
    </tr>

    {#each $Selection?.SelectedUIDefinition?.EingabeFelder as item}
        <tr class:rowIsSelected={$Selection.SelectedUIFelder.find((i) => i.Tag == item.Tag)}>
            <td><input type="checkbox" on:change={(e) => setSelectedUIFeld(e, item)} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.Reihenfolge} on:change={() => feldChanged(item, 'Reihenfolge')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.EingabeLevel} on:change={() => feldChanged(item, 'EingabeLevel')} /></td>
            <td class="text-left"><input type="checkbox" class="w-full" bind:checked={item.IstKonfiguratorFeld} on:change={() => feldChanged(item, 'IstKonfiguratorFeld')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.GruppenId} on:change={() => feldChanged(item, 'GruppenId')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.GehoertZuGruppeId} on:change={() => feldChanged(item, 'GehoertZuGruppeId')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.Caption} on:change={() => feldChanged(item, 'Caption')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.Tag} on:change={() => feldChanged(item, 'Tag')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.WertelistenName} on:change={() => feldChanged(item, 'WertelistenName')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.Regel} on:change={() => feldChanged(item, 'Regel')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.Vorgabewert} on:change={() => feldChanged(item, 'Vorgabewert')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.BelegblattText} on:change={() => feldChanged(item, 'BelegblattText')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.AngebotsblattText} on:change={() => feldChanged(item, 'AngebotsblattText')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.MinWert} on:change={() => feldChanged(item, 'MinWert')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.MaxWert} on:change={() => feldChanged(item, 'MaxWert')} /></td>
            <td class="text-left"><input type="date" class="w-full" class:date={!item.GueltigAb} bind:value={item.GueltigAb} on:change={() => feldChanged(item, 'GueltigAb')} /></td>
            <td class="text-left"><input type="date" class="w-full" class:date={!item.GueltigBis} bind:value={item.GueltigBis} on:change={() => feldChanged(item, 'GueltigBis')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.PreisfeldAnzeigen} on:change={() => feldChanged(item, 'PreisfeldAnzeigen')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.MindestBreite} on:change={() => feldChanged(item, 'MindestBreite')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.HilfeText} on:change={() => feldChanged(item, 'HilfeText')} /></td>
            <td class="text-left"><input class="w-full" bind:value={item.FehlerText} on:change={() => feldChanged(item, 'FehlerText')} /></td>
        </tr>
    {/each}
</table>

<style>
    button {
        border: 2px solid lightgrey;
        padding: 3px;
    }

    .disabled {
        color: rgba(54, 54, 58, 0.5);
    }
    .isChanged {
        border-color: red;
    }

    .rowIsSelected {
        border: 2px solid rgba(0, 0, 255, 0.5);
    }
    .date {
        color: transparent;
    }

    table {
        border-collapse: collapse;
        border-spacing: 0px;
    }
    th {
        border: 2px solid black;
        padding: 0;
        margin: 0px;
        overflow: auto;
    }

    div {
        overflow: hidden;
        margin: 0px;
        padding: 0px;
        border: 1px solid black;
        display: block;
    }

    th > div:hover {
        resize: horizontal;
    }
    th > div:active {
        resize: horizontal;
    }

    th div {
        border: 0;
        width: auto;
        height: auto;
        min-height: 20px;
        min-width: 20px;
    }

    td {
        border: 1px solid lightgray;
    }
</style>
