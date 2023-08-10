using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;

namespace Audio
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundEffectView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private AudioLibrary _audioLibrary;
		///  PRIVATE VARIABLES         ///
		///  PRIVATE METHODS           ///
		
		///  PUBLIC API                ///
		public void PlayAudioClip(string clipName, Transform t=null)
		{
			var clip =_audioLibrary.GetClip(clipName);
			if (clip)
			{
				if (t)
				{
					AudioSource.PlayClipAtPoint(clip,t.position);
				}
				else
				{
					AudioSource.PlayClipAtPoint(clip, transform.position);

				}
			}
		}
	}
}
