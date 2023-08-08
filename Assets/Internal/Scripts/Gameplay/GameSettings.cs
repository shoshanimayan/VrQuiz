using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System;
using UniRx;
using Signals;
namespace Gameplay
{
	public class GameSettings: MonoBehaviour
	{

		///  INSPECTOR VARIABLES      ///

		///  PRIVATE VARIABLES         ///

		private int _topScore=-1;

		private int _questionNumber = 10;
		private float _questionTime = 20f;

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
		///  PUBLIC API               ///

		public int GetQuestionNumber()
		{
			return _questionNumber;
		}

		public float GetQuestionTime()
		{
			return _questionTime;
		}

		///    Implementation        ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_signalBus.GetStream<SetQuestionNumberSignal>()
					   .Subscribe(x => OnQuestionNumberChanged(x.QuestionNumber)).AddTo(_disposables);
			_signalBus.GetStream<SetQuestionTimeSignal>()
					   .Subscribe(x => OnQuestionTimeChanged(x.QuestionTime)).AddTo(_disposables);

		}

		public void Dispose()
		{

			_disposables.Dispose();

		}
	}
}
