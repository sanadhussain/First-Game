using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public void PlayGame()
	{

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
