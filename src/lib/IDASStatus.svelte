<script>
  import { idasBackend } from "../stores";

  let mandantGuid = globalThis.idasTokens.userInfo.mandantGuid;
  const promise = $idasBackend.get("Mandanten/" + mandantGuid);
</script>

{#await promise}
  Lade
{:then a}
  <h1 class="text-lg font-bold">Mandant-Info</h1>
  <p>Server: {$idasBackend.baseUrl}</p>
  <p>Firma: {a.Name}</p>
  <p>Guid: {a.MandantGuid}</p>
  <p>Produzent: {a.IstProduzent} Händler: {a.IstHaendler}</p>
{:catch error}
  <p>
    Fehler beim Laden der Mandanten-Infos:<br />
    {error.message}
  </p>
{/await}
