using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;

namespace Gameplay
{
	public class QuizMediator: MediatorBase<QuizView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///


		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{
			if (state == State.Loading)
			{
				_view.Display(true);

			}
			else
			{
				_view.Display(false);

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
