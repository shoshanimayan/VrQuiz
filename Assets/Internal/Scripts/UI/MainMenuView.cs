using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

namespace UI
{
	public class MainMenuView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private MainMenuMediator _mediator;
		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void Init(MainMenuMediator mediator)
		{
			_mediator = mediator;
		}


		public void Play()
		{
			_mediator.SetPlayState();
		}

		public void OnQuestionNumberChange(TMP_Dropdown index)
		{
			int val = 0;
			switch (index.value)
			{
				case 0:
					val = 10;
					break;
				case 1:
					val = 20;
					break;
				case 2:
					val = 30;
					break;
				case 3:
					val = 40;
					break;
				case 4:
					val = 50;
					break;

			}
			_mediator.SetQuestionNumber(val);
		}

		public void OnQuestionTimeChange(TMP_Dropdown index)
		{
			float val = 0;
			switch (index.value)
			{
				case 0:
					val = 20;
					break;
				case 1:
					val = 30;
					break;
				case 2:
					val = 40;
					break;
				case 3:
					val = 50;
					break;
				case 4:
					val = 60;
					break;

			}
			_mediator.SetQuestionTime(val);
		}

		public void ExitGame()
		{
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
		}
	}
}
