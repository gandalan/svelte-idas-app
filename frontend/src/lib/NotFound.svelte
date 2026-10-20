<script>
  import { followLink, href, router } from "./router.svelte.js";
</script>

<div class="card bg-base-100 border border-base-300">
  <div class="card-body">
    <h1 class="card-title text-2xl">Seite nicht gefunden</h1>
    <p>Für <code>{router.path}</code> gibt es keine Seite.</p>
    <div class="card-actions">
      <a class="btn btn-primary" href={href("/")} onclick={followLink}>Zur Startseite</a>
    </div>
  </div>
</div>
