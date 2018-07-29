using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuizVersus.Core.Services;
using QuizVersus.Fragments;

namespace QuizVersus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private TextView _textMessage;
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

            _textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_sended:
                    {
                        RunOnUiThread(async () =>
                        {
                            var sendedQuizes = await _quizService.GetSended();

                            fragment = new SendedQuizesFragment(sendedQuizes);

                            SupportFragmentManager.BeginTransaction()
                                .Replace(Resource.Id.fragmentContainer, fragment)
                                .Commit();
                        });
                        break;
                    }
                case Resource.Id.navigation_quick:
                    {
                        RunOnUiThread(async () =>
                        {
                            var res = await _quizService.SendQuickQuiz();
                        });
                        break;
                    }
                case Resource.Id.navigation_received:
                    {
                        RunOnUiThread(async () =>
                        {
                            var receivedQuizes = await _quizService.GetRecived();

                            fragment = new ReceivedQuizesFragment(receivedQuizes);
                            
                            SupportFragmentManager.BeginTransaction()
                                .Replace(Resource.Id.fragmentContainer, fragment)
                                .Commit();
                        });
                        break;
                    }
            }
        }
    }
}

