using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Gameplay;

namespace Network
{
	public class NetworkMediator: MediatorBase<NetworkView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{
			if (state == State.Loading)
			{
				

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
