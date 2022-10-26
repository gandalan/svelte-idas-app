<script>
  import logo from './assets/svelte.png';
  import Auth from './lib/Auth.svelte';
  import Counter from './lib/Counter.svelte';
  import IdasStatus from './lib/IDASStatus.svelte';
  import { Router, Route } from 'svelte-routing';
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  <Router>
    <Route path="/auth*" component={Auth} />
    <Route path="/counter" component={Counter} />
    <Route component={IdasStatus} />
  </Router>
</main>
