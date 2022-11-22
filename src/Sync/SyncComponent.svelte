<script>
    import { BackendFactory } from "../lib/Backend";
    let backend;

    const promise = BackendFactory.create().then((b) => (backend = b));
</script>
{#await promise}
    Lade Daten...
{:then}
    <!--<button class="border-2 border-lightgrey rounded-sm ">Reset alle Änderungen</button>-->
{/await}