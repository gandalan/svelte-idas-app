<script>
  import logo from "./assets/svelte.png";
  import { Router, Route } from "svelte-routing";
  import Navbar from "./Navbar.svelte";
  import { BackendFactory } from "./lib/Backend";
    import UiComponente from "./UI/UIComponente.svelte";

  async function getIDASApiUrl() {
    const backend = await BackendFactory.create();
    let url = localStorage.getItem("IDAS_ApiBaseUrl");
    if (!url) {
      url = await backend.auth.getApiUrl();
      localStorage.setItem("IDAS_ApiBaseUrl", url);
    }
  }

  let promise = getIDASApiUrl();
</script>

{#await promise}
  <div>Lade Daten...</div>
{:then}
  <main class="p-0 m-0 bg-neher-main-border-bg h-screen w-screen flex flex-col">
    <div class="mb-4 items-center p-2 flex">
      <img src={logo} class="w-12 h-12" alt="Logo" title="Logo" />
      <h1
        class="text-xl ml-4 pt-1 border-b-neher-accent-green border-b-4"
        style=""
      >
        <strong>UI-Pflege</strong>
      </h1>
    </div>

    <div class="flex flex-1 overflow-hidden">
      <Navbar />

      <div class="p-2 bg-neher-main-bg overflow-auto w-full h-full">
        <Router>
          <div class="p-4">
            <Route path="/ui" component={UiComponente} />
          </div>
        </Router>
      </div>
    </div>
  </main>
{:catch error}
  <div>URL des IDAS Backend konnte nicht ermittelt werden.</div>
{/await}
