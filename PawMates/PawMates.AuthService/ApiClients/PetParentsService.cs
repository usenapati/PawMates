using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System.Net;

namespace PawMates.AuthService.ApiClients
{
    public class PetParentsService : IPetParentsService
    {
        // HttpFactory injected into the constructor
        private IHttpClientFactory _clientFactory { get; }
        private HttpClient client;
        private CircuitBreakerPolicy circuitBreakerPolicy;
        AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy;
        public PetParentsService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            //HttpClient client = _clientFactory.CreateClient();
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            client = new HttpClient(handler);

            client.BaseAddress = new Uri("https://localhost:7197");
            timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreaker(2, TimeSpan.FromSeconds(5));

        }
        public async Task<PetParentDTO>? GetPetParentAsync(int petParentId)
        {
            var response = await circuitBreakerPolicy.Execute(() => timeoutPolicy.ExecuteAsync(() => client.GetAsync($"api/Parents/{petParentId}")));

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                var petParent = await response.Content.ReadFromJsonAsync<PetParentDTO>();
                return petParent;
            }
            return null;
        }

        public async Task<List<PetParentDTO>?> GetPetParentsAsync()
        {
            var response = await circuitBreakerPolicy.Execute(() => timeoutPolicy.ExecuteAsync(() => client.GetAsync("api/Parents")));

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                var petParents = await response.Content.ReadFromJsonAsync<List<PetParentDTO>>();
                return petParents;
            }
            return null;
        }
    }
}
