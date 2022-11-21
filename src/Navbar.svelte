<script>
  import { area } from './stores';
  import { navigate } from 'svelte-routing';

  if (window.location.pathname === "/ui")
    $area = 1;
  if (window.location.pathname === "/auftraege")
    $area = 2;

  function go(areaId, url)
  {
    $area = areaId;
    navigate(url);
  }
</script>

<div class="flex flex-col">
    <!--<button class="m-2 p-1 text-left" class:active={$area === 0} on:click={() => go(0, "/")}>Start</button>-->
    <!--<button class="m-2 p-1 text-left" class:active={$area === 1} on:click={() => go(1, "/vorplanung")}>Vorplanung</button> -->
    <button class="m-2 p-1 text-left" class:active={$area === 1} on:click={() => go(1, "/ui")}>UI</button>
</div>

<style lang="postcss">
  .active { @apply border-l-4 border-green-500; }
</style>

