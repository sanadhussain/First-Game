using UnityEngine.SceneManagement; 
using UnityEngine;
using System.Collections;
using TMPro;
// Script to handel truning on or off of panels in ui
public class MenuScript : MonoBehaviour
{
	public Animator animator;
	public GameObject recordUi;
	public GameObject volumeUi;
	public GameObject modeUi;
	public GameObject buttonInfoUi;
	public GameObject swipeInfoUi;
	public GameObject PauseScreen;
	public TextMeshProUGUI wait;
	public GameObject timeCounter;
	public int countDownTime;
	// Implementation of what happens when user presses play btn in main menu
	public void PlayGame()
	{
		recordUi.SetActive(false);
		volumeUi.SetActive(false);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		StartCoroutine(Load());


	}
	// Implementatin of boost btn in game
	public void boost()
	{
		PlayerMovement.instance.isBoosted = true;
		StartCoroutine(PlayerMovement.instance.spendBoost());
	}

	// Implementation of what happens when user presses Volume btn in main menu
	public void volumeActive()
	{
		volumeUi.SetActive(true);
		recordUi.SetActive(false);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("volume");
	}
	// Implementation of what happens when user presses record btn in main menu
	public void recordActive()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(true);
		modeUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("record");
	}
	// Implementation of what happens when user presses mode btn in main menu
	public void modeActive()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		modeUi.SetActive(true);
		animator.SetTrigger("mode");
	}
	// Implementation of what happens when user presses anywhere else on display screen in main menu
	public void disableAll()
	{
		recordUi.SetActive(false);
		volumeUi.SetActive(false);
		modeUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		animator.SetTrigger("main");
	}
	// Implementation of what happens when user presses exit btn in main menu
	public void Exit()
	{
		volumeUi.SetActive(false);
		recordUi.SetActive(false);
		modeUi.SetActive(false);
		buttonInfoUi.SetActive(false);
		swipeInfoUi.SetActive(false);
		Application.Quit();
	}
	// Implementation of what happens when user presses info btn in main menu -> mode -> btninfo
	public void buttonInfoActive()
	{
		buttonInfoUi.SetActive(true);
		swipeInfoUi.SetActive(false);
		animator.SetTrigger("button");
		
	}
	// Implementation of what happens when user presses play btn in pause menu
	public void Play()
	{
		countDownTime = 3;
		timeCounter.SetActive(true);
		PauseScreen.SetActive(false);
		StartCoroutine(gap());
		

	}
	// count down timer 
	IEnumerator gap()
	{
		while (countDownTime > 0)
		{
			wait.text = countDownTime.ToString();
			yield return new WaitForSecondsRealtime(1);
			countDownTime--;
		}
		
		timeCounter.SetActive(false);
		Time.timeScale = 1;
		
		
		
	}

	// Implementation of what happens when user presses pause btn in playing screen
	public void Pause()
	{
		PauseScreen.SetActive(true);
		Time.timeScale = 0;
		PlayerMovement.instance.play = false;
		
	}
	// Implementation of what happens when user presses info btn in main menu -> mode -> SwipeInfo
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
	// Implementation of what happends when user presses main menu btn in pause menu
	public void mainMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		Time.timeScale = 1;
	}
	// Implementation of what happends when user presses restart btn in pause menu
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	// Implementation of playing sounds when hovered over any btn
	public void HoverSound()
	{
		FindObjectOfType<AudioManager>().Play("buttonHover");
	}
	// Implementation of playing sounds when any btn is clicked
	public void ClickedSound()
	{
		FindObjectOfType<AudioManager>().Play("buttonClicked");
	}
	// Loading next scene
	IEnumerator Load()
	{
		animator.SetTrigger("end");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
	
}

