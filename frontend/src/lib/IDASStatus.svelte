<script>
  import { idasBackend } from "../stores";

  const mandantGuid = /** @type {{ mandantGuid?: string } | undefined} */ ($idasBackend?.userInfo)?.mandantGuid;
  const idas = /** @type {import("@gandalan/weblibs").IDASFluentApi | null} */ ($idasBackend);
  const promise = mandantGuid && idas
    ? /** @type {Promise<import("@gandalan/weblibs").MandantDTO>} */ (idas.get(`Mandanten/${mandantGuid}`))
    : Promise.reject(new Error("IDAS authentication is not configured."));
</script>

<div class="card bg-base-100 border border-base-300">
  <div class="card-body">
    <h1 class="card-title text-2xl">Mandant-Info</h1>
    {#await promise}
      <span class="loading loading-spinner" role="status" aria-label="Lade Mandant-Daten"></span>
    {:then a}
      <dl class="grid grid-cols-[auto_1fr] gap-x-6 gap-y-1">
        <dt class="opacity-70">Server</dt><dd class="selectable">{$idasBackend?.baseUrl}</dd>
        <dt class="opacity-70">Firma</dt><dd class="selectable">{a.Name}</dd>
        <dt class="opacity-70">Guid</dt><dd class="selectable font-mono">{a.MandantGuid}</dd>
        <dt class="opacity-70">Produzent</dt><dd>{a.IstProduzent ? "Ja" : "Nein"}</dd>
        <dt class="opacity-70">Händler</dt><dd>{a.IstHaendler ? "Ja" : "Nein"}</dd>
      </dl>
    {:catch error}
      <div role="alert" class="alert alert-error alert-soft">Fehler beim Laden der Mandanten-Infos: {error.message}</div>
    {/await}
  </div>
</div>
