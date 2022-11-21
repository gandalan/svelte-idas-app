import { IDASFactory, RESTClient } from '@gandalan/weblibs';
import IdasStatus from './IDASStatus.svelte';

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

class Backend 
{
    init(idas) 
    {
        _idas = idas;
        _restClient = new RESTClient(_localApiBaseUrl, idas.restClient.token, true);
    }

    getBaseUrl() {
        return _localApiBaseUrl;
    }


    auth = {
        async getUserName() {
            return await _restClient.get(`Auth/GetUserName`);
        },
        getToken(){
            return _restClient.token
        },
        async getApiUrl(){
            return await _restClient.get(`Auth/GetIDASApiUrl`);
        },
        async isAdmin(){
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
        async getAll() {
            return await _restClient.get(`Variante/GetAllVarianten`);
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
            return await _restClient.get(`UIDefinition/GetUIDefinitionByName?name=`+name);
        }        
    }

}
