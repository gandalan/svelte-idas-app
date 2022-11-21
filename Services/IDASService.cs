using Gandalan.IDAS.WebApi.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Gandalan.IDAS.IDASWebApp.Services
{
    public class IDASService
    {
        public UserAuthTokenDTO AuthToken { get; set; }
        public WebClient WebClient { get; set; }


        public IDASService()
        {
            WebClient = new WebClient();
            WebClient.Headers[HttpRequestHeader.ContentType] = "application/json;+charset=UTF-8";
            WebClient.Headers[HttpRequestHeader.Accept] = "application/json";
            WebClient.Encoding = Encoding.UTF8;
        }

        public bool Login()
        {
            UserAuthTokenDTO result = null;
            if (AuthToken == null || AuthToken.Expires < DateTime.UtcNow)
            {
                var ldto = new LoginDTO()
                {
                    Email = "denis.koenig@gandalan.de",
                    Password = "Transpatec82",
                    AppToken = new Guid("66b70e0b-f7c4-4829-b12a-18ad309e3970")
                };
                string json = JsonConvert.SerializeObject(ldto);
                var resultString = WebClient.UploadString("https://api.dev.idas-cloudservices.net/api/Login/Authenticate", "POST", json);
                result = JsonConvert.DeserializeObject<UserAuthTokenDTO>(resultString);
            }

            if (result != null)
            {
                AuthToken = result;
                WebClient.Headers.Add("X-Gdl-AuthToken: " + AuthToken.Token);
                return true;
            }

            AuthToken = null;
            return false;
        }


        public async Task<VarianteDTO[]> GetAllVariantenFromIDAS()
        {
            if (Login())
            {
                var variantenString = WebClient.DownloadString("https://api.dev.idas-cloudservices.net/api/Variante?includeUIDefs=true&maxLevel=99");
                return JsonConvert.DeserializeObject<VarianteDTO[]>(variantenString);
            }
            return new VarianteDTO[0];
        }

        public async Task<VarianteDTO> GetVarianteFromIDAS(Guid varianteGuid)
        {
            if (Login())
            {
                var variantenString = WebClient.DownloadString($"https://api.dev.idas-cloudservices.net/api/Variante/{varianteGuid}?includeUIDefs=true&includeKonfigs=true");
                return JsonConvert.DeserializeObject<VarianteDTO>(variantenString);
            }
            return null;
        }
    }
}
