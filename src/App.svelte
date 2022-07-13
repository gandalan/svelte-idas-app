<script>
  import logo from './assets/svelte.png';
  import IdasStatus from './lib/IDASStatus.svelte';

  const awaiter = fetch('/api/counter')
    .then(res => res.json())
    .then(data => {
      console.log(data)
      return data
    })
    .catch(err => {
      console.log(err)
      return err
    });
</script>

<main>
  <img src={logo} alt="Svelte Logo" class="w-16" />

  {#await awaiter}
    <progress />
  {:then a} 
    <p class="mb-12">Sub-API says: {JSON.stringify(a)}</p>
  {/await}

  <IdasStatus />
</main>
