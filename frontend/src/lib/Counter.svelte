<script>
  import { localBackend } from "../stores";

  const counterPromise = $localBackend.get("/counter");
</script>

<div class="card bg-base-100 border border-base-300">
  <div class="card-body">
    <h1 class="card-title text-2xl">Counter</h1>
    {#await counterPromise}
      <span class="loading loading-spinner" role="status" aria-label="Lade Counter"></span>
    {:then a}
      <p>Sub-API says: <code>{JSON.stringify(a)}</code></p>
    {:catch error}
      <div role="alert" class="alert alert-error alert-soft">Fehler beim Laden der Counter-Infos: {error.message}</div>
    {/await}
  </div>
</div>
