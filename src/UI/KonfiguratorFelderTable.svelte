<script>
    export let selectedUIDef;
</script>

<table class="border-2 mt-4 border-collapse w-full table-fixed">
    <tr>
        <th class="text-left w-14">Reihenfolge</th>
        <th class="text-left w-10">Eingabelevel</th>
        <th class="text-left w-16">ProfilId</th>
        <th class="text-left w-16">GehoertZuProfilId</th>
        <th class="text-left">Caption</th>
        <th class="text-left">Tag</th>
        <th class="text-left">Kuerzel</th>
        <th class="text-left">WertelistenName</th>
        <th class="text-left">Vorgabewert</th>
        <th class="text-left">BelegblattText</th>
        <th class="text-left">AngebotsblattText</th>
        <th class="text-left">GueltigAb</th>
        <th class="text-left">GueltigBis</th>
    </tr>

    {#each selectedUIDef.konfiguratorFelder as item}
        <tr>
            <td class="text-left"><input class="w-full" bind:value={item.reihenfolge} />
            <td class="text-left"><input class="w-full" bind:value={item.eingabeLevel} />
            <td class="text-left"><input class="w-full" bind:value={item.profilId} />
            <td class="text-left"><input class="w-full" bind:value={item.gehoertZuProfilId} />
            <td class="text-left"><input class="w-full" bind:value={item.caption} />
            <td class="text-left"><input class="w-full" bind:value={item.tag} />                
            <td class="text-left"><input class="w-full" bind:value={item.kuerzel} />
            <td class="text-left"><input class="w-full" bind:value={item.werteListeName} />
            <td class="text-left"><input class="w-full" bind:value={item.vorgabewert} />
            <td class="text-left"><input class="w-full" bind:value={item.belegblattText} />
            <td class="text-left"><input class="w-full" bind:value={item.angebotsblattText} />
            <td class="text-left"><input class="w-full" bind:value={item.gueltigAb} />
            <td class="text-left"><input class="w-full" bind:value={item.gueltigBis} />
   
        </tr>
    {/each}
</table>

<style>
table, th, td {
  border: 1px solid lightgrey;
  overflow: hidden;
   white-space: nowrap;
}

</style>
