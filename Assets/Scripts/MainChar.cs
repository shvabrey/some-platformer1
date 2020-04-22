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

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
        if (Input.GetKeyDown(KeyCode.UpArrow) && isgrounded)
        {
            bool grounded = (Physics2D.Raycast(obj.transform.position, Vector3.down, 1f, LayerMask.NameToLayer("ground"))); // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer
            Debug.DrawRay((new Vector3(obj.transform.position.x, obj.transform.position.y + 1f, obj.transform.position.z)), Vector3.down, Color.green, 5);
            if (grounded == true)
            {
                print("grounded!");
                obj.AddForce(jump, ForceMode2D.Impulse);
            }
        }
    }
}

