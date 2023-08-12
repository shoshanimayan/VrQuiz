using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.UI;

namespace Gameplay
{
	public class QuizView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _canvas;
		[SerializeField] private TextMeshProUGUI _timerText;
		[SerializeField] private TextMeshProUGUI _questionText;
		[SerializeField] private TrackedDeviceGraphicRaycaster _tracked;
		[SerializeField] private GraphicRaycaster _graphic;



		///  PRIVATE VARIABLES         ///
		private QuizMediator _mediator;
		private Vector3 _origin;


		private bool _canCountDown;
		private float _timer;
		///  PRIVATE METHODS           ///
		private void Awake()
		{
			_origin = transform.localPosition;

			Side1.enabled = false;
			Side2.enabled = false;
			CorrectDisplay.enabled = false;
			IncorrectDisplay.enabled = false;
			EndDisplay.enabled = false;
		}
		///  PUBLIC API                ///


		public TextMeshProUGUI Question1;
		public TextMeshProUGUI Question2;
		public TextMeshProUGUI Question3;
		public TextMeshProUGUI Question4;

		public TextMeshProUGUI EndScore;
		public TextMeshProUGUI QuestionCount;


		public Canvas Side1;
		public Canvas Side2;

		public Canvas CorrectDisplay;
		public Canvas IncorrectDisplay;
		public Canvas EndDisplay;

		

		public void Display(bool display)
		{
			_canvas.enabled = display;
			_graphic.enabled = display;
			_tracked.enabled = display;
			if (display)
			{
				CorrectDisplay.enabled = false;
				IncorrectDisplay.enabled = false;
				transform.localPosition= _origin;

			}
			else
			{
				transform.localPosition = new Vector3(_origin.x, _origin.y + 10, _origin.z);
			}
		}

		public void SetTimer(bool enable, float time=0)
		{
			_canCountDown = enable;
			if (!enable) 
			{
				return;
			}
			_timer = time;

		}

		private void Update()
		{
			if (_canCountDown)
			{
				_timer -= Time.deltaTime;
				_timerText.text = "Time: " + ((int)_timer+1).ToString();
				if (_timer <= 0)
				{
					_mediator.PlayIncorrectAudio();
					_mediator.NextQuestion();
				}
			}
			else
			{
				_timerText.text = "";
			}

		}

		public void ReturnToMenu()
		{
			_mediator.ReturnToMenu();
		}

		public void SetQuestion(string text)
		{
			_questionText.text = text;
		}

		public void SendAnswer(TextMeshProUGUI QuestionText)
		{
			_mediator.CheckAnswer(QuestionText.text);
		}

		public void DisplayQuestion(bool enable)
		{
			_questionText.enabled = enable;
		}

		public void Initialize(QuizMediator mediator)
		{
			_mediator = mediator;
		}

		public void SetQuestionCount(string count)
		{
			QuestionCount.text = count;
		}

		
	}
}
