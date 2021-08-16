﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Jokemachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {

        Dalmanager dal = new Dalmanager();
         
        public List<string> dadJokesEN = D;
        public List<string> blondJokesEN; 

        int index = 0;
        Random random = new Random();



        [HttpGet("Get Joke")]
        public string Get(string category)
        {
            //Method for getting the pref language from the header.
            string language = Request.Headers["Accept-Language"].ToString();
            string[] lang = language.Split(',');
            string prefLang = "";
            foreach (string languages in lang)
            {
                if (languages.Contains("0.9"))
                {
                    prefLang = languages.Remove(2, languages.Length - 2);
                }
            }

            List<string> Dadjokes = HttpContext.Session.GetObjectFromJson<List<string>>("Dadjokes");

            if (category == "Dadjoke")
            {
                if (Dadjokes == null)
                {
                    HttpContext.Session.SetObjectAsJson("Dadjokes", dadJokesEN);
                    List<string> tempDadjokes = HttpContext.Session.GetObjectFromJson<List<string>>("Dadjokes");
                    index = random.Next(tempDadjokes.Count);
                    string tempJoke = tempDadjokes[index];
                    dadJokesEN.RemoveAt(index);
                    HttpContext.Session.SetObjectAsJson("Dadjokes", dadJokesEN);
                    return tempJoke;
                }
                else
                {
                    index = random.Next(Dadjokes.Count);
                    string tempJoke = Dadjokes[index];
                    Dadjokes.RemoveAt(index);
                    HttpContext.Session.SetObjectAsJson("Dadjokes", Dadjokes);
                    return tempJoke;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
