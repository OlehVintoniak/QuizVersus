
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using QuizVersus.Core.Models.Account;
using QuizVersus.Core.Services;

namespace QuizVersus
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private Button _loginButton;
        private Button _goToRegistrationButton;
        private TextView _loginTextView;
        private TextView _passwordTextView;
        private readonly AccountService _accountService = new AccountService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginActivity);
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            _loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            _goToRegistrationButton = FindViewById<Button>(Resource.Id.GoToRegistrationButton);
            _loginTextView = FindViewById<TextView>(Resource.Id.LoginTextView);
            _passwordTextView = FindViewById<TextView>(Resource.Id.PasswordTextView);
        }

        private void HandleEvents()
        {
            _goToRegistrationButton.Click += GoToRegistrationButton_Click;
            _loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            var email = _loginTextView.Text;
            var password = _passwordTextView.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Toast.MakeText(this, "Email and password are required", ToastLength.Short).Show();
            }
            else
            {
                RunOnUiThread(async () =>
                {
                    var progressDialog = ProgressDialog.Show(this, "Please wait...", "Checking account info...", true);
                    var loginResult = await _accountService.LogIn(new LoginModel
                    {
                        Email = email, Password = password
                    });
                    if (loginResult)
                    {
                        progressDialog.Hide();
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Incorrect login or password", ToastLength.Short).Show();
                    }
                });
            }
        }

        private void GoToRegistrationButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegistrationAvtivity));
            StartActivity(intent);
        }
    }
}