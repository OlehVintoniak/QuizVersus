using System.Collections.Generic;
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
            //Todo: go to quiz passing if not passed.
        }

        protected void FindViews()
        {
            receivedQuizesListView = this.View.FindViewById<ListView>(Resource.Id.receivedQuizesListView);
        }
    }
}