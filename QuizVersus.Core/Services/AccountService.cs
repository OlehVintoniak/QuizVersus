using QuizVersus.Core.Models;
using QuizVersus.Core.Models.Account;
using QuizVersus.Core.Services.Abstract;
using QuizVersus.Models.Account;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuizVersus.Core.Services
{
    public class AccountService : BaseHttpService
    {
        public AccountService() : base("Account") { }

        public async Task<int> Register(RegisterModel model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var http = Http();
            using (http)
            using (var response = await http.PostAsync(EntireUrl("Register"), content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return 0;
                }
                return 1;
            }
        }

        public async Task<bool> LogIn(LoginModel model)
        {
            HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.Email),
                new KeyValuePair<string, string>("password", model.Password)
            });

            var http = Http();
            using (http)
            using (var response = await http.PostAsync(UrlForLogin("Token"), content))
            {
                var result = await response.Content.ReadAsStringAsync();
                LoginUserInfo responseModel = JsonConvert.DeserializeObject<LoginUserInfo>(result);
                Access.Token = responseModel.access_token;
                if (Access.Token != null)
                    return true;
            }
            return false;
        }

        public async Task<List<FakePost>> GetFakePosts()
        {
            List<FakePost> responseModel;
            var http = Http();
            using (http)
            using (var response = await http.GetAsync("https://jsonplaceholder.typicode.com/posts"))
            {
                var result = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<List<FakePost>>(result);
            }
            return responseModel;
        }

    }
    public class FakePost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
