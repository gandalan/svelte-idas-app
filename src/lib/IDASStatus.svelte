<script>
    import { idasBackend } from "../stores";

    const servername = $idasBackend.settings.apiBaseurl;
    const promise = $idasBackend.mandanten.get($idasBackend.mandantGuid);
</script>

{#await promise}
    Lade
{:then a}
    <h1 class="text-lg font-bold">Mandant-Info</h1>

    <p>Server: {servername}</p>
    <p>Firma: {a.Name}</p>
    <p>Guid: {a.MandantGuid}</p>
    <p>Produzent: {a.IstProduzent} Händler: {a.IstHaendler}</p>
{/await}
