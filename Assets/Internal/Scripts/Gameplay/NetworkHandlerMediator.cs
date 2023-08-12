using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using Signals;
namespace Gameplay
{
	public class NetworkHandlerMediator: MediatorBase<NetworkHandlerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private async void GetQuiz()
		{

			string url = string.Format("https://the-trivia-api.com/v2/questions?limit={0}", _gameSettings.GetQuestionNumber());
			var quiz = await new WebCall().ApiGet(url);

			if (quiz != null)
			{
				_gameSettings.SetQuiz(quiz);
				_signalBus.Fire(new StateChangeSignal { ToState = State.Play });

			}
			else
			{
				Debug.LogError("COULD NO ACCESS API");

			}

		}
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{

			if (state == State.Loading)
			{
				GetQuiz();

			}
			
		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject] private SignalBus _signalBus;

		[Inject] private GameSettings _gameSettings;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_signalBus.GetStream<StateChangedSignal>()
				   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
