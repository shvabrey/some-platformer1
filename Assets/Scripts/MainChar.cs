using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    Rigidbody2D obj;
    public Vector2 speed = new Vector2(10, 0);
    private Vector2 movement;
    private bool isgrounded;
    public Vector2 jump = new Vector2(0, 10);

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        movement = new Vector2(speed.x * inputX, obj.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider = GameObject.FindWithTag("ground").GetComponent<Collider2D>())
        {
            isgrounded = true;
        } 
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
        if (Input.GetKeyDown(KeyCode.UpArrow) && isgrounded)
        {
            obj.AddForce(jump, ForceMode2D.Impulse);
        }
    }
}

