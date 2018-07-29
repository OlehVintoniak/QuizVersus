using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using QuizVersus.Core.Models;

namespace QuizVersus.Adapters
{
    public class ReceivedQuizesListAdapter : BaseAdapter<RecivedQuizSimple>
    {
        private List<RecivedQuizSimple> _items;
        private Activity _context;


        public ReceivedQuizesListAdapter(Activity context, List<RecivedQuizSimple> items) : base()
        {
            _items = items;
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override int Count => _items.Count;
        public override RecivedQuizSimple this[int position] => _items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            if (convertView == null)
            {
                convertView
                    = _context.LayoutInflater.Inflate(Resource.Layout.ReceivedQuizesRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.senderNameTextView).Text = item.SenderFullName;
            convertView.FindViewById<TextView>(Resource.Id.quizesCount).Text = item.QuestionCount.ToString();
            return convertView;
        }
    }
}