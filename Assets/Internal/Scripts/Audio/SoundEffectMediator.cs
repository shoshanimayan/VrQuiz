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
	public class SoundEffectMediator: MediatorBase<SoundEffectView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private AudioSource _source;
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		public void PlayAudio(AudioBlipSignal signal)
		{
			_view.PlayAudioClip(signal.clipName);

		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			
			_signalBus.GetStream<AudioBlipSignal>()
					   .Subscribe(x => PlayAudio(x)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
