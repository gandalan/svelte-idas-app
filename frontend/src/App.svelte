<script>
  import { Router } from "svelte-mini-router";
  import logo from "./assets/svelte.png";
  import { routerConf } from "./routerConf.js";
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router {routerConf} />
</main>
