using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoundEffect{
	public class SoundManager : MonoBehaviour{
		public static SoundManager instance;
		[SerializeField] private List<AudioClip> audioClips;
		private AudioSource _audioSource;

		private void Start(){
			_audioSource = GetComponent<AudioSource>();
			if(instance == null){
				instance = this;
			}
		}

		public void PlaySoundEffect(string effectName){
			foreach(var audioClip in from audioClip in audioClips
				let clipName = audioClip.name
				let isSame = clipName.Equals(effectName)
				where isSame
				select audioClip){
				_audioSource.PlayOneShot(audioClip);
			}
		}
	}
}