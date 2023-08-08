using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System;
using UniRx;
using Signals;

namespace Core
{
	public enum State {Menu,Play}
	public class StateManager : IstateManager
	{



		///  INSPECTOR VARIABLES      ///

		///  PRIVATE VARIABLES         ///
		private State _state;

		///  PRIVATE METHODS          ///

		///  LISTNER METHODS          ///

		


		///  PUBLIC API               ///
		
		

		public State GetState()
		{
			return _state;
		}

		private void SetState(State state)
		{
			_state = state;
			_signalBus.Fire(new StateChangedSignal() { ToState = state });
			
		}

		private void OnStateChanged(StateChangeSignal signal)
		{
			SetState(signal.ToState);
		}




		///    Implementation        ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			
			_signalBus.GetStream<StateChangeSignal>()
					   .Subscribe(x => OnStateChanged(x)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}


	}
}
