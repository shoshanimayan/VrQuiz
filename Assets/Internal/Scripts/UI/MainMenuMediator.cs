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
			}
			else
			{
				_view.Display(false);

			}
		}

		///  PUBLIC API                ///

		public void SetPlayState()
		{
			_signalBus.Fire(new StateChangeSignal { ToState = State.Play });
		}

		public void SetQuestionNumber(int number)
		{

			_signalBus.Fire(new SetQuestionNumberSignal { QuestionNumber = number }); 

		}

		public void SetQuestionTime(float time)
		{

			_signalBus.Fire(new SetQuestionTimeSignal { QuestionTime = time }); 

		}

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
			_signalBus.GetStream<StateChangedSignal>()
					   .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
