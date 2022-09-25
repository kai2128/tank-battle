using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;

    public bool isAllyBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * (moveSpeed * Time.deltaTime),Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Tank":
                if (!isAllyBullet)
                {
                    col.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Base":
                col.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isAllyBullet)
                {
                    col.SendMessage("Die");
                    Destroy(gameObject);
                }               
                break;
            case "Wall":
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if(isAllyBullet)
                    col.SendMessage("PlayAudio");
                Destroy(gameObject);
                break;
        }
    }
}

