using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace UI
{
	public class MainMenuView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _canvas;
		[SerializeField] private TrackedDeviceGraphicRaycaster _tracked;
		[SerializeField] private GraphicRaycaster _graphic;

		[SerializeField] private TMP_Dropdown  _numberDropdown;
		[SerializeField] private TMP_Dropdown _timeDropdown;
		[SerializeField] private TextMeshProUGUI _ScoreText;

		///  PRIVATE VARIABLES         ///
		private MainMenuMediator _mediator;
		private Vector3 _origin;
		///  PRIVATE METHODS           ///
		private void Awake()
		{
			_origin = transform.localPosition;
		}
		///  PUBLIC API                ///
		public void Init(MainMenuMediator mediator)
		{
			_mediator = mediator;

			if (PlayerPrefs.HasKey("number"))
			{
				int n = PlayerPrefs.GetInt("number");

				_numberDropdown.value = n;
				int val = 0;
				switch (n)
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
			else
			{
				PlayerPrefs.SetInt("number", 0);


			}

			if (PlayerPrefs.HasKey("time"))
			{
				int n = PlayerPrefs.GetInt("time");
				_timeDropdown.value = n;
				float val = 0;
				switch (n)
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
			else
			{
				PlayerPrefs.SetInt("time", 0);


			}
		}

		public void SetScoreUI(int score)
		{
			_ScoreText.text = score.ToString();
		}


		public void Display(bool display)
		{
			_canvas.enabled = display;
			_graphic.enabled = display;
			_tracked.enabled = display;
			if (display)
			{
				transform.localPosition = _origin;
			}
			else
			{
				transform.localPosition = new Vector3(_origin.x, _origin.y + 10, _origin.z);
			}
		}

		public void Play()
		{
			_mediator.PlayClickAudio();
			_mediator.InitializePlay();
		}


		

		public void OnQuestionNumberChange(TMP_Dropdown index)
		{
			PlayerPrefs.SetInt("number", index.value);

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
			_mediator.PlayClickAudio();

		}

		public void OnQuestionTimeChange(TMP_Dropdown index)
		{
			PlayerPrefs.SetInt("time", index.value);

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
			_mediator.PlayClickAudio();

		}

		public void ExitGame()
		{
			_mediator.PlayClickAudio();
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
		}
	}
}
