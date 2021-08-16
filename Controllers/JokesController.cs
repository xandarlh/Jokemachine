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
        public List<string> dadJokesEN = new List<string>() {
            "I'm afraid for the calendar. Its days are numbered.",
            "My wife said I should do lunges to stay in shape. That would be a big step forward",
            "Why do fathers take an extra pair of socks when they go golfing? - In case they get a hole in one!",
            "Singing in the shower is fun until you get soap in your mouth. Then it's a soap opera.",
            "What do a tick and the Eiffel Tower have in common? - They're both Paris sites.",
            "How do you follow Will Smith in the snow? - You follow the fresh prints."
        };
        public List<string> blondJokesEN = new List<string>() {
            "Ha ha ha",
            "Ha ha",
            "Ha"};

        int index = 0;
        Random random = new Random();

        //Method for getting the pref language from the header.
        public string GetPrefLang()
        {
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
            return prefLang;
        }
        [HttpGet("Get Joke")]
        public string Get(string category)
        {
            string prefLang = GetPrefLang();
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
