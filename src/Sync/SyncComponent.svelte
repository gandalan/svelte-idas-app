<script>
    import { Backend } from "../stores";



    async function syncVariantenFromIDAS() {
        await $Backend.idas.SyncAllWertelistenFromIDAS();
        console.log("fertig");
    }

    async function syncWertelistenFromIDAS() {
        await $Backend.idas.SyncAllWertelistenFromIDAS();
        console.log("fertig");
    }


</script>
    <button class="border-2 border-lightgrey rounded-sm" on:click={syncVariantenFromIDAS}>Sync All Varianten from IDAS</button>
    <button class="border-2 border-lightgrey rounded-sm" on:click={syncWertelistenFromIDAS}>Sync Wertelisten from IDAS</button>
