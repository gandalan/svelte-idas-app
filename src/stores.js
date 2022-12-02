import { writable} from "svelte/store";
const area = writable(0);

export { area };


export const Selection = writable({
    SelectedUIDefinition: null,
    DBUIDefinition: null,
    SelectedUIFelder: [],
    SelectedKonfiguratorFeld: null,
    SelectedVariante: null,
    SelectedWerteliste:null
});

export const Backend = writable();
