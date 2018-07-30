
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using QuizVersus.Core.Services;
using QuizVersus.Models.Account;

namespace QuizVersus
{
    [Activity(Label = "RegistrationAvtivity")]
    public class RegistrationAvtivity : Activity
    {
        private Button _registerButton;
        private Button _goToLoginButton;
        private TextView _emailTextView;
        private TextView _passwordTextView;
        private TextView _confirmPasswordTextView;
        private TextView _firstNameTextView;
        private TextView _lastNameTextView;
        private readonly AccountService _accountService = new AccountService();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistrationActivity);
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            _registerButton = FindViewById<Button>(Resource.Id.RegistrationButton);
            _goToLoginButton = FindViewById<Button>(Resource.Id.GoToLoginButton);
            _emailTextView = FindViewById<TextView>(Resource.Id.EmailTextView);
            _passwordTextView = FindViewById<TextView>(Resource.Id.PasswordTextView);
            _confirmPasswordTextView = FindViewById<TextView>(Resource.Id.ConfirmPasswordTextView);
            _firstNameTextView = FindViewById<TextView>(Resource.Id.FirstNameTextView);
            _lastNameTextView = FindViewById<TextView>(Resource.Id.LastNameTextView);
        }

        private void HandleEvents()
        {
            _goToLoginButton.Click += _goToLoginButton_Click;
            _registerButton.Click += _registerButton_Click;
        }

        private void _registerButton_Click(object sender, System.EventArgs e)
        {
            var registerModel = new RegisterModel
            {
                Email = _emailTextView.Text,
                Password = _passwordTextView.Text,
                ConfirmPassword = _confirmPasswordTextView.Text,
                FirstName = _firstNameTextView.Text,
                LastName = _lastNameTextView.Text
            };
            if (string.IsNullOrWhiteSpace(registerModel.Email) ||
                string.IsNullOrWhiteSpace(registerModel.Password) ||
                string.IsNullOrWhiteSpace(registerModel.ConfirmPassword) ||
                string.IsNullOrWhiteSpace(registerModel.FirstName) ||
                string.IsNullOrWhiteSpace(registerModel.LastName))
            {
                Toast.MakeText(this, "All fields must be filled", ToastLength.Short).Show();
            }
            else
            {
                RunOnUiThread(async () =>
                {
                    var progressDialog = ProgressDialog.Show(this, "Please wait...", "Checking account info...", true);
                    var registerResult = await _accountService.Register(registerModel);
                    if (registerResult)
                    {
                        progressDialog.Hide();
                        Intent intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Something went wrong, try again :(", ToastLength.Short).Show();
                    }
                });
            }
        }

        private void _goToLoginButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
    }
}