using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Models;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly AppOptions _options;

        public ConfigController(IOptions<AppOptions> options)
        {
            _options = options.Value;
        }

        /* [HttpGet]
         public IDictionary<string, string> GetAll()
         {
             return _configuration.AsEnumerable()
                 .ToDictionary(item => item.Key, item => item.Value);
         }
 
         private readonly ConfigService _service;
 
         public ConfigController(ConfigService service)
         {
             _service = service;
         }
 
         // GET: api/ConfigController
         [HttpGet]
         public IEnumerable<Config> Get()
         {
             return _service.GetAll();
         }
 
         // GET: api/ConfigController/{name}
         [HttpGet("{name}", Name = "Get")]
         public Config Get(String name)
         {
             return _service.GetConfig(name);
         }*/
    }
}