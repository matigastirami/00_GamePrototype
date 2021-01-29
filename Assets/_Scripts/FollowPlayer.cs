using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3 { x = 0, y = 5, z = -6 };
    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }
}
