
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_follow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float threshold;
    private int score = 600;
    


    private Vector3 camera_follow;

	private void LateUpdate()
	{
        Vector3 camerapositon = transform.position;
        camerapositon.x = 0f;
        camerapositon.y = 0f;
      
		if (camerapositon.magnitude > threshold)
		{
            camerapositon.z = (int)transform.position.z;
			foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
			{
                
                g.transform.position -= camerapositon;
                
			}
            PlayerMovement.instance.setSpawn(450f);
            PlayerMovement.instance.setGroundSpawn(200f);
            PlayerMovement.instance.setScore(score);
            score += 600;
        }
        camera_follow.z = player.transform.position.z;
        camera_follow.x = player.transform.position.x;
        camera_follow.y = 0f;
        transform.position = camera_follow + offset;
    }
}
