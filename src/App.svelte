<script>
  import logo from './assets/svelte.png';
  import Auth from './lib/Auth.svelte';
  import Counter from './lib/Counter.svelte';
  import IdasStatus from './lib/IDASStatus.svelte';
  import Router, { link } from 'svelte-spa-router'

  const routes = {
    // Home
    '/': IdasStatus,
    // Auth
    '/auth*': Auth,
    // Counter test
    '/counter': Counter,
  }
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router {routes} />
</main>
