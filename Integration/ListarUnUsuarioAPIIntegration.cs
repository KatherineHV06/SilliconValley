using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilliconValley.Integration.dto;
using SilliconValley.Integration;


namespace SilliconValley.Integration
{
    public class ListarUnUsuarioAPIIntegration
    {
        private readonly ILogger<ListarUnUsuarioAPIIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users/";
        private readonly HttpClient httpClient;

        public ListarUnUsuarioAPIIntegration(ILogger<ListarUnUsuarioAPIIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }

        public async Task<Usuario> GetUser(int Id)
        {

            string requestUrl =  $"{API_URL}{Id}";
            Usuario usuario = new Usuario();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (apiResponse != null)
                    {
                        usuario = apiResponse.Data ?? new Usuario();
                    }
                }
            }
            catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return usuario;

        }
        class ApiResponse
        {
            public Usuario Data { get; set; }
            public Support Support { get; set; }
        }
    }
}