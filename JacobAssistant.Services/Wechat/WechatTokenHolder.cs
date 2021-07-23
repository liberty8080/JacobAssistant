using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using JacobAssistant.Common.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JacobAssistant.Services.Wechat
{
    public class WechatTokenHolder
    {
        private readonly AppOptions _options;

        private const string TokenRequestUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=ID&corpsecret=SECRET";

        private DateTime _lastFetchTime;
        private WechatTokenResp _token;

        public string Token
        {
            get
            {
                if (_token != null && !IsExpired(_token.ExpireIn)) return _token.AccessToken;
                Fetch();
                return _token.AccessToken;

            }
        }

        public WechatTokenHolder(IOptions<AppOptions> options)
        {
            _options = options.Value;
            Fetch();
        }

        private bool IsExpired(int expireIn)
        {
            // 超过时间
            return DateTime.Now.CompareTo(_lastFetchTime.AddSeconds(expireIn)) >= 0;
        }

        /// <summary>
        /// Get Token
        /// </summary>
        private void Fetch()
        {
            try
            {
                using var client = new HttpClient();
                var url = TokenRequestUrl
                    .Replace("ID", _options.WechatCorpId)
                    .Replace("SECRET", _options.WechatAppSecret);

                var tempRes = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

                var res = JsonSerializer.Deserialize<WechatTokenResp>(tempRes);
                if (res is not {Errcode: 0})
                {
                    throw new Exception($"wechat token fetch failed! msg: {res?.Errmsg}");
                }

                _lastFetchTime = DateTime.Now;
                _token = res;
            }
            catch (Exception e)
            {
                Log.Error("Wechat Token Fetch Failed ");
                throw new ApplicationException("Wechat Token Fetch Failed ",e);
            }
            
        }
    }


    public class WechatTokenResp
    {
        [JsonPropertyName("errcode")]
        public int Errcode { get; set; }
        [JsonPropertyName("errmsg")]

        public string Errmsg { get; set; }
        [JsonPropertyName("access_token")]

        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]

        public int ExpireIn { get; set; }
    }
}