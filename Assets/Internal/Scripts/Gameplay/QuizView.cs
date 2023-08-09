using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Gameplay
{
	public class QuizView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _canvas;
		[SerializeField] private TextMeshProUGUI _timerText;
		[SerializeField] private TextMeshProUGUI _questionText;

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void Display(bool display)
		{
			_canvas.enabled = display;
		}

		public void SetTimer(string text)
		{

			_timerText.text = text;
		}

		public void SetQuestion(string text)
		{
			_questionText.text = text;
		}
	}
}