using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;

namespace Audio
{
	public class MusicMediator: MediatorBase<MusicView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{
			if (state == State.Play)
			{
				_view.ChangeMusic("play");

			}
			else if (state == State.Menu)
			{
				_view.ChangeMusic("menu");

			}

		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

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
