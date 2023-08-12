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
		
		}

		

		private void End()
		{
			_signalBus.Fire(new SetTopScoreSignal { Score=_score});
			ReturnToMenu();

		}

		private void ReturnToMenu()
		{
			_signalBus.Fire(new StateChangeSignal { ToState = State.Menu });

		}
		///  LISTNER METHODS           ///

		private void CheckAnswer(string answer)
		{
			if (answer == _questions[0].Answer)
			{

			}
			else
			{ 
			
			}
		}

		private void OnStateChanged(State state)
		{
			if (state == State.Play )
			{
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
				_view.Display(true);

			}
			else
			{
				_questions.Clear();
				_view.Display(false);

			}
		}
		///  PUBLIC API                ///
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
		///  IMPLEMENTATION            ///

		[Inject] private SignalBus _signalBus;
		[Inject] private GameSettings _gameSettings;


		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Display(false);
			_signalBus.GetStream<StateChangedSignal>()
					   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
			_signalBus.GetStream<AnswerSignal>()
					   .Subscribe(x => CheckAnswer(x.Answer)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

		public struct Question
		{

			public string Body { get; }
			public string Answer { get; }
			public string[] Wrong { get; }

			public Question(string body, string answer, string[] wrongAnswers)
			{
				Body = body;
				Answer = answer;
				Wrong = wrongAnswers;
			}

		}

	}
}
