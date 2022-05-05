using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

	private void Awake()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
	private void Start()
	{
		Play("MenuSound");
	}
	public void Play(string name)
	{

		Sound s = Array.Find(sounds, Sound => Sound.name == name);
		if (s == null)
		{
			Debug.LogWarning(name + " Not Found in sounds");
			return;
		}
		
			
		s.source.Play();
	}
}
