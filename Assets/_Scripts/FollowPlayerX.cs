using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private Vector3 offset = new Vector3 { x = 0, y = 5, z = -10 };

    private Vector3 playerPrevPos, playerMoveDir;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = offset.magnitude;
        playerPrevPos = player.transform.position;
        
        transform.position = player.transform.position - playerMoveDir * distance;

        transform.position += offset.normalized;
    }

    void LateUpdate()
    {
        playerMoveDir = player.transform.position - playerPrevPos;

        if (playerMoveDir != Vector3.zero)
        {
            playerMoveDir.Normalize();
            
            transform.position = player.transform.position - playerMoveDir * distance;

            transform.position += offset.normalized;

            transform.LookAt(player.transform.position);

            playerPrevPos = player.transform.position;
        }
    }
}
