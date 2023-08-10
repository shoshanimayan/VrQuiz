using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;

namespace Audio
{
	public class MusicView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private AudioLibrary _audioLibrary;

		///  PRIVATE VARIABLES         ///
		private AudioSource _source;
		///  PRIVATE METHODS           ///
		private void Awake()
		{
			_source = GetComponent<AudioSource>();
		}

		private void Start()
		{
			ChangeMusic("menu");
		}
		///  PUBLIC API                ///
		public void ChangeMusic(string clipName)
		{
			var clip=_audioLibrary.GetClip(clipName);
			if (clip)
			{
				_source.clip = clip;
				_source.Play();
			}
		}

	}
}
