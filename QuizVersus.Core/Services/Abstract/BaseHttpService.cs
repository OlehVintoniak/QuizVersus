using System;
using System.Net.Http;

namespace QuizVersus.Core.Services.Abstract
{
    public abstract class BaseHttpService
    {
        private readonly string _baseUrl = "http://quizversus.apphb.com/api/";
        private readonly string _baseForLogin;
        protected BaseHttpService(string controllerName)
        {
            _baseForLogin = _baseUrl;
            _baseUrl += $"{controllerName}/";
        }

        protected HttpClient Http()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Accept", "application/json");
            http.BaseAddress = new Uri(_baseUrl);
            return http;
        }

        protected string EntireUrl(string url)
        {
            return $"{_baseUrl}/{url}";
        }

        protected string UrlForLogin(string url)
        {
            return $"{_baseForLogin}/{url}";
        }
    }
}
