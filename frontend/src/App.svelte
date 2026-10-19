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

<main class={inNeherApp3 ? "h-full" : "min-h-screen"}>
  <nav class="flex items-center gap-4 mb-4">
    <img src={logo} alt="Svelte Logo" class="w-16" />
    <a href={href("/")} onclick={(e) => onNavigate(e, "/")}>Mandant</a>
    <a href={href("/counter")} onclick={(e) => onNavigate(e, "/counter")}>Counter</a>
  </nav>

  {#key router.path}
    <router.component />
  {/key}
</main>
