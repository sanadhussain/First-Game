using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUi : MonoBehaviour
{
    public Text highScore;
    public Text coin;
    public Text totalDistance;
    public Text mostCoins;
    public Slider music;
    public Slider vfx;
    public ToggleGroup radio;
    private string toggleName;
    public Toggle button;
    public Toggle swipe;
    public Slider sensitivity;
    public GameObject modeUI;
    public Animator anim;
    public Text sliderText;
    
    

    public static UpdateUi instance;

    // Class to load user saved data of main menu to update UI
	private void Awake()
	{
        instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
       
        PlayerData data = SaveSystem.loadData();
		if (data == null)
		{
            return;
		}
		else
		{
            highScore.text = data.highScore.ToString();
            coin.text = data.coin.ToString();
            totalDistance.text = data.distance.ToString();
            mostCoins.text = data.mostCoins.ToString();
            music.value = data.music;
            vfx.value = data.vfx;
            sensitivity.value = data.sensitivity;
			if (data.mode == "Swipe")
			{
                swipe.isOn = true;
			}
			else if (data.mode == "Button")
			{
                button.isOn = true;
			}
            AudioManager.instance.changeMusicVolume(data.music);
            AudioManager.instance.changeVFXVolume(data.vfx);
        }
            
    }
	private void Update()
	{
		if (modeUI.activeInHierarchy)
		{
            toggleName = radio.GetFirstActiveToggle().tag;
        }
        sliderText.text = sensitivity.value.ToString();
    }

    // Method to return senstivity slider value to diffrenet classes
    public int getSensitivity()
	{
        return (int)sensitivity.value;
	}
    // Method to return Music slider value to diffrenet classes
    public float getMusic()
	{
        return music.value;
	}
    // Method to return VFX slider value to diffrenet classes
    public float getVfx()
    {
        return vfx.value;
    }
    // Method to return what mode is active to diffrenet classes
    public string getRadio()
	{
        return toggleName;
	}
}
