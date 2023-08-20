using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;

namespace UI
{
	public class ErrorUIMediator: MediatorBase<ErrorUIView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{
			if (state == State.Error)
			{

				_view.Display(true);

			}
			else
			{
				_view.Display(false);


			}
		}
		///  PUBLIC API                ///
		public void ReturnToMenu()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "click" });
			_signalBus.Fire(new StateChangeSignal { ToState = State.Menu });

		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Innit(this);
			_view.Display(false);
			_signalBus.GetStream<StateChangedSignal>()
								   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
