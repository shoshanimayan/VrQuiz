using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Gameplay;
namespace UI
{
	public class MainMenuMediator: MediatorBase<MainMenuView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///
		
///  LISTNER METHODS           ///
private void OnStateChanged(State state)
		{

			if (state == State.Menu)
			{
				_view.Display(true);
				_view.SetScoreUI(_gameSettings.GetTopScore());

			}
			else
			{
				_view.Display(false);

			}
		}

		///  PUBLIC API                ///

		public void InitializePlay()
		{ 
			_signalBus.Fire(new StateChangeSignal { ToState = State.Loading });
		}

		public void SetQuestionNumber(int number)
		{
			_signalBus.Fire(new SetQuestionNumberSignal { QuestionNumber = number }); 

		}

		public void SetQuestionTime(float time)
		{
			_signalBus.Fire(new SetQuestionTimeSignal { QuestionTime = time }); 

		}

		public void PlayClickAudio()
		{
			_signalBus.Fire(new AudioBlipSignal { clipName = "click"});
		}

		///  IMPLEMENTATION            ///

		[Inject]
		private SignalBus _signalBus;

		[Inject]
		private GameSettings _gameSettings;

		[Inject]
		private StateManager _stateManager;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
			_signalBus.GetStream<StateChangedSignal>()
					   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
			_signalBus.Fire(new StateChangeSignal { ToState = State.Menu });

		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
