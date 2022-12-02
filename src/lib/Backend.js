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




    Idas = {
        async SyncAllVariantenFromIDAS() {
            console.log("Start Variantenabgleich")
            var varianten = await _idas.varianten.getAll();
            for (let v of varianten) {
                console.log(v);
                await _restClient.put(`Variante/AddOrUpdateVariante`, v);
            }
        },
        async SyncAllWertelistenFromIDAS() {
            console.log("Start Wertelistenabgleich")
            var werteliste = await _idas.wertelisten.getAll(false);
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


    Auth = {
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

    Varianten = {
        async GetAll(includeUI = false, onlyDirty = false) {
            return await _restClient.get(`Variante/GetAllVarianten?includeUI=${includeUI}&onlyDirty=${onlyDirty}`);
        },
        async GetAllNamen() {
            return await _restClient.get(`Variante/GetAllVariantenNamen`);
        },
        async GetByName(name, includeUI = false) {
            return await _restClient.get(`Variante/GetVarianteByName?name=${name}&includeUI=${includeUI}`);
        }
    }

    UIDefinition = {
        async GetAll() {
            return _restClient.get(`UIDefinition/GetAllUIDefinition`);
        },
        async GetAllNamen() {
            return await _restClient.get(`UIDefinition/GetAllUIDefinitionNamen`);
        },
        async GetByName(name) {
            return await _restClient.get(`UIDefinition/GetUIDefinitionByName?name=` + name);
        },
        async Save(uidef) {
            return await _restClient.put(`UIDefinition/internal/AddOrUpdateUIDefinition`, uidef);
        },
        async Create(newUIDefName, existingUIDefName) {
            let uri = `UIDefinition/internal/CreateUIDefinition?newUIDefName=${newUIDefName}`;
            if(existingUIDefName) uri += `&vorhandeneUIDefName=${existingUIDefName}`
            return await _restClient.put(uri);
        },
        EingabeFelder: {
            async Create(uidefguid) {
                return await _restClient.put(`UIDefinition/internal/CreateUIFeld?uiDefGuid=${uidefguid}`);
            },
            async Copy(uiDefName, feldGuid)
            {
                return await _restClient.put(`UIDefinition/internal/CopyUIFeld?destUIDefName=${uiDefName}&uiFeldGuid=${feldGuid}`);
            },
            async Delete(feldGuid) {
                
                return await _restClient.delete(`UIDefinition/internal/DeleteUIFeld?guid=${feldGuid}`);
            }
        }

    }

    Wertelisten = {
        async GetAll() {
            return _restClient.get(`Werteliste/GetAllWertelisten`);
        },
        async GetAllNamen() {
            return await _restClient.get(`Werteliste/GetAllWertelistenNamen`);
        },
        async GetByName(name) {
            return await _restClient.get(`Werteliste/GetWertelisteByName?name=` + name);
        }
    }

}
