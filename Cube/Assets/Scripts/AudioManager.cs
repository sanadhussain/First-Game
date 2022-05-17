using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
	public static AudioManager instance; 
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

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
	public void changeMusicVolume(float volume)
	{
		foreach (Sound s in sounds)
		{
			if (s.tag == "Music")
			{
				s.source.volume = volume;
				PlayerData data = SaveSystem.loadData();
				SaveSystem.savePlayer(data.highScore, data.coin, data.distance, data.mostCoins, volume, data.vfx, data.sensitivity, data.mode);
			}
		}
	}
	public void changeVFXVolume(float volume)
	{
		foreach (Sound s in sounds)
		{
			if (s.tag == "VFX")
			{
				s.source.volume = volume;
				PlayerData data = SaveSystem.loadData();
				SaveSystem.savePlayer(data.highScore, data.coin, data.distance, data.mostCoins, data.music, volume, data.sensitivity, data.mode);
			}
		}
	}
}
