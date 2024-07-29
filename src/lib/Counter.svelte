<script>
  import { localBackend } from "../stores";

  const counterPromise = $localBackend.get("/counter");
</script>

{#await counterPromise}
  <progress />
{:then a}
  <p class="mb-12">Sub-API says: {JSON.stringify(a)}</p>
{:catch error}
  <p>
    Fehler beim Laden der Counter-Infos:<br />
    {error.message}
  </p>
{/await}
