using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System;
using UniRx;
using Signals;
using Core;

namespace Gameplay
{
	public class GameSettings:  IDisposable
	{

		///  INSPECTOR VARIABLES      ///

		///  PRIVATE VARIABLES         ///

		private int _topScore=-1;

		private int _questionNumber = 10;
		private float _questionTime = 20f;

		private string _quiz;
		///  PRIVATE METHODS          ///





		///  LISTNER METHODS          ///
		private void OnQuestionNumberChanged(int number)
		{
			_questionNumber = number;
		}

		private void OnQuestionTimeChanged(float time)
		{
			_questionTime = time;
		}

		private void OnTopScoreChanged(int score)
		{

			if (score > _topScore)
			{
				PlayerPrefs.SetInt("topScore", score);

				_topScore = score;
			}
		}
		///  PUBLIC API               ///

		public int GetQuestionNumber()
		{
			return _questionNumber;
		}

		public float GetQuestionTime()
		{
			return _questionTime;
		}

		public int GetTopScore()
		{
			return _topScore;
		}

		public void SetQuiz(string quiz)
		{
			_quiz = quiz;
			_signalBus.Fire(new StateChangeSignal { ToState = State.Play });

		}

		public string GetQuiz()
		{
			return _quiz;

		}



		///    Implementation        ///



		readonly SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public GameSettings(SignalBus signalBus)
		{
			_signalBus = signalBus;
			if (PlayerPrefs.HasKey("topScore"))
			{
				int n = PlayerPrefs.GetInt("topScore");
				_topScore = n;

			}
			else
			{
				PlayerPrefs.SetInt("topScore", 0);


			}

			_signalBus.GetStream<SetQuestionNumberSignal>()
					   .Subscribe(x => OnQuestionNumberChanged(x.QuestionNumber)).AddTo(_disposables);
			_signalBus.GetStream<SetQuestionTimeSignal>()
					   .Subscribe(x => OnQuestionTimeChanged(x.QuestionTime)).AddTo(_disposables);
			_signalBus.GetStream<SetTopScoreSignal>()
					   .Subscribe(x => OnTopScoreChanged(x.Score)).AddTo(_disposables);
		}

		

		public void Dispose()
		{

			_disposables.Dispose();

		}
	}
}
