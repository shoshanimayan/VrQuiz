using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System;
using UniRx;
using Signals;
namespace Gameplay
{
	public class GameSettings: MonoBehaviour
	{

		///  INSPECTOR VARIABLES      ///

		///  PRIVATE VARIABLES         ///

		private int TopScore=-1;


		private int questionNumber = 10;
		private float QuestionTime = 20f;

		///  PRIVATE METHODS          ///

		///  LISTNER METHODS          ///




		///  PUBLIC API               ///



		




		///    Implementation        ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{

			
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}
	}
}
