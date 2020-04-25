using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyHP = 100;
    public GameObject Player;
    public float AgrTime;
    public float AgrDistance = 5f;
    Rigidbody2D obj;
    bool faceRight = false;

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
    }

    public void GetHit(float damage)
    {
        //смотрим откуда стреляли
        if (DistanceToPlayer() > 0 && !IsPlayerSpotted() && faceRight == true)
        {
            FlipX();
        }
        else if (DistanceToPlayer() < 0 && !IsPlayerSpotted() && faceRight == false)
        {
            FlipX();
        }
        AgrDistance = 20f;
        //получение урона
        EnemyHP -= damage;
    }
    private bool Way()
    {
        RaycastHit2D hitRight;
        RaycastHit2D hitLeft;
        hitRight = Physics2D.Raycast(transform.position, new Vector2(1f, -1f), 1f);
        hitLeft = Physics2D.Raycast(transform.position, new Vector2(-1f, -1f), 1f);
        Debug.DrawRay(transform.position, new Vector3(1f, -1f));
        Debug.DrawRay(transform.position, new Vector3(-1f, -1f));
        if (faceRight == false && hitLeft.collider != null && hitLeft.collider.gameObject.tag == "ground")
        {
            return true;
        }
        if (faceRight == true && hitRight.collider != null && hitRight.collider.gameObject.tag == "ground")
        {
            return true;
        }
        return false;
    }

    float DistanceToPlayer()
    {
        return transform.position.x - Player.transform.position.x;
    }

    void FlipX()
    {
        transform.Rotate(0, 180, 0);
        faceRight = !faceRight;
    }

    //ходьба
    void Go()
    {
        obj.velocity = new Vector2(0, obj.velocity.y);
        if (IsPlayerSpotted() && DistanceToPlayer() >= 1f)
        {
            if (faceRight == true)
            {
                FlipX();
            }
            if (Way())
            {
                obj.velocity = new Vector2(-3, obj.velocity.y);
            }
        }
        else if (IsPlayerSpotted() && DistanceToPlayer() <= -1f)
        {
            if (faceRight == false)
            {
                FlipX();
            }
            if (Way())
            {
                obj.velocity = new Vector2(3, obj.velocity.y);
            }
        }
    }

    bool IsPlayerSpotted()
    {
        RaycastHit2D hit;
        if (faceRight == false)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, AgrDistance);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, AgrDistance);
        }
        if ((hit.collider != null && hit.collider.gameObject.tag == "Player"))
        {
            AgrTime = 5f;
            return true;
        }
        else
        {
            AgrTime -= Time.deltaTime;
            if (AgrTime <= 0 || Mathf.Abs(DistanceToPlayer()) > 10f)
            {
                AgrDistance = 5f;
                return false;
            }
            return true;
        }
    }

    void Update()
    {
        Go();
        //урон
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
        //патрулирование будет здесь

    }
}
