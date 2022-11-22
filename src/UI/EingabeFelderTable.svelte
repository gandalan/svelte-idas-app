<script>
    export let selectedUIDef;
    export let dbUIDef;

    
function feldChanged(item, prop)
{
    var dbFeld = dbUIDef?.eingabeFelder?.find(i => i.tag == item.tag);
    item.isDirty = dbFeld[prop] != item[prop];
}

</script>

<table class="border-2 mt-4 border-collapse w-full table-fixed">
    <tr>
        <th class="text-left w-14">Reihenfolge</th>
        <th class="text-left w-10">Eingabelevel</th>
        <th class="text-left w-10">IstKonfiguratorfeld</th>
        <th class="text-left w-16">GruppenId</th>
        <th class="text-left w-16">GehoertZuGruppeId</th>
        <th class="text-left">Caption</th>
        <th class="text-left">Tag</th>
        <th class="text-left">WertelistenName</th>
        <th class="text-left w-10">Regel</th>
        <th class="text-left">Vorgabewert</th>
        <th class="text-left">BelegblattText</th>
        <th class="text-left">AngebotsblattText</th>
        <th class="text-left">MinWert</th>
        <th class="text-left">MaxWert</th>
        <th class="text-left">GueltigAb</th>
        <th class="text-left">GueltigBis</th>
        <th class="text-left">PreisfeldAnzeigen</th>
        <th class="text-left">MindestBreite</th>
        <th class="text-left">HilfeText</th>
        <th class="text-left">FehlerText</th>
    </tr>

    {#each selectedUIDef.eingabeFelder as item}
        <tr>
            <td class="text-left" class:isChanged={dbUIDef?.eingabeFelder?.find(i => i.tag == item.tag)?.reihenfolge != item.reihenfolge}><input class="w-full" bind:value={item.reihenfolge} on:change={() => feldChanged(item, "reihenfolge")}/>
            <td class="text-left"><input class="w-full" bind:value={item.eingabeLevel} />
            <td class="text-left"><input type="checkbox" class="w-full" bind:checked={item.istKonfiguratorFeld} />
            <td class="text-left"><input class="w-full" bind:value={item.gruppenId} />
            <td class="text-left"><input class="w-full" bind:value={item.gehoertZuGruppeId} />
            <td class="text-left"><input class="w-full" bind:value={item.caption} />
            <td class="text-left"><input class="w-full" bind:value={item.tag} />
            <td class="text-left"><input class="w-full" bind:value={item.wertelistenName} />
            <td class="text-left"><input class="w-full" bind:value={item.regel} />
            <td class="text-left"><input class="w-full" bind:value={item.vorgabewert} />
            <td class="text-left"><input class="w-full" bind:value={item.belegblattText} />
            <td class="text-left"><input class="w-full" bind:value={item.angebotsblattText} />
            <td class="text-left"><input class="w-full" bind:value={item.minWert} />
            <td class="text-left"><input class="w-full" bind:value={item.maxWert} />
            <td class="text-left"><input class="w-full" bind:value={item.gueltigAb} />
            <td class="text-left"><input class="w-full" bind:value={item.gueltigBis} />
            <td class="text-left"><input class="w-full" bind:value={item.preisfeldAnzeigen} />
            <td class="text-left"><input class="w-full" bind:value={item.mindestBreite} />
            <td class="text-left"><input class="w-full" bind:value={item.hilfeText} />
            <td class="text-left"><input class="w-full" bind:value={item.fehlerText} />

   
        </tr>
    {/each}
</table>

<style>
table, th, td {
  border: 1px solid lightgrey;
  overflow: hidden;
   white-space: nowrap;
}

.isChanged{
    border-color: red;
}

</style>
