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
		[SerializeField] private Transform _target;
		private void Recenter()
		{
			XROrigin xROrigin = GetComponent<XROrigin>();
			xROrigin.MoveCameraToWorldLocation(_target.position);
			xROrigin.MatchOriginUpCameraForward(_target.up, _target.forward);
		}

	}
}
