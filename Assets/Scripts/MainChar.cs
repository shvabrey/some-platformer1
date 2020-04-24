using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    Rigidbody2D obj;
    public Vector2 speed = new Vector2(10, 0);
    private Vector2 movement;
    public bool isgrounded = false;
    public Vector2 jump = new Vector2(0, 10);
    public Vector2 powerOfShot = new Vector2(10, 2);
    public GameObject shot;
    public bool faceRight = true;
    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        movement = new Vector2(speed.x * inputX, obj.velocity.y);


        if (faceRight == true)
        {
            if (inputX < 0)
            {
                transform.Rotate(0, 180, 0);
                faceRight = false;
            }
        }
        if (faceRight == false)
        {
            if (inputX > 0)
            {
                transform.Rotate(0, 180, 0);
                faceRight = true;
            }
        }


        GetComponent<Rigidbody2D>().velocity = movement;
        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))
        {
            //ниже жалкая попытка рэйкаста для определения земли
            isgrounded = (Physics2D.Raycast(new Vector2(obj.transform.position.x - 0.15f, obj.transform.position.y - 0.5f), Vector2.down, 0.2f)) || (Physics2D.Raycast(new Vector2(obj.transform.position.x + 0.15f, obj.transform.position.y - 0.5f), Vector2.down, 0.2f));
            if (isgrounded == true)
            {
                obj.AddForce(jump, ForceMode2D.Impulse);
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(shot, new Vector3(transform.position.x, transform.position.y-0.3f,-1f), Quaternion.identity);
            if (faceRight == true)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(powerOfShot, ForceMode2D.Impulse);
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().transform.Rotate(0, 180, 0);
                bullet.GetComponent<Rigidbody2D>().AddForce(-powerOfShot, ForceMode2D.Impulse);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 7; i >= 0; i--)
            {
                GameObject bullet = Instantiate(shot, new Vector3(transform.position.x, transform.position.y - 0.3f, -1f), Quaternion.identity);
                if (faceRight == true)
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(8f, 10f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
                }
                else
                {
                    bullet.GetComponent<Rigidbody2D>().transform.Rotate(0, 180, 0);
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-8f, -10f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
                }
            }
        }
    }
}
