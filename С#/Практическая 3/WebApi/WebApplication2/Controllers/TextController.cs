using Microsoft.AspNetCore.Mvc;
using WordCounterLibrary;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : Controller
    {
        [HttpGet]
        public string Get() 
        {
            var text = "Hi there!";
            return text;
        }

        [HttpPost]
        public ActionResult<Dictionary<string, int>> Post([FromBody] string text) 
        {
            WordCounter wordCounter = new WordCounter();
            Dictionary<string, int> result = wordCounter.CountWordsMultiThreaded(text);
            return Ok(result);
        }
    }
}
