using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (player)
            transform.position = player.transform.position + new Vector3(0, 0, -25);

    }
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //transform.position = player.transform.position;
    }
}