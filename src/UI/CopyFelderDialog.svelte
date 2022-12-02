<script>
    import { writable } from 'svelte/store';
    import Modal from '../Modal.svelte';
    import { Backend, Selection } from '../stores';
    export let VariantenNamen;
    export let showCopyFelderToUIDef;

    let localSelectedUIList = [];
    let localSelectedCheckBoxList = [];
    let localSelectedUIStore = writable([]);

    async function setSelectedUI(e, item) {
        if (e.target.checked) {
            localSelectedUIList.push(item);
            localSelectedCheckBoxList.push(e.target);
        } else {
            var index = localSelectedUIList.indexOf(item);
            localSelectedUIList.splice(index, 1);
            index = localSelectedCheckBoxList.indexOf(e);
            localSelectedCheckBoxList.splice(index, 1);
        }
        $localSelectedUIStore = localSelectedUIList;
    }

    async function copyFelder() {
        let success = true;
        for (let uiDefName of $localSelectedUIStore) {
            for (let uiFeld of $Selection?.SelectedUIFelder) {
                var result = await $Backend.UIDefinition.EingabeFelder.Copy(uiDefName, uiFeld.UIEingabeFeldGuid);
                if (!result) success = false;
            }
        }
        if (success) alert('Kopieren der Felder war erfolgreich');
    }
</script>

<Modal bind:isOpen={showCopyFelderToUIDef} style="width: 500px; height: 800px;">
    <b>Ausgewählte Felder kopieren</b>
    <br />
    <p>Bitte wählen Sie die UIs aus:</p>
    <br />
    <div style="height: 650px;" class="w-1/2 overflow-auto float-left">
        <table>
            {#each VariantenNamen as item}
                <tr>
                    <td class="w-5"><input type="checkbox" on:change={(e) => setSelectedUI(e, item)} /></td>
                    <td>{item}</td>
                </tr>
            {/each}
        </table>
    </div>
    <div style="height: 650px;" class="ml-4 w-1/3 overflow-auto float-left">
        <p class="mb-3">Ausgewählte Items</p>
        <table>
            {#each $localSelectedUIStore as item}
                <tr>
                    <td>{item}</td>
                </tr>
            {/each}
        </table>
    </div>
    <div class="border-gray-400 absolute right-0 bottom-0 m-5">
        <button class="border-2" on:click={() => (showCopyFelderToUIDef = false)}>Abbrechen</button>
        <button class="border-2" on:click={copyFelder}>Felder kopieren</button>
    </div>
</Modal>

<style>
    table {
        border-collapse: collapse;
        border-spacing: 0px;
    }
    td {
        border: 1px solid lightgray;
    }
</style>
