<script>
  import { onDestroy } from "svelte";
  import logoUrl from "./assets/svelte.png";
  import { href, navigate, router, startRouter } from "./lib/router.svelte.js";
  import { notFound, routes } from "./routerConf.js";

  // embedding "neherapp3": module inside the shell (neherapp3.js) - hash routing below the module name
  let { embedding = "standalone", moduleName = "" } = $props();
  // Props do not change after mount, so they are read only once on purpose
  // svelte-ignore state_referenced_locally
  const inNeherApp3 = embedding === "neherapp3";

  // The Vite dev server delivers asset URLs root-relative, which would point to the shell when embedded
  const logo = new URL(logoUrl, import.meta.url).href;

  // Start before the first render so pages can read getPathParams() during their initialization
  // svelte-ignore state_referenced_locally
  onDestroy(startRouter({ mode: inNeherApp3 ? "hash" : "path", prefix: inNeherApp3 ? `${moduleName}/` : "", routes, notFound }));

  const navRoutes = routes.filter((r) => r.title);

  // Standalone demo: switch the theme by hand (main.js sets the initial one from the system setting)
  let theme = $state(document.documentElement.dataset.theme ?? "neher");
  $effect(() => {
    if (!inNeherApp3) {
      document.documentElement.dataset.theme = theme;
    }
  });

  /**
   * In path mode navigate without a full page reload; the hash mode is handled by the browser.
   * @param {MouseEvent} event
   * @param {string} path
   */
  function onNavigate(event, path) {
    if (!inNeherApp3 && !event.ctrlKey && !event.metaKey && !event.shiftKey && event.button === 0) {
      event.preventDefault();
      navigate(path);
    }
  }
</script>

<!-- Named container "content" like the shell's content area, so pages can use @3xl/content: in both cases -->
<div class="bg-base-200 text-base-content {inNeherApp3 ? 'h-full overflow-auto' : 'min-h-screen @container/content'}">
  {#if !inNeherApp3}
    <!-- Standalone only: inside NeherApp3 the shell provides header and menu -->
    <header class="navbar bg-base-100 border-b border-base-300 px-4 gap-2">
      <a class="flex items-center gap-2 font-semibold mr-4" href={href("/")} onclick={(e) => onNavigate(e, "/")}>
        <img src={logo} alt="" class="w-8" />
        Svelte IDAS App
      </a>
      <nav class="tabs tabs-border self-stretch" aria-label="Hauptnavigation">
        {#each navRoutes as route (route.path)}
          <a
            class="tab h-full"
            class:tab-active={router.path === route.path}
            aria-current={router.path === route.path ? "page" : undefined}
            href={href(route.path)}
            onclick={(e) => onNavigate(e, route.path)}
          >{route.title}</a>
        {/each}
      </nav>
      <select class="select select-sm w-auto ml-auto" bind:value={theme} aria-label="Theme">
        <option value="neher">Hell</option>
        <option value="neher-dark">Dunkel</option>
        <option value="i3">i3</option>
      </select>
    </header>
  {/if}

  <main class="p-4 @3xl/content:p-6 max-w-6xl mx-auto">
    {#key router.path}
      <router.component />
    {/key}
  </main>
</div>
