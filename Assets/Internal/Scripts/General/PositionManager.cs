using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace General
{
	public class PositionManager: MonoBehaviour
	{



		///  PRIVATE METHODS           ///
		private void Awake()
		{
			if (PlayerPrefs.HasKey("x"))
			{
				Vector3 pos = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
				transform.position = pos;
			}
			else
			{
				PlayerPrefs.SetFloat("x",transform.position.x);
				PlayerPrefs.SetFloat("y", transform.position.y);
				PlayerPrefs.SetFloat("z", transform.position.z);


			}
		}

		///  PUBLIC API                ///

		public void UpdateNewPos()
		{
			PlayerPrefs.SetFloat("x", transform.position.x);
			PlayerPrefs.SetFloat("y", transform.position.y);
			PlayerPrefs.SetFloat("z", transform.position.z);
		}
	}
}
