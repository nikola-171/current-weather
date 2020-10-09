using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projekat.Models;
using projekat.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Nancy.Json;

namespace projekat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
         
   
        
        /*dodato*/
       
        [HttpPost]
        public string Daj()
        {
            
            /*enter a valid api id*/
            string appId = "";
            
            string grad = HttpContext.Request.Form["cityname"];
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", grad, appId);

            if(appId == "")
            {
                return "missing";
            }
            string json = null;
                using (WebClient client = new WebClient())
                {
                    try {
                        json = client.DownloadString(url);
                    }
                    catch (WebException exp)
                    {
                        if(((HttpWebResponse) exp.Response).StatusCode == HttpStatusCode.NotFound)
                        {
                            return "404";
                        }
                    }

                    //Converting to OBJECT from JSON string.  
                    RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                    if(weatherInfo.cod == 404)
                    {
                        return "404";

                    }
                    
                    ResultViewModel rslt = new ResultViewModel();

                    rslt.Country = weatherInfo.sys.country;
                    rslt.City = weatherInfo.name;
                    rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                    rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                    rslt.Description = weatherInfo.weather[0].description;
                    rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                    rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                    rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                    rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                    rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                    rslt.WeatherIcon = weatherInfo.weather[0].icon;

                    //Converting OBJECT to JSON String   
                    var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                    //Return JSON string.  
                    return jsonstring;
                }    
        } 

       
        public IActionResult Vreme()
        {
            return View();
        }
 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
