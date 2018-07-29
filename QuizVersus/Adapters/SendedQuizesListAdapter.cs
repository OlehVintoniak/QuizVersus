using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using QuizVersus.Core.Models;

namespace QuizVersus.Adapters
{
    public class SendedQuizesListAdapter : BaseAdapter<SendedQuizSimple>
    {
        private List<SendedQuizSimple> _items;
        private Activity _context;


        public SendedQuizesListAdapter(Activity context, List<SendedQuizSimple> items) : base()
        {
            _items = items;
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override int Count => _items.Count;
        public override SendedQuizSimple this[int position] => _items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            if (convertView == null)
            {
                convertView
                    = _context.LayoutInflater.Inflate(Resource.Layout.SendedQuizesRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.receiverNameTextView).Text = item.ReciverFullName;
            convertView.FindViewById<TextView>(Resource.Id.quizesCount).Text = item.QuestionCount.ToString();
            return convertView;
        }
    }
}