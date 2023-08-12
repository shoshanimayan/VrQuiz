using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Gameplay
{
	public class QuizMediator: MediatorBase<QuizView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		

		private List<Question> _questions = new List<Question>();
		private int _score = 0;
		
		///  PRIVATE METHODS           ///
		private void SetQuestionUI()
		{
			SetQuestionCount();
			_view.CorrectDisplay.enabled = false;
			_view.IncorrectDisplay.enabled = false;
			var body = _questions[0].Body;
			reshuffle(_questions[0].Options);
			var q1 = _questions[0].Options[0];
			var q2 = _questions[0].Options[1];
			var q3 = _questions[0].Options[2];
			var q4 = _questions[0].Options[3];

			_view.SetQuestion(body);
			_view.Question1.text = q1;
			_view.Question2.text = q2;
			_view.Question3.text = q3;
			_view.Question4.text = q4;

			_view.Side1.enabled = true;
			_view.Side2.enabled = true;
			_view.SetTimer(true, _gameSettings.GetQuestionTime());
			_view.DisplayQuestion(true);
		}

		private void SetQuestionCount(bool enabled=true)
		{
			if (enabled)
			{
				_view.SetQuestionCount(((_gameSettings.GetQuestionNumber() - _questions.Count)+1).ToString() + "/" + _gameSettings.GetQuestionNumber().ToString());
			}
			else
			{
				_view.SetQuestionCount("");
			}
		}

		private void SetAnswerUI(bool answer)
		{
			SetQuestionCount(false);
			_view.DisplayQuestion(false);
			_view.SetTimer(false);
			_view.Side1.enabled = false;
			_view.Side2.enabled = false;
			if (answer)
			{
				_view.CorrectDisplay.enabled = true;
			}
			else
			{
				_view.IncorrectDisplay.enabled = true;

			}
			DelayedNextQuestion(2.5f);

		}

		private void reshuffle(string[] texts)
		{
			// Knuth shuffle algorithm :: courtesy of Wikipedia :)
			for (int t = 0; t < texts.Length; t++)
			{
				string tmp = texts[t];
				int r = UnityEngine.Random.Range(t, texts.Length);
				texts[t] = texts[r];
				texts[r] = tmp;
			}
		}

		private void End()
		{
			_view.DisplayQuestion(false);
			_view.SetTimer(false);
			_view.Side1.enabled = false;
			_view.Side2.enabled = false;
			SetQuestionCount(false);
			_view.CorrectDisplay.enabled = false;
			_view.IncorrectDisplay.enabled = false;
			_signalBus.Fire(new AudioBlipSignal { clipName = "end" });
			_signalBus.Fire(new SetTopScoreSignal { Score=_score});
			_view.SetTimer(false);
			_view.EndDisplay.enabled = true;
			_view.EndScore.text = "Correct Answers:\n" + _score.ToString();
			

		}

		private async void DelayedNextQuestion(float seconds)
		{
			await Task.Delay((int)(1000*seconds));
			NextQuestion();

		}

		
		///  LISTNER METHODS           ///

		private void OnStateChanged(State state)
		{
			if (state == State.Play )
			{
				_view.EndDisplay.enabled = false;

				_score = 0;
				string quiz = _gameSettings.GetQuiz();
				JArray array = JArray.Parse(quiz);
				foreach (JObject obj in array.Children<JObject>())
				{
					string body =(string) obj["question"]["text"];
					string answer = (string)obj["correctAnswer"];
					string[] allAnswer = new string[4];
					var index = 0;
					foreach(string x in obj["incorrectAnswers"])
					{
						allAnswer[index] = x;
						index++;
					}
					allAnswer[3] = answer;
					Question q = new Question(
					body,answer,allAnswer
					);
					
					_questions.Add(q);
					
				}
				SetQuestionUI();
				_view.Display(true);

			}
			else
			{
				_questions.Clear();
				_view.Display(false);

			}
		}
		///  IMPLEMENTATION            ///
		[Inject] private SignalBus _signalBus;
		[Inject] private GameSettings _gameSettings;


		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Initialize(this);
			_view.Display(false);
			_signalBus.GetStream<StateChangedSignal>()
					   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);

		}
		///  PUBLIC API                ///


		public void Dispose()
		{

			_disposables.Dispose();

		}

		public void PlayClickAudio()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "click" });
		}

		public void PlayCorrectAudio()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "correct" });
		}
		public void PlayIncorrectAudio()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "incorrect" });
		}

		public void NextQuestion()
		{

			_questions.RemoveAt(0);
			if (_questions.Count == 0)
			{
				End();
			}
			else
			{
				SetQuestionUI();
			}
		}

		public void ReturnToMenu()
		{
			_signalBus.Fire(new StateChangeSignal { ToState = State.Menu });

		}

		public void CheckAnswer(string answer)
		{
			PlayClickAudio();
			if (answer == _questions[0].Answer)
			{
				PlayCorrectAudio();
				_score++;
				SetAnswerUI(true);
			}
			else
			{
				PlayIncorrectAudio();

				SetAnswerUI(false);
			}
		}

		public struct Question
		{

			public string Body { get; }
			public string Answer { get; }
			public string[] Options { get; }

			public Question(string body, string answer, string[] options)
			{
				Body = body;
				Answer = answer;
				Options = options;
			}

		}

	}
}
