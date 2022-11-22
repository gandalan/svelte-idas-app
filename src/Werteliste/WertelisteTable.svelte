<script>
export let selectedWerteliste;
</script>
<table class="border-2 mt-4 border-collapse w-full table-fixed">
    <tr>
        <th class="text-left w-14">Reihenfolge</th>
        <th class="text-left w-10">Kürzel</th>
        <th class="text-left w-16">Beschreibung</th>
        <th class="text-left w-16">Belegblattext</th>
        <th class="text-left w-16">Angebotstext</th>
        <th class="text-left w-16">GültigAb</th>
        <th class="text-left w-16">GültigBis</th>
    </tr>

    {#each selectedWerteliste.items as item}
        <tr>
            <td class="text-left"><input class="w-full" bind:value={item.reihenfolge} />
            <td class="text-left"><input class="w-full" bind:value={item.kuerzel} >
            <td class="text-left"><input class="w-full" bind:value={item.beschreibung} />
            <td class="text-left"><input class="w-full" bind:value={item.belegBlattText} />                
            <td class="text-left"><input class="w-full" bind:value={item.angebotsText} />
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
    