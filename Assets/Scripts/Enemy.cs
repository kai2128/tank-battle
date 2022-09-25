using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // props
    public float moveSpeed=3;
    private Vector3 bulletEulerAngles;
    public float bulletCd = 5;

    private SpriteRenderer sr;
    public Sprite[] tankSprites;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private float timer;
    private float changeDirectionTimer;
    private float v = -1;
    private float h;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Start is called before the first frame update
    void  Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= bulletCd)
        {
            if(Random.value < .5) Attack();
        }
        else
            timer += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Attack()
    {
            Instantiate(bulletPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timer =0;
    }

    private void Move()
    {
        if (changeDirectionTimer >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }else if (num==0)
            {
                v = 1;
                h = 0;
            }else if (num is > 0 and <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num is > 2 and <= 4)
            {
                h = 1;
                v = 0;
            }

            changeDirectionTimer = 0;
        }
        else
        {
            changeDirectionTimer += Time.fixedDeltaTime;
        }
        
        
        transform.Translate(Vector3.up * (v * moveSpeed * Time.fixedDeltaTime), Space.World);
        if (v < 0) // down
        {
            sr.sprite = tankSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }else if (v > 0) // up
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        
        if(v!=0)
            return;
        
        transform.Translate(Vector3.right * (h * moveSpeed * Time.fixedDeltaTime), Space.World );
        if (h < 0) // left
        {
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }else if (h > 0) // right
        {
            sr.sprite = tankSprites[3];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

    }

    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        // boom
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // die
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            changeDirectionTimer = 4;
        }
    }
}
