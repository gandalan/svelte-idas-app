<script>
  import { href, router } from "./router.svelte.js";
</script>

<h1>Seite nicht gefunden</h1>
<p>Für <code>{router.path}</code> gibt es keine Seite.</p>
<p><a href={href("/")}>Zur Startseite</a></p>
