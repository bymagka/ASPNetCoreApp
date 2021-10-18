using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Dictionary<int, string> ValuesDict = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value: {i}"))
            .ToDictionary(v => v.Id, v => v.Value);

        public ValuesController()
        {

        }

        [HttpGet]
        public IActionResult Get() => Ok(ValuesDict.Values);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!ValuesDict.ContainsKey(id))
                return NotFound();

            return Ok(ValuesDict[id]);

        }

        [HttpGet("count")]
        public IActionResult Count() => Ok(ValuesDict.Count);

        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add(string value)
        {
            var id = ValuesDict.Count == 0 ? 1 : ValuesDict.Keys.Max() + 1;

            ValuesDict[id] = value;

            return CreatedAtAction("GetById", new { id = id });
        }

        [HttpPut("{id}")]
        public IActionResult Replace(int id, string value)
        {
            if (!ValuesDict.ContainsKey(id))
                return NotFound();

            ValuesDict[id] = value;

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ValuesDict.ContainsKey(id))
                return NotFound();

            ValuesDict.Remove(id);

            return Ok();
        }
    }
}
