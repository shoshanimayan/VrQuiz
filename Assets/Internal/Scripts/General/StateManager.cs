using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System;
using UniRx;
using Signals;

namespace Core
{
	public enum State {Menu,Play,Loading, Error}
	public class StateManager :  IDisposable
	{

		///  INSPECTOR VARIABLES      ///

		///  PRIVATE VARIABLES         ///
		private State _state;

		///  PRIVATE METHODS          ///

		///  LISTNER METHODS          ///

		private void OnStateChanged(StateChangeSignal signal)
		{
			SetState(signal.ToState);
		}

		///  PUBLIC API               ///

		public State GetState()
		{
			return _state;
		}

		private void SetState(State state)
		{
			if (_state != state)
			{
				_state = state;
				_signalBus.Fire(new StateChangedSignal() { ToState = state });
			}
		}


		///    Implementation        ///

		readonly SignalBus _signalBus;
		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public StateManager(SignalBus signalBus)
		{

			_signalBus = signalBus;
			_state = State.Loading;
			_signalBus.GetStream<StateChangeSignal>()
					   .Subscribe(x => OnStateChanged(x)).AddTo(_disposables);


		}


		

		public void Dispose()
		{

			_disposables.Dispose();

		}


	}
}
