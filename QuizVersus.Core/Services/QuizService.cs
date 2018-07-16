using QuizVersus.Core.Models;
using QuizVersus.Core.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuizVersus.Core.Services
{
    public class QuizService : BaseHttpService
    {
        public QuizService() : base("Quiz") { }

        public async Task<List<SendedQuizSimple>> GetSended()
        {
            List<SendedQuizSimple> responseModel;
            var http = HttpWithToken();
            using (http)
            using (var response = await http.GetAsync(EntireUrl("Sended")))
            {
                var result = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<List<SendedQuizSimple>>(result);
            }
            return responseModel;
        }

        public async Task<List<RecivedQuizSimple>> GetRecived()
        {
            List<RecivedQuizSimple> responseModel;
            var http = HttpWithToken();
            using (http)
            using (var response = await http.GetAsync(EntireUrl("Recived")))
            {
                var result = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<List<RecivedQuizSimple>>(result);
            }
            return responseModel;
        }

        public async Task<EntireQuiz> SendQuickQuiz()
        {
            EntireQuiz responseModel;
            var http = HttpWithToken();
            using (http)
            using (var response = await http.PostAsync(EntireUrl("Quick"), null))
            {
                var result = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<EntireQuiz>(result);
            }
            return responseModel;
        }

        private HttpClient HttpWithToken()
        {
            var http = Http();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Access.Token);
            return http;
        }

    }
}
