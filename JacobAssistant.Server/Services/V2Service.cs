using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;
using Newtonsoft.Json;
using static JacobAssistant.Services.HttpClientService;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JacobAssistant.Services
{
    public class V2Service
    {
        private readonly string _subLink;

        public V2Service(string subLink)
        {
            _subLink = subLink;
        }
        public string GetV2Sub()
        {
            return GetAsync(_subLink).GetAwaiter().GetResult();
        }

        public string Expire()
        {
            var s = from entity in DecryptSub(GetV2Sub())
                where entity.ps.Contains("过期")
                select entity.ps;
            return s.FirstOrDefault();
        }

        public List<VMessEntity> DecryptSub(string subData)
        {
            var base64EncodedBytes  = Convert.FromBase64String(subData);
            var vMesses = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var vMessEntities = new List<VMessEntity>();
            foreach (var vMess in vMesses.Split("\n"))
            {
                if (vMess.Equals("")) continue;
                var temp = vMess.Replace("vmess://", "");
                var vMessJson = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(temp));
                var obj = JsonSerializer.Deserialize<VMessEntity>(vMessJson);
                vMessEntities.Add(obj);

            }
            return vMessEntities;
        }
        
        
        
    }
}