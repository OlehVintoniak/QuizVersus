using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using QuizVersus.Core.Models;
using QuizVersus.Core.Models.Quiz;
using QuizVersus.Core.Services;

namespace QuizVersus
{
    [Activity(Label = "QuizPassingActivity")]
    public class QuizPassingActivity : Activity
    {
        private TextView senderNameTextView;
        private TextView receiverNameTextView;
        private TextView questionNumberTextView;
        private TextView questionTextTextView;
        private RadioButton radioAnswer1;
        private RadioButton radioAnswer2;
        private RadioButton radioAnswer3;
        private RadioButton radioAnswer4;
        private Button nextButton;
        private RadioGroup radioGroup;

        private EntireQuiz selectedQuiz;
        private CommitedQuiz commitedQuiz = new CommitedQuiz();
        private QuizService _quizService = new QuizService();
        private int _currentQuestionIndex = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizPassingActivity);
            RunOnUiThread(async () =>
                {
                    FindViews();
                    selectedQuiz = await _quizService.GetEntireQuizById(Intent.Extras.GetInt("selectedQuizId"));
                    if (selectedQuiz != null)
                    {
                        BindData();
                        HandleEvents();
                    }
                });
        }

        private void FindViews()
        {
            senderNameTextView = FindViewById<TextView>(Resource.Id.senderNameTextView);
            receiverNameTextView = FindViewById<TextView>(Resource.Id.receiverNameTextView);
            questionNumberTextView = FindViewById<TextView>(Resource.Id.questionNumberTextView);
            questionTextTextView = FindViewById<TextView>(Resource.Id.questionTextTextView);
            radioAnswer1 = FindViewById<RadioButton>(Resource.Id.radio_answer1);
            radioAnswer2 = FindViewById<RadioButton>(Resource.Id.radio_answer2);
            radioAnswer3 = FindViewById<RadioButton>(Resource.Id.radio_answer3);
            radioAnswer4 = FindViewById<RadioButton>(Resource.Id.radio_answer4);
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            nextButton = FindViewById<Button>(Resource.Id.nextButton);
        }

        private void BindData()
        {
            senderNameTextView.Text = selectedQuiz.SenderFullName;
            receiverNameTextView.Text = selectedQuiz.ReciverFullName;
            questionNumberTextView.Text = $"{_currentQuestionIndex + 1}/{selectedQuiz.Questions.Count().ToString()}";
            questionTextTextView.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Text;
            radioAnswer1.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer1;
            radioAnswer2.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer2;
            radioAnswer3.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer3;
            radioAnswer4.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer4;
            commitedQuiz.Id = selectedQuiz.Id;
        }

        private void HandleEvents()
        {
            nextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, System.EventArgs e)
        {
            var checkedButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            checkedButton.Checked = false;

            commitedQuiz.Questions.Add(new CommitedQuestion
            {
                Id = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Id,
                CheckedAnswer = GetAnswerIndexByRadioButtonId(checkedButton.Id)
            });

            _currentQuestionIndex++;
            questionNumberTextView.Text = $"{_currentQuestionIndex + 1}/{selectedQuiz.Questions.Count().ToString()}";
            questionTextTextView.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Text;
            radioAnswer1.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer1;
            radioAnswer2.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer2;
            radioAnswer3.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer3;
            radioAnswer4.Text = selectedQuiz.Questions.ToList()[_currentQuestionIndex].Answer4;
            if (_currentQuestionIndex == selectedQuiz.Questions.ToList().Count - 1)
            {
                nextButton.Text = "Finish the quiz";
                nextButton.Click -= NextButton_Click;
                nextButton.Click += CommitQuiz;
            }
        }

        private void CommitQuiz(object sender, EventArgs a)
        {
            RunOnUiThread(async () =>
            {
                var res = await _quizService.ReceiverCommit(commitedQuiz);
                if (res)
                {
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Server error =(", ToastLength.Short).Show();
                }
            });
        }

        private int GetAnswerIndexByRadioButtonId(int radioButtonId)
        {
            switch (radioButtonId)
            {
                case Resource.Id.radio_answer1: return 1;
                case Resource.Id.radio_answer2: return 2;
                case Resource.Id.radio_answer3: return 3;
                case Resource.Id.radio_answer4: return 4;
            }
            return -1;
        }
    }
}