using Microsoft.AspNetCore.Http;
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
        int index = 0;
        Random random = new Random();



        [HttpGet("Get Joke")]
        public string Get(string category)
        {
            //All jokes from dal
            List<string> dadJokesEN = dal.dadJokesEN;
            List<string> blondineJokesDK = dal.blondineJokesDK;


            //Method for getting the pref language from the header.
            //Starts with taking the Accept-language from the header -
            //then splits it to find the longest, which is the users prefered language -
            //the final result will be "da"/"en" which is the 'prefLang' attribute.
            
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

            //Checks what prefered language is
            //To pick a catogory:
            //Input options = "Dadjoke" and "Blondinejoke" is implemented.
            if (prefLang == "en")
            {
                //Checks if thers currently "dadjokes" in the session
                List<string> Dadjokes = HttpContext.Session.GetObjectFromJson<List<string>>("Dadjokes");
                if (category == "Dadjoke")
                {
                    //if there isn't dadjokes in the session it will fill a new one with the "dadJokesEN" from the dalmanager.
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
                else if(category == "")
                {
                    return null;
                }
            }
            else if (prefLang == "da")
            {
                List<string> Blondinejokes = HttpContext.Session.GetObjectFromJson<List<string>>("Blondinejokes");
                if (category == "Blondinejoke")
                {
                    if (Blondinejokes == null)
                    {
                        HttpContext.Session.SetObjectAsJson("Blondinejokes", blondineJokesDK);
                        List<string> tempBlondinejokes = HttpContext.Session.GetObjectFromJson<List<string>>("Blondinejokes");
                        index = random.Next(tempBlondinejokes.Count);
                        string tempJoke = tempBlondinejokes[index];
                        blondineJokesDK.RemoveAt(index);
                        HttpContext.Session.SetObjectAsJson("Blondinejokes", blondineJokesDK);
                        return tempJoke;
                    }
                    else
                    {
                        index = random.Next(Blondinejokes.Count);
                        string tempJoke = Blondinejokes[index];
                        Blondinejokes.RemoveAt(index);
                        HttpContext.Session.SetObjectAsJson("Blondinejokes", Blondinejokes);
                        return tempJoke;
                    }
                }
                else if (category == "")
                {
                    return null;
                }
            }
            return null;
        }
    }
}
