using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // Array to store all sounds
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
	// To play sounds from different scripts
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
	// To change volume of specific tag through main menu slider and storing it in file
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
	// To change volume of specific tag through main menu slider and storing it in file
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
