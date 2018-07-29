
using Android.Widget;
using QuizVersus.Core.Models;
using QuizVersus.Core.Services;
using System.Collections.Generic;

namespace QuizVersus.Fragments
{
    public class SendedQuizesBaseListFragment : Android.Support.V4.App.Fragment
    {
        protected ListView sendedQuizesListView;
        protected List<SendedQuizSimple> sendedQuizes;
        protected QuizService QuizService;

        public SendedQuizesBaseListFragment()
        {
            QuizService = new QuizService();
        }
        protected void HandleEvents()
        {
            sendedQuizesListView.ItemClick += SendedQuizesListView_ItemClick;
        }

        private void SendedQuizesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedQuiz = sendedQuizes[e.Position];
            //Todo: go to quiz passing if not passed.
        }

        protected void FindViews()
        {
            sendedQuizesListView = this.View.FindViewById<ListView>(Resource.Id.sendedQuizesListView);
        }
    }
}