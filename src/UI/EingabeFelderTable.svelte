<script>
    export let selectedUIDef;
</script>

<table class="border-2 mt-4 border-collapse w-full">
    <tr>
        <th class="text-left">Reihenfolge</th>
        <th class="text-left">Caption</th>
        <th class="text-left">Tag</th>
        <th class="text-left w-auto">Vorgabewert</th>
    </tr>
    
        {#each selectedUIDef.eingabeFelder as item}
            <tr>
                <td class="text-left"><input class="w-full" bind:value={item.caption}/></td>
                <th class="text-left"><input class="w-full" bind:value={item.tag}/></th>
                <th class="text-left w-auto"><input class="w-full" bind:value={item.vorgabeWert}/></th>
            </tr>
        {/each}

</table>
