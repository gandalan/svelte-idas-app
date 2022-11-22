import { IDASFactory, RESTClient } from '@gandalan/weblibs';

let _localApiBaseUrl = `//${window.location.host}/api/`;
let _restClient;
let _idas = null;

export let BackendFactory = {
    async create() {
        const backend = new Backend();
        backend.init(await IDASFactory.create());
        return backend;
    }
}

class Backend {
    init(idas) {
        _idas = idas;
        _restClient = new RESTClient(_localApiBaseUrl, idas.restClient.token, true);
    }

    getBaseUrl() {
        return _localApiBaseUrl;
    }




    idas = {
        async SyncAllVariantenFromIDAS() {
            var varianten = await _idas.varianten.getAll();
            for (let v of varianten) {
                console.log(v);
                await _restClient.put(`Variante/AddOrUpdateVariante`, v); 
            }
        },
        async SyncAllWertelistenFromIDAS() {
            var werteliste = await _idas.wertelisten.getAll();
            for (let w of werteliste) {
                console.log(w);
                await _restClient.put(`Werteliste/AddOrUpdateWerteliste`, w); 
            }
        },
        async SyncVarianteFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncKonfigSatzFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncKonfigSatzEintragFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncUIDefinitionFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncUIEingabefeldFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncUIKonfiguratorFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncWertelisteFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        },
        async SyncWertelistenItemFromIDAS(guid) {
            var idasVariante = await _idas.varianten.get(guid);
        }


    }


    auth = {
        async getUserName() {
            return await _restClient.get(`Auth/GetUserName`);
        },
        getToken() {
            return _restClient.token
        },
        async getApiUrl() {
            return await _restClient.get(`Auth/GetIDASApiUrl`);
        },
        async isAdmin() {
            return await _restClient.get(`Auth/IsAdmin`);
        }
    };

    vorgaenge = {
        async getByGuid(guid) {
            return await _idas.vorgaenge.getByGuid(guid);
        },
        async getByVorgangsnummer(vorgangsNummer, jahr) {
            return await _idas.vorgaenge.getByVorgangsnummer(vorgangsNummer, jahr);
        },
    };

    positionen = {
        async getByPcode(pcode) {
            return await _idas.positionen.getByPcode(pcode);
        },
    };

    varianten = {
        async getAll(includeUI = false, onlyDirty = false) {
            return await _restClient.get(`Variante/GetAllVarianten?includeUI=${includeUI}&onlyDirty=${onlyDirty}`);
        },
        async getAllNamen() {
            return await _restClient.get(`Variante/GetAllVariantenNamen`);
        },
        async getByName(name, includeUI = false) {
            return await _restClient.get(`Variante/GetVarianteByName?name=${name}&includeUI=${includeUI}`);
        }
    }

    uidef = {
        async getAll() {
            return _restClient.get(`UIDefinition/GetAllUIDefinition`);
        },
        async getAllNamen() {
            return await _restClient.get(`UIDefinition/GetAllUIDefinitionNamen`);
        },
        async getByName(name) {
            return await _restClient.get(`UIDefinition/GetUIDefinitionByName?name=` + name);
        },
        async save(guid, uidef){
            return await _restClient.put(`UIDefinition/internal/AddOrUpdateUIDefinition?guid=${guid}`, uidef);
        }
    }

    wertelisten = {
        async getAll() {
            return _restClient.get(`Werteliste/GetAllWertelisten`);
        },
        async getAllNamen() {
            return await _restClient.get(`Werteliste/GetAllWertelistenNamen`);
        },
        async getByName(name) {
            return await _restClient.get(`Werteliste/GetWertelisteByName?name=` + name);
        }
    }

}
