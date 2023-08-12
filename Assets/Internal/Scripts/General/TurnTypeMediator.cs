using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;

namespace General
{
	public class TurnTypeMediator: MediatorBase<TurnTypeView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void PlayClickAudio()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "click" });
		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Initialize(this);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
