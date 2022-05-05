using UnityEngine.SceneManagement; 
using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
	public Animator animator;

	public void PlayGame()
	{

		StartCoroutine(Load());


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

