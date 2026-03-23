<script>
  import { idasBackend } from "../stores";

  const mandantGuid = /** @type {{ mandantGuid?: string } | undefined} */ ($idasBackend?.userInfo)?.mandantGuid;
  const idas = /** @type {import("@gandalan/weblibs").IDASFluentApi | null} */ ($idasBackend);
  const promise = mandantGuid
    ? /** @type {Promise<import("@gandalan/weblibs").MandantDTO>} */ (idas.get(`Mandanten/${mandantGuid}`))
    : Promise.reject(new Error("IDAS authentication is not configured."));
</script>

{#await promise}
  🔄 Lade Mandant-Daten...
{:then a}
  <h1>Mandant-Info</h1>
  <p>Server: {$idasBackend.baseUrl}</p>
  <p>Firma: {a.Name}</p>
  <p>Guid: {a.MandantGuid}</p>
  <p>Produzent: {a.IstProduzent} | Händler: {a.IstHaendler}</p>
{:catch error}
  <p>
    Fehler beim Laden der Mandanten-Infos:<br />
    {error.message}
  </p>
{/await}
