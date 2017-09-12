using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Test.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _baseConfig;

        protected async System.Threading.Tasks.Task<T> GetAsync<T>(string uri)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                var baseUri = "http://localhost:39048/api/";
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri + uri);
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseJson);
        }

        protected HttpResponseMessage Get(string uri)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                var baseUri = "http://localhost:39048/api/";
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.GetAsync(baseUri + uri);
                return response.Result;
            }
        }

        protected async System.Threading.Tasks.Task<string> PostAsync(string uri, StringContent data)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                var baseUri = "http://localhost:39048/api/";
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("UserName", GetCurrentUserName());

                var response = await client.PostAsync(baseUri + uri, data);
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseJson);
        }

        protected HttpResponseMessage Post(string uri, StringContent data)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                var baseUri = _baseConfig["ApiUrl"];
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("UserName", GetCurrentUserName());

                var response = client.PostAsync(baseUri + uri, data);
                return response.Result;
            }
        }

        

        protected string GetCurrentUserName()
        {
            
            return string.Empty;
        }
    }
}