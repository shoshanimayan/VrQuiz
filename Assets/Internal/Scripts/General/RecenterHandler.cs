using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Management;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;

namespace General
{
	public class RecenterHandler : MonoBehaviour
	{
		private void Start()
		{
			Recenter();

		}

		private void Awake()
		{
			Recenter();
		}

		private void OnEnable()
		{
			Recenter();
		}

		private void OnBecameVisible()
		{
			Recenter();

		}

		

		private bool _recentered;


		[SerializeField] private Transform _target;
		[SerializeField] private Transform _head;
		[SerializeField] private Transform _origin;

		[SerializeField] private InputActionProperty _recenterButton;

		private void Recenter()
		{
			Vector3 offset = _head.position - _origin.position;
			offset.y = 0;
			_origin.position = _target.position - offset;
			Vector3 targetForward = _target.forward;
			targetForward.y = 0;
			Vector3 cameraForwrd = _head.forward;
			cameraForwrd.y = 0;

			float angle = Vector3.SignedAngle(cameraForwrd, targetForward, Vector3.up);
			_origin.RotateAround(_head.position, Vector3.up, angle);
		}

		private void Update()
		{
			if (!_recentered)
			{
				_recentered = true;
				Recenter();
			}

			if (_recenterButton.action.WasPerformedThisFrame())
			{
				Recenter();
			}
		}

	}
}
