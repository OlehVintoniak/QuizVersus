using System.Collections.Generic;
using Android.OS;
using Android.Views;
using QuizVersus.Adapters;
using QuizVersus.Core.Models;

namespace QuizVersus.Fragments
{
    public class SendedQuizesFragment : SendedQuizesBaseListFragment
    {
        public SendedQuizesFragment(List<SendedQuizSimple> sendedQuizes)
        {
            this.sendedQuizes = sendedQuizes;
        }
       
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            HandleEvents();
            sendedQuizesListView.Adapter = new SendedQuizesListAdapter(this.Activity, sendedQuizes);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.SendedQuizesListFragment, container, false);
        }
    }
}