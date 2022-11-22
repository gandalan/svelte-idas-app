<script>
  import { area } from './stores';
  import { navigate } from 'svelte-routing';

  if (window.location.pathname === "/ui")
    $area = 1;
  if (window.location.pathname === "/werteliste")
    $area = 2;
    if (window.location.pathname === "/konfigsatz")
    $area = 3
    if (window.location.pathname === "/sync")
    $area = 4
    
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
    <button class="m-2 p-1 text-left" class:active={$area === 2} on:click={() => go(2, "/werteliste")}>Wertelisten</button>
    <button class="m-2 p-1 text-left" class:active={$area === 3} on:click={() => go(3, "/konfigsatz")}>Konfigsatz</button>
    <button class="m-2 p-1 text-left" class:active={$area === 4} on:click={() => go(4, "/sync")}>Sync</button>
</div>

<style lang="postcss">
  .active { @apply border-l-4 border-green-500; }
</style>

