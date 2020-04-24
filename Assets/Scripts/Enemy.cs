using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyHP = 100;
    public GameObject Player;
    Rigidbody2D obj;

    public void GetHit(float damage)
    {
        EnemyHP -= damage;
    }

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(IsPlayerSpotted() && (transform.position.x - Player.transform.position.x) >= 1f)
        {
            obj.velocity = new Vector2(-3, obj.velocity.y);
        }
        else
        {
            obj.velocity = new Vector2(0, obj.velocity.y);
        }
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    bool IsPlayerSpotted()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
