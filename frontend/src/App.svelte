<script>
  import { Route, Router } from "svelte-routing";
  import logo from "./assets/svelte.png";
  import Counter from "./lib/Counter.svelte";
  import IdasStatus from "./lib/IDASStatus.svelte";
  import { idasBackend, localBackend } from "./stores";

  export let idas;
  export let local;

  $idasBackend = idas;
  $localBackend = local;
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router>
    <Route path="/" component={IdasStatus} />
    <Route path="/counter" component={Counter} />
  </Router>
</main>
