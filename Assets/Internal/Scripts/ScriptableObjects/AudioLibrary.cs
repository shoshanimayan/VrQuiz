using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioLibrary", order = 1)]

	public class AudioLibrary : ScriptableObject
	{
		///  INSPECTOR VARIABLES       ///

		[Serializable]
		public class AudioDictionaryEntry
		{
			public string key;
			public AudioClip value;
		}

		[SerializeField]
		private List<AudioDictionaryEntry> inspectorDictionary;
		///  PRIVATE VARIABLES         ///

		private Dictionary<string, AudioClip> myDictionary;

		///  PRIVATE METHODS           ///
		private void Awake()
		{
			myDictionary = new Dictionary<string, AudioClip>();
			foreach (AudioDictionaryEntry entry in inspectorDictionary)
			{
				myDictionary.Add(entry.key, entry.value);
			}
		}

		///  PUBLIC API                ///

		public AudioClip GetClip(string key)
		{
			if (myDictionary == null)
			{
				myDictionary = new Dictionary<string, AudioClip>();
				foreach (AudioDictionaryEntry entry in inspectorDictionary)
				{
					myDictionary.Add(entry.key, entry.value);
				}
			}
			if (myDictionary.ContainsKey(key))
			{

				return myDictionary[key];
			}
			return null;
		}

	}
}
