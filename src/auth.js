import { setup, isInvalid, redirectToLogin } from "@gandalan/weblibs/api/authUtils";

export async function initIDAS(apptoken)
{
    let jwtTokenFromUrl = "";
    localStorage.setItem("IDAS_AppToken", apptoken);
    let urlParams = new URLSearchParams(location.search);
    if (urlParams.has("m"))
    {
        localStorage.setItem("IDAS_MandantGuid", urlParams.get("m"));
    }
    if (urlParams.has("a"))
    {
        localStorage.setItem("IDAS_ApiBaseUrl", urlParams.get("a"));
    }
    if (urlParams.has("j")) // it is JWT
    {
        jwtTokenFromUrl = urlParams.get("j");
    }
    if (urlParams.has("t")) // it is authToken
    {
        localStorage.setItem("IDAS_AuthJwtRefreshToken", urlParams.get("t"));
    }

    try
    {
        if (window.chrome?.webview?.hostObjects?.sync?.idasJwtToken)
        {
            jwtTokenFromUrl = JSON.parse(window.chrome?.webview?.hostObjects?.sync?.idasJwtToken);
        }
    }
    catch
    // eslint-disable-next-line no-empty
    {}

    let settings = {
        appToken: localStorage.getItem("IDAS_AppToken"),
        mandantGuid: localStorage.getItem("IDAS_MandantGuid"),
        apiBaseurl: localStorage.getItem("IDAS_ApiBaseUrl") || "https://api.dev.idas-cloudservices.net/api/",
        authUrl: localStorage.getItem("IDAS_ApiBaseUrl") || "https://api.dev.idas-cloudservices.net/SSO",
        jwtToken: jwtTokenFromUrl,
        jwtRefreshToken: localStorage.getItem("IDAS_AuthJwtRefreshToken"),
        jwtCallbackPath: localStorage.getItem("IDAS_AuthJwtCallbackPath"),
    }

    // eslint-disable-next-line no-console
    console.log(settings)

    try
    {
        await setup(settings);
        if (isInvalid(settings))
        {
            redirectToLogin(settings, "/");
        }
    }
    catch
    {
        redirectToLogin(settings, "/");
    }
    return settings;
}
