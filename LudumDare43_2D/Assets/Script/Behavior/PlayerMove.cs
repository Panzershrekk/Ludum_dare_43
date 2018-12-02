using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private float speed;
    private Rigidbody2D rb2d;
    private int health;

    void Start()
    {
        speed = GetComponent<PlayerBehavior>().stats.moveSpeed;
        health = GetComponent<PlayerBehavior>().stats.hitpoint;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (health <= 0)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(movement * 0);
            return;
        }
        

        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(movement * GetComponent<PlayerBehavior>().stats.moveSpeed);
        health = GetComponent<PlayerBehavior>().stats.hitpoint;
    }
}