using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;

namespace Audio
{
	
	public class SoundEffectView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private AudioLibrary _audioLibrary;
		///  PRIVATE VARIABLES         ///
		private AudioSource _audioSource;
		///  PRIVATE METHODS           ///
		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}
		///  PUBLIC API                ///
		public void PlayAudioClip(string clipName)
		{
			var clip =_audioLibrary.GetClip(clipName);
			if (clip)
			{
				_audioSource.PlayOneShot(clip);
			}
		}
	}
}
