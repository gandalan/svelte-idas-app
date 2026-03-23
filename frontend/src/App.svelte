<script>
  import { Router } from "svelte-mini-router";
  import logo from "./assets/svelte.png";
  import { routerConf } from "./routerConf.js";
  import { idasBackend, localBackend } from "./stores";

  /**
   * @typedef {import("@gandalan/weblibs").IDASFluentApi} IDASFluentApi
   * @typedef {import("@gandalan/weblibs").FluentApi} FluentApi
   */
  
  /** @type({ idas: IDASFluentApi, api: FluentApi }) */
  let { idas, api } = $props();

  // Svelte 5: Use $effect for reactive assignments
  $effect(() => {
    $idasBackend = idas;
    $localBackend = api;
  });
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router {routerConf} />
</main>
