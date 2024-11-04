using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIAnallyzer_v2.Services
{
    public class ValidationService
    {
        private readonly HttpClient _httpClient;

        public ValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-mails-api-key", "56bd511c-4610-4a49-961d-6ea0ae9073e0");
        }

        /// <summary>
        /// Valida se o e-mail contém o símbolo "@" e tem um formato mínimo válido.
        /// </summary>
        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@");
        }

        /// <summary>
        /// Verifica se o CNPJ possui exatamente 14 dígitos numéricos.
        /// </summary>
        public bool IsValidCNPJ(string cnpj)
        {
            return !string.IsNullOrEmpty(cnpj) && cnpj.Length == 14 && long.TryParse(cnpj, out _);
        }

        /// <summary>
        /// Valida o e-mail utilizando uma API externa.
        /// </summary>
        public async Task<bool> IsEmailValidAsync(string email)
        {
            if (!IsValidEmail(email)) return false;
            
            _httpClient.DefaultRequestHeaders.Add("x-mails-api-key", "56bd511c-4610-4a49-961d-6ea0ae9073e0");
            
            var response = await _httpClient.GetAsync($"https://api.mails.so/v1/validate?email={email}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<EmailValidationResponse>(jsonResponse);
                
                return result?.Result != "undeliverable"; 
            }
            return false;
        }

    }

    public class EmailValidationResponse
    {
        public string Result { get; set; }
    }
}