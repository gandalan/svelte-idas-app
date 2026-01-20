<script>
  import { Router } from "svelte-mini-router";
  import logo from "./assets/svelte.png";
  import { routerConf } from "./routerConf.js";
  import { idasBackend, localBackend } from "./stores";

  // Svelte 5: export let → $props()
  let { idas, local } = $props();

  // Svelte 5: Use $effect for reactive assignments
  $effect(() => {
    $idasBackend = idas;
    $localBackend = local;
  });
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router {routerConf} />
</main>
