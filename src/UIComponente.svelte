<script>
    import { BackendFactory } from "./lib/Backend";

    let selectedUIDef;
    let selectedUIDefName;
    let backend;

    $: {        
        selectedUIDef = "SP1/1";
        if(backend)
        {
        let test = backend.uidef.getAll();
        console.log(test);
    }
    }

    const promise = BackendFactory.create().then((b) =>
        backend =b
    
    ).then(b => backend.uidef.getAllNamen());

    function selectionChange(){

    }
</script>

{#await promise}
    Lade Daten...
{:then VariantenNamen}
<select
    bind:value={selectedUIDefName}
    class="border-2 w-auto"
>
    {#each VariantenNamen as v}
        <option>{v}</option>
    {/each}
</select>
{/await}
