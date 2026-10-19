<script>
  import { Building2, CircleCheck, Server, UserRound } from "@lucide/svelte";
  import { idasBackend, localBackend } from "../../stores";
  import { href } from "../router.svelte.js";
  import UiShowcase from "../components/UiShowcase.svelte";

  /** @type {{ id?: string, benutzerGuid?: string, mandantGuid?: string, rights?: string | string[] } | undefined} */
  const userInfo = /** @type {any} */ ($idasBackend)?.authManager?.userInfo ?? /** @type {any} */ ($idasBackend)?.userInfo;
  const rights = [userInfo?.rights ?? []].flat();

  const mandantPromise = userInfo?.mandantGuid && $idasBackend
    ? /** @type {Promise<import("@gandalan/weblibs").MandantDTO>} */ ($idasBackend.get(`Mandanten/${userInfo.mandantGuid}`))
    : Promise.reject(new Error("Keine IDAS-Anmeldung"));

  /** @type {Promise<unknown>} */
  let counterPromise = $state($localBackend.get("/counter"));

  const steps = [
    { title: "App-Token eintragen", text: "Bei dev-support@gandalan.de anfordern und in main.js eintragen." },
    { title: "Namen festlegen", text: "Service- und Modulname in appConfig.js, Projektname in backend/idas-app.csproj." },
    { title: "Backend konfigurieren", text: "backend/.env.sample nach .env kopieren und den IDAS-Public-Key eintragen. Secrets nur dort ablegen." },
    { title: "Eigene Seiten anlegen", text: "Seiten unter lib/pages/ bauen und in routerConf.js eintragen." },
  ];
</script>

<div class="flex flex-col gap-6">
  <header class="flex flex-wrap items-start justify-between gap-4">
    <div>
      <h1 class="text-2xl font-semibold">Svelte IDAS App</h1>
      <p class="opacity-70">Vorlage für IDAS-Micro-Apps mit Svelte 5 und .NET 10.</p>
    </div>
    <div class="flex flex-wrap gap-2">
      <span class="badge badge-primary">{import.meta.env.DEV ? "Entwicklung" : "Produktion"}</span>
      <span class="badge badge-outline">IDAS {$idasBackend?.baseUrl ? new URL($idasBackend.baseUrl).hostname : "nicht verbunden"}</span>
    </div>
  </header>

  <section class="grid grid-cols-1 gap-4 @3xl/content:grid-cols-3" aria-label="Status">
    <div class="card bg-base-100 border border-base-300">
      <div class="card-body">
        <h2 class="card-title"><UserRound size="20" /> Benutzer</h2>
        {#if userInfo?.id}
          <p class="text-lg font-medium selectable">{userInfo.id}</p>
          <p class="text-sm opacity-70">{rights.length} {rights.length === 1 ? "Recht" : "Rechte"}</p>
        {:else}
          <p class="opacity-70">Nicht angemeldet</p>
        {/if}
      </div>
    </div>

    <div class="card bg-base-100 border border-base-300">
      <div class="card-body">
        <h2 class="card-title"><Building2 size="20" /> Mandant</h2>
        {#await mandantPromise}
          <div class="skeleton h-6 w-40"></div>
          <div class="skeleton h-4 w-24"></div>
        {:then mandant}
          <p class="text-lg font-medium selectable">{mandant.Name}</p>
          <div class="flex flex-wrap gap-2">
            {#if mandant.IstProduzent}<span class="badge badge-soft badge-secondary">Produzent</span>{/if}
            {#if mandant.IstHaendler}<span class="badge badge-soft badge-accent">Händler</span>{/if}
          </div>
          <div class="card-actions justify-end">
            <a class="btn btn-sm btn-ghost" href={href("/mandant")}>Details</a>
          </div>
        {:catch error}
          <p class="text-error text-sm">{error.message}</p>
        {/await}
      </div>
    </div>

    <div class="card bg-base-100 border border-base-300">
      <div class="card-body">
        <h2 class="card-title"><Server size="20" /> Backend</h2>
        {#await counterPromise}
          <span class="loading loading-dots loading-md" role="status" aria-label="Backend wird abgefragt"></span>
        {:then value}
          <p class="flex items-center gap-2"><span class="status status-success"></span> Erreichbar</p>
          <p class="text-sm opacity-70">Antwort von /api/counter: <code>{JSON.stringify(value)}</code></p>
        {:catch error}
          <p class="flex items-center gap-2"><span class="status status-error"></span> Nicht erreichbar</p>
          <p class="text-error text-sm">{error.message}</p>
        {/await}
        <div class="card-actions justify-end">
          <button type="button" class="btn btn-sm" onclick={() => (counterPromise = $localBackend.get("/counter"))}>Erneut abfragen</button>
        </div>
      </div>
    </div>
  </section>

  <section class="card bg-base-100 border border-base-300" aria-labelledby="steps-title">
    <div class="card-body">
      <h2 id="steps-title" class="card-title">Erste Schritte mit der Vorlage</h2>
      <ul class="grid grid-cols-1 gap-3 @3xl/content:grid-cols-2">
        {#each steps as step (step.title)}
          <li class="flex gap-3">
            <CircleCheck size="20" class="text-primary shrink-0 mt-0.5" />
            <div>
              <p class="font-medium">{step.title}</p>
              <p class="text-sm opacity-70">{step.text}</p>
            </div>
          </li>
        {/each}
      </ul>
    </div>
  </section>

  <UiShowcase />
</div>
