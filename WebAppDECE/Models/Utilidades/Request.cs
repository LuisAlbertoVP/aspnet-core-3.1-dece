using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WebAppDECE.Models.Utilidades
{
    public class Request<T>
    {
        private readonly string host = "https://localhost:44379";
        private readonly string _url;
        private readonly HttpClient client = new HttpClient();
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings();
        private readonly HandleError _handle;

        public Request(HandleError handle, string url)
        {
            _handle = handle;
            _url = url;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public Request(HandleError handle, string url, string token)
        {
            _handle = handle;
            _url = url;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            settings.NullValueHandling = NullValueHandling.Ignore;
        }


        public async Task<T> SendGet()
        {

            var response = await client.GetAsync(host + _url);
            if (response.IsSuccessStatusCode)
            {
                string json_received = await response.Content.ReadAsStringAsync(); 
                T t = JsonConvert.DeserializeObject<T>(json_received);
                return t;
            }
            else
            {
                throw new RequestException(response.StatusCode.ToString()).Handle(_handle);
            }
        }

        public async Task<T> SendPost(T t) 
        {
            string json = JsonConvert.SerializeObject(t, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(host + _url, content);
            if (response.IsSuccessStatusCode)
            {
                string json_received = await response.Content.ReadAsStringAsync();
                t = JsonConvert.DeserializeObject<T>(json_received);
                return t;
            }
            else
            {
                throw new RequestException(response.StatusCode.ToString()).Handle(_handle);
            }
        }

        public async Task<Token> Authenticate(Seguridad.Usuario user)
        {
            string json = JsonConvert.SerializeObject(user, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(host + _url, content);
            if (response.IsSuccessStatusCode)
            {
                string json_received = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(json_received);
                return token;
            }
            else
            {
                throw new RequestException(response.StatusCode.ToString()).Handle(_handle);
            }
        }
    }
}
