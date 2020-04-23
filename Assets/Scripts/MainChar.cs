using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    Rigidbody2D obj;
    public Vector2 speed = new Vector2(10, 0);
    private Vector2 movement;
    public bool isgrounded;
    public Vector2 jump = new Vector2(0, 10);
    public Vector2 powerOfShot = new Vector2(10, 10);
    public GameObject shot;
    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        movement = new Vector2(speed.x * inputX, obj.velocity.y);
        //ниже жалкая попытка рэйкаста для определения земли
        isgrounded = (Physics2D.Raycast(new Vector2(obj.transform.position.x - 0.5f, obj.transform.position.y - 0.5f), Vector2.down, 0.1f)) || (Physics2D.Raycast(new Vector2(obj.transform.position.x + 0.5f, obj.transform.position.y - 0.5f), Vector2.down, 0.1f));
    }

    void FixedUpdate()
    {
        if (isgrounded == true)
        {
            GetComponent<Rigidbody2D>().velocity = movement;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                obj.AddForce(jump, ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(shot);
            bullet.GetComponent<Rigidbody2D>().AddForce(powerOfShot, ForceMode2D.Impulse);
        }
    }
}
