import App from './App.svelte';
import './index.css';

import { setup, jwtTokenInvalid, jwtAuthenticateOnBackend } from '@gandalan/weblibs/api/authUtils';

let jwtTokenFromUrl = "";

localStorage.setItem("IDAS_AppToken", "d87d5ab7-a024-4925-81ea-d7495c8b7dd7");
let urlParams = new URLSearchParams(location.search);
if (urlParams.has("m")) {
  localStorage.setItem("IDAS_MandantGuid", urlParams.get("m"));
}
if (urlParams.has("a")) {
  localStorage.setItem("IDAS_ApiBaseUrl", urlParams.get("a"));
}
if (urlParams.has("j")) { // it is JWT
  jwtTokenFromUrl = urlParams.get("j");
}
if (urlParams.has("t")) { // it is authToken
  localStorage.setItem("IDAS_AuthJwtRefreshToken", urlParams.get("t"));
}

let settings = {
  appToken : localStorage.getItem("IDAS_AppToken"),
  mandantGuid : localStorage.getItem("IDAS_MandantGuid"),
  apiBaseurl : localStorage.getItem("IDAS_ApiBaseUrl"),
  authUrl : localStorage.getItem("IDAS_ApiBaseUrl"),
  jwtToken: jwtTokenFromUrl,
  jwtRefreshToken : localStorage.getItem("IDAS_AuthJwtRefreshToken"),
  jwtCallbackPath : localStorage.getItem("IDAS_AuthJwtCallbackPath")
}

try
{
  await setup(settings);
  if (jwtTokenInvalid(settings))
    jwtAuthenticateOnBackend(settings, "/");
} 
catch 
{
  jwtAuthenticateOnBackend(settings, "/");
}

const app = new App({
  target: document.getElementById('app'),
  props : {
    settings
  }
})

export default app
