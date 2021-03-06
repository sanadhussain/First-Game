using UnityEngine;

// class of user defined data type for storing sounds in project.
[System.Serializable]
public class Sound
{
	public string name;
	public string tag;
	public AudioClip clip;
	public bool loop;

	[Range(0f, 1f)]
	public float volume;

	[Range(0.1f, 3f)]
	public float pitch;

	[HideInInspector]
	public AudioSource source;

}
