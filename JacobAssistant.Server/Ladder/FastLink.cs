using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JacobAssistant.Exceptions;

namespace JacobAssistant.Ladder
{
    public class FastLink
    {
        private const string LoginUrl = "https://fastlink.ws/auth/login";
        private const string UserPage = "https://fastlink.ws/user";
        private string _userPageContent;
        private readonly string _email;
        private readonly string _passwd;
        private static FastLink _instance;
        private static readonly object Locker = new();
        
        private readonly HttpClient _client = new(new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true
        });

        public FastLink(string email, string passwd)
        {
            _email = email;
            _passwd = passwd;
            AddHeaders();
        }
        
        
        public void GetUserPage()
        {
            var result = _client.GetAsync(UserPage).Result.Content.ReadAsStringAsync().Result;
            _userPageContent = result;
        }

        /// <summary>
        /// 添加header
        /// </summary>
        private void AddHeaders()
        {
            _client.DefaultRequestHeaders.Add("UserAgent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <exception cref="FastLinkLoginFailedException">登录失败Exception</exception>
        public void Login()
        {
            var payload = new List<KeyValuePair<string, string>>
            {
                new("email", _email), new("passwd", _passwd), new("code", "")
            };
            var res = _client
                .PostAsync(LoginUrl, new FormUrlEncodedContent(payload))
                .Result;
            var loginRes = res.Content.ReadAsStringAsync().Result;
            if (!loginRes.Contains("\"ret\":1"))
            {
                throw new FastLinkLoginFailedException(loginRes);
            }

        }

        public string GetInfo()
        {
            GetUserPage();
            return $"fastlink\n剩余流量:{GetFlow()}\n剩余天数:{GetExpireTime()}\n";
        }

        /// <summary>
        /// 剩余流量查询
        /// </summary>
        /// <returns>100 GB</returns>
        public string GetFlow()
        {
            return GetFlow(_userPageContent);
        }
        public string GetFlow(string content)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var flow = htmlDoc.DocumentNode
                .SelectSingleNode("//h4[contains(text(),\"剩余流量\")]/../../div[@class='card-body']")
                .InnerText.Trim();
            
            var match = Regex.Match(flow, @"\d+.\d+\s[(GB)(MB)]");
            if (match.Success)
            {
                return flow;
            }

            throw new Exception($"流量获取失败: {flow}");
        }

        /// <summary>
        /// 剩余天数查询
        /// </summary>
        /// <returns>7 天</returns>
        public string GetExpireTime()
        {
            return GetExpireTime(_userPageContent);
        }
        public string GetExpireTime(string content)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var expireTime = htmlDoc.DocumentNode
                .SelectSingleNode("//h4[contains(text(),\"会员时长\")]/../../div[@class='card-body']")
                .InnerText.Trim();
            
            var match = Regex.Match(expireTime, @"\d+\s天");
            if (match.Success)
            {
                return expireTime;
            }

            throw new Exception($"时间获取失败:{expireTime}");
        }

        public static FastLink GetInstance(string email,string passwd)
        {
            if (_instance == null)
            {
                lock (Locker)
                {
                    if (_instance == null)
                    {
                        _instance = new FastLink(email, passwd);
                        _instance.Login();
                    }
                }
            }

            return _instance;
        }
    }
}