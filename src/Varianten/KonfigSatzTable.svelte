<script>
export let selectedKonfigSatz;
</script>
<table class="border-2 mt-4 border-collapse w-full table-fixed">
    <tr>
        <th class="text-left w-14">UnterkomponenteName</th>
        <th class="text-left w-10">KonfigName</th>
        <th class="text-left w-16">Wert</th>
        <th class="text-left w-16">DatenTyp</th>
    </tr>

    {#each selectedKonfigSatz.eintraege as item}
        <tr>
            <td class="text-left"><input class="w-full" bind:value={item.unterkomponenteName} />
            <td class="text-left"><input class="w-full" bind:value={item.konfigName} >
            <td class="text-left"><input class="w-full" bind:value={item.wert} />
            <td class="text-left"><input class="w-full" bind:value={item.datenTyp} />
   
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
    