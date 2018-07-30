using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using QuizVersus.Core.Services;
using QuizVersus.Fragments;

namespace QuizVersus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private QuizService _quizService = new QuizService();
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            LoadFragment(item.ItemId);
            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            LoadSendedQuizes();
        }

        void LoadFragment(int id)
        {
            switch (id)
            {
                case Resource.Id.navigation_sended:
                    {
                        LoadSendedQuizes();
                        break;
                    }
                case Resource.Id.navigation_quick:
                    {
                        RunOnUiThread(async () =>
                        {
                            var res = await _quizService.SendQuickQuiz();
                            var intent = new Intent();
                            intent.SetClass(this, typeof(QuizPassingActivity));
                            intent.PutExtra("selectedQuizId", res.Id);
                            StartActivity(intent);
                        });
                        break;
                    }
                case Resource.Id.navigation_received:
                    {
                       LoadRecivedQuizes();
                        break;
                    }
            }
        }

        public void LoadSendedQuizes()
        {
            RunOnUiThread(async () =>
            {
                Android.Support.V4.App.Fragment fragment = null;
                var sendedQuizes = await _quizService.GetSended();

                fragment = new SendedQuizesFragment(sendedQuizes);

                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.fragmentContainer, fragment)
                    .Commit();
            });
        }

        public void LoadRecivedQuizes()
        {
            RunOnUiThread(async () =>
            {
                Android.Support.V4.App.Fragment fragment = null;
                var receivedQuizes = await _quizService.GetRecived();

                fragment = new ReceivedQuizesFragment(receivedQuizes);

                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.fragmentContainer, fragment)
                    .Commit();
            });
        }
    }
}

