using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace blogapi.Service
{
    public class ContactFormService : IContactFormService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContactFormService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SubmitContactFormAsync(ContactFormModel model)
        {
            var url = "https://getform.io/f/bdrnryeb";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }


   

    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
