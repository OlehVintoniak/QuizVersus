using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using QuizVersus.Core.Models;

namespace QuizVersus.Fragments
{
    public class ReceivedQuizesBaseListFragment : Android.Support.V4.App.Fragment
    {
        protected ListView receivedQuizesListView;
        protected List<RecivedQuizSimple> receivedQuizes;

        protected void HandleEvents()
        {
            receivedQuizesListView.ItemClick += ReceivedQuizesListViewItemClick;
        }

        private void ReceivedQuizesListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedQuiz = receivedQuizes[e.Position];
            if (!selectedQuiz.ReciverResult.HasValue)
            {
                var intent = new Intent();
                intent.SetClass(this.Activity, typeof(QuizPassingActivity));
                intent.PutExtra("selectedQuizId", selectedQuiz.Id);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this.Activity, "Quiz has beed already finished", ToastLength.Short).Show();
            }
        }

        protected void FindViews()
        {
            receivedQuizesListView = this.View.FindViewById<ListView>(Resource.Id.receivedQuizesListView);
        }
    }
}