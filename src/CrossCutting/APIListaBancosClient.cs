using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class APIListaBancosClient : IBancos
    {
        private HttpClient _client;
        private IConfiguration _configuration;

        public APIListaBancosClient(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _client.BaseAddress = new Uri(
                configuration.GetSection("APIListaBancos:UrlBase").Value);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _configuration = configuration;

        }

        public async Task<List<Bancos>> ListarBancos()
        {
            var requestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "");
            
            var retornoApi =  await _client.SendAsync(requestMessage);
            var body = await retornoApi.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Bancos>>(body);
        }
    }
}
