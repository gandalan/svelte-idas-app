<script>
  import { Info, Plus, RotateCcw, Search, TriangleAlert } from "@lucide/svelte";

  // Example data for the table - replace with data from your own API
  const orders = [
    { nummer: "VG-24017", kunde: "Fensterbau Müller", status: "In Arbeit", betrag: 1240.5 },
    { nummer: "VG-24018", kunde: "Tischlerei Weber", status: "Offen", betrag: 389.0 },
    { nummer: "VG-24021", kunde: "Bauelemente Schmidt", status: "Erledigt", betrag: 2875.9 },
  ];

  /** @type {Record<string, string>} */
  const statusBadge = { "Offen": "badge-warning", "In Arbeit": "badge-info", "Erledigt": "badge-success" };

  const currency = new Intl.NumberFormat("de-DE", { style: "currency", currency: "EUR" });

  let search = $state("");
  let onlyOpen = $state(false);
  const visibleOrders = $derived(orders.filter((o) =>
    (!onlyOpen || o.status !== "Erledigt")
    && `${o.nummer} ${o.kunde}`.toLowerCase().includes(search.trim().toLowerCase())));
</script>

<section class="flex flex-col gap-4" aria-labelledby="showcase-title">
  <div>
    <h2 id="showcase-title" class="text-xl font-semibold">Bausteine</h2>
    <p class="text-sm opacity-70">Die gängigen Elemente im aktuellen Theme - als Vorlage für eigene Seiten.</p>
  </div>

  <div class="grid grid-cols-1 gap-4 @3xl/content:grid-cols-2">
    <div class="card bg-base-100 border border-base-300">
      <div class="card-body gap-4">
        <h3 class="card-title text-base">Schaltflächen und Kennzeichen</h3>
        <div class="flex flex-wrap gap-2">
          <button type="button" class="btn btn-primary"><Plus size="16" /> Neu anlegen</button>
          <button type="button" class="btn btn-secondary">Speichern</button>
          <button type="button" class="btn"><RotateCcw size="16" /> Aktualisieren</button>
          <button type="button" class="btn btn-ghost">Abbrechen</button>
          <button type="button" class="btn" disabled>Deaktiviert</button>
        </div>
        <div class="flex flex-wrap gap-2">
          <span class="badge">Standard</span>
          <span class="badge badge-primary">Primär</span>
          <span class="badge badge-soft badge-info">Info</span>
          <span class="badge badge-soft badge-success">Erfolg</span>
          <span class="badge badge-soft badge-warning">Warnung</span>
          <span class="badge badge-soft badge-error">Fehler</span>
        </div>
      </div>
    </div>

    <div class="card bg-base-100 border border-base-300">
      <div class="card-body gap-3">
        <h3 class="card-title text-base">Meldungen</h3>
        <div role="alert" class="alert alert-info alert-soft"><Info size="18" /> <span>Neue Version verfügbar.</span></div>
        <div role="alert" class="alert alert-success alert-soft"><span>Änderungen wurden gespeichert.</span></div>
        <div role="alert" class="alert alert-warning alert-soft"><TriangleAlert size="18" /> <span>Liefertermin liegt in der Vergangenheit.</span></div>
        <div role="alert" class="alert alert-error alert-soft"><span>Speichern fehlgeschlagen.</span></div>
      </div>
    </div>

    <div class="card bg-base-100 border border-base-300">
      <div class="card-body gap-3">
        <h3 class="card-title text-base">Formular</h3>
        <fieldset class="fieldset">
          <legend class="fieldset-legend">Kunde</legend>
          <input class="input w-full" placeholder="z. B. Fensterbau Müller" />
        </fieldset>
        <fieldset class="fieldset">
          <legend class="fieldset-legend">Produktgruppe</legend>
          <select class="select w-full">
            <option>Insektenschutz</option>
            <option>Sonnenschutz</option>
            <option>Zubehör</option>
          </select>
        </fieldset>
        <fieldset class="fieldset">
          <legend class="fieldset-legend">Bemerkung</legend>
          <textarea class="textarea w-full" rows="2" placeholder="Optional"></textarea>
        </fieldset>
        <label class="label gap-3">
          <input type="checkbox" class="toggle toggle-primary" checked />
          Benachrichtigung senden
        </label>
      </div>
    </div>

    <div class="card bg-base-100 border border-base-300">
      <div class="card-body gap-3">
        <h3 class="card-title text-base">Liste</h3>
        <div class="flex flex-wrap items-center gap-3">
          <label class="input input-sm grow">
            <Search size="16" class="opacity-50" />
            <input type="search" placeholder="Suchen" bind:value={search} />
          </label>
          <label class="label gap-2 text-sm">
            <input type="checkbox" class="checkbox checkbox-sm" bind:checked={onlyOpen} />
            Nur offene
          </label>
        </div>
        <div class="overflow-x-auto">
          <table class="table">
            <thead>
              <tr><th>Vorgang</th><th>Kunde</th><th>Status</th><th class="text-right">Betrag</th></tr>
            </thead>
            <tbody>
              {#each visibleOrders as order (order.nummer)}
                <tr class="hover:bg-base-200">
                  <td class="font-mono whitespace-nowrap">{order.nummer}</td>
                  <td>{order.kunde}</td>
                  <td><span class="badge badge-soft whitespace-nowrap {statusBadge[order.status]}">{order.status}</span></td>
                  <td class="text-right tabular-nums whitespace-nowrap">{currency.format(order.betrag)}</td>
                </tr>
              {:else}
                <tr><td colspan="4" class="text-center opacity-70">Keine Treffer</td></tr>
              {/each}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</section>
