using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace UI
{
	public class ErrorUIView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _canvas;
		///  PRIVATE VARIABLES         ///
		private Vector3 _origin;
		private ErrorUIMediator _mediator;

		///  PRIVATE METHODS           ///
		private void Awake()
		{
			_origin = transform.localPosition;

			
		}
		///  PUBLIC API                ///
		public void Display(bool display)
		{
			_canvas.enabled = display;
			
			if (display)
			{
				
				transform.localPosition = _origin;

			}
			else
			{
				transform.localPosition = new Vector3(_origin.x, _origin.y + 10, _origin.z);
			}
		}

		public void Innit(ErrorUIMediator mediator)
		{
			_mediator = mediator;
		}

		public void ReturnToMenu()
		{ 
			
		}
	}
}
