using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Test.Controllers
{
    public class HomeController : BaseController
    {
        
        public async Task<ActionResult> Index()
        {
            string user = "quynhnv";
            string pass = "quynhvan@123";
            var token= "";
            var baseUri = "http://localhost:39048/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri + "token/get?username=" + user + "&password=" + pass);
                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();                    
                }
            }
            token = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(token);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(baseUri + "Value/Get?s=quynhnv111");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}