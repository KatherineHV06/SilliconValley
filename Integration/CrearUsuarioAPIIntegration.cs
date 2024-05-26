using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace SilliconValley.Integration
{
    public class CrearUsuarioAPIIntegration
    {
        private readonly ILogger<CrearUsuarioAPIIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users";
        private readonly HttpClient httpClient;

        public CrearUsuarioAPIIntegration(ILogger<CrearUsuarioAPIIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }

        public async Task<ApiResponse> CreateUser(string name, string job)
        {
            string requestUrl = API_URL;
            ApiResponse apiResponse = null;
            try
            {
                // Construir el objeto de datos del usuario a enviar en la solicitud
                var userData = new { name, job };
                // Convertir el objeto a JSON
                var jsonUserData = JsonSerializer.Serialize(userData);
                // Crear una solicitud HTTP POST con los datos del usuario
                var requestContent = new StringContent(jsonUserData, Encoding.UTF8, "application/json");

                // Enviar la solicitud HTTP POST a la API
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl, requestContent);
                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta de la API
                    var responseBody = await response.Content.ReadAsStringAsync();
                    // Deserializar la respuesta JSON
                    apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseBody);
                }
                else
                {
                    // Manejar el error si la solicitud no fue exitosa
                    _logger.LogDebug($"La solicitud POST a la API no fue exitosa. Código de estado: {response.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la solicitud
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return apiResponse;
        }

        public class ApiResponse
        {
            public string Name { get; set; }
            public string Job { get; set; }
            public string Id { get; set; }
            public DateTime CreatedAt { get; set; }
        }

    }
}