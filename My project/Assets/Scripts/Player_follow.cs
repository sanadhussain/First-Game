
using UnityEngine;

public class Player_follow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    private Vector3 camera_follow;

	private void LateUpdate()
	{
        camera_follow.z = player.position.z;
        camera_follow.x = 0;
        camera_follow.y = 0;
        transform.position = camera_follow + offset;
    }
}
