namespace QuizVersus.Core.Models.Account
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserInfo
    {
        public string access_token { get; set; }

        public string  userName { get; set; }
    }
}
