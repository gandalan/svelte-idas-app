<script>
    import { IDAS } from '@gandalan/weblibs';
    const idas = new IDAS();

    const servername = localStorage.getItem("IDAS_ApiBaseUrl");
    const promise = idas
        .authenticateWithJwt('/#/auth')
        .then(r => idas.mandanten.get(idas.mandantGuid));

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
