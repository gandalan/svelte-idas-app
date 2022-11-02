<script>
    import { IDASFactory } from '@gandalan/weblibs';

    const servername = localStorage.getItem("IDAS_ApiBaseUrl");
    const promise = IDASFactory.create()
    .then(idas => {
        return idas.mandanten.get(idas.mandantGuid);
    });
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
