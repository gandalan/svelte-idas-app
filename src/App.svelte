<script>
  import logo from './assets/svelte.png';
  import Counter from './lib/Counter.svelte';
  import IdasStatus from './lib/IDASStatus.svelte';
  import { Router, Route } from 'svelte-routing';
  import { idasBackend } from "./stores";
  import { IDASFactory } from '@gandalan/weblibs';

  export let settings;

  // You can create an IDAS Client:
  $idasBackend = IDASFactory(settings);
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router>
    <Route path="/counter" component={Counter} />
    <Route component={IdasStatus} />
  </Router>
</main>
