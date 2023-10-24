using PawMates.CORE.DTOs;
using Polly.CircuitBreaker;
using Polly.Timeout;
using Polly;
using System.Net;
using Microsoft.Extensions.Logging;

namespace PawMates.PlayDateAPI.ApiClients
{
    public class PetsService : IPetsService
    {
        // HttpFactory injected into the constructor
        private IHttpClientFactory _clientFactory { get; }
        private HttpClient client;
        private CircuitBreakerPolicy circuitBreakerPolicy;
        AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy;
        public PetsService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            //HttpClient client = _clientFactory.CreateClient();
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            client = new HttpClient(handler);

            client.BaseAddress = new Uri("https://localhost:7254");
            timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreaker(2, TimeSpan.FromSeconds(5));

        }
        public async Task<PetDTO>? GetPetAsync(int petId)
        {
            var response = await circuitBreakerPolicy.Execute(() => timeoutPolicy.ExecuteAsync(() => client.GetAsync($"api/Pets/{petId}")));

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                var pet = await response.Content.ReadFromJsonAsync<PetDTO>();
                return pet;
            }
            return null;
        }

        public async Task<List<PetDTO>?> GetPetsAsync()
        {
            var response = await circuitBreakerPolicy.Execute(() => timeoutPolicy.ExecuteAsync(() => client.GetAsync("api/Pets")));

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                var Pets = await response.Content.ReadFromJsonAsync<List<PetDTO>>();
                return Pets;
            }
            return null;
        }
    }
}
