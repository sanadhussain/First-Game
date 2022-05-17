using UnityEngine.SceneManagement; 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	public Animator animator;
	public GameObject recordUi;
	public GameObject volumeUi;
	public GameObject modeUi;
	public GameObject buttonInfoUi;
	public GameObject swipeInfoUi;
	public void PlayGame()
	{
		recordUi.SetActive(false);
		volumeUi.SetActive(false);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		StartCoroutine(Load());


	}
	public void volumeActive()
	{
		volumeUi.SetActive(true);
		recordUi.SetActive(false);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("volume");
	}
	public void recordActive()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(true);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("record");
	}
	public void modeActive()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		modeUi.SetActive(true);
		animator.SetTrigger("mode");
	}
	public void disableAll()
	{
		recordUi.SetActive(false);
		volumeUi.SetActive(false);
		modeUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("main");
	}
	public void Exit()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(false);
		modeUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		Application.Quit();
	}
	public void buttonInfoActive()
	{
		buttonInfoUi.SetActive(true);
		swipeInfoUi.SetActive(false);
		animator.SetTrigger("button");
		
	}
	public void swipeInfoActive()
	{
		if (buttonInfoUi.activeInHierarchy)
		{
			swipeInfoUi.SetActive(true);
			buttonInfoUi.SetActive(false);
			animator.SetTrigger("swipe2");
		}
		else
		{
			swipeInfoUi.SetActive(true);
			buttonInfoUi.SetActive(false);
			animator.SetTrigger("swipe");
		}
		
	}
	public void mainMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		Time.timeScale = 1;
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void HoverSound()
	{
		FindObjectOfType<AudioManager>().Play("buttonHover");
	}
	public void ClickedSound()
	{
		FindObjectOfType<AudioManager>().Play("buttonClicked");
	}

	IEnumerator Load()
	{
		animator.SetTrigger("end");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
	
}

