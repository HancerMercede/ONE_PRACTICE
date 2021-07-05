using CensoApp.Entities;
using CensoApp.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CensoApp.Controllers
{
    public class ProvinciaController:Controller
    {
        private readonly ILogger<ProvinciaController> _Logger;
        private  IConfiguration _configuration;
        public ProvinciaController(ILogger<ProvinciaController> Logger,IConfiguration configuration)
        
        {
            _Logger = Logger;
            _configuration = configuration;
        }
        public IEnumerable<Provincia> Provincias { get; set; }
        public ActionResult<Provincia> Get()
        {
            var provincias = new Provincia();
            var url = $"http://provinciasrd.raydelto.org/provincias";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return NotFound();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                    
                            provincias = JsonSerializer.Deserialize<Provincia>(responseBody); 
                            // Do something with responseBody
                            return View(provincias);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("provincia/IndexAsync")]
        public async Task<ActionResult<IEnumerable<Provincia>>> IndexAsync() 
        {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiUrl:Url"));
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpClientMessage = await httpClient.GetAsync("http://provinciasrd.raydelto.org/provincias");
                
                if (httpClientMessage.IsSuccessStatusCode) {

                    var response = await httpClientMessage.Content.ReadAsStringAsync();
                    var JsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                   Provincias = JsonSerializer.Deserialize<IEnumerable<Provincia>>(response,JsonOptions);
                
                }
            }
            return View(Provincias);
        }
    }
}
