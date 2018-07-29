using System.Collections.Generic;
using Android.OS;
using Android.Views;
using QuizVersus.Adapters;
using QuizVersus.Core.Models;

namespace QuizVersus.Fragments
{
    public class ReceivedQuizesFragment : ReceivedQuizesBaseListFragment
    {
        public ReceivedQuizesFragment(List<RecivedQuizSimple> receivedQuizes)
        {
            this.receivedQuizes = receivedQuizes;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            HandleEvents();
            receivedQuizesListView.Adapter = new ReceivedQuizesListAdapter(this.Activity, receivedQuizes);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return LayoutInflater.From(Context).Inflate(Resource.Layout.ReceivedQuizesListFargment, container);
            return inflater.Inflate(Resource.Layout.ReceivedQuizesListFargment, container, false);
        }
    }
}