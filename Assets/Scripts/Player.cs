using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // props
    public float moveSpeed=3;
    private Vector3 bulletEulerAngles;
    private float timer = 0;
    public float bulletCd = 0.3f;
    public float immortalTime = 3;
    public bool immortal = true;

    private SpriteRenderer sr;
    public Sprite[] tankSprites;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject immortalEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudios;


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
        if (PlayerManager.Instance.isDefeat) return;
        if (immortal)
        {
            immortalEffectPrefab.SetActive(true);
            immortalTime -= Time.deltaTime;
            if (immortalTime <= 0)
            {
                immortal = false;
                immortalEffectPrefab.SetActive(false);
            }   
        }

        if (timer >= bulletCd)
        {
            Attack();
        }
        else
            timer += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat) return;   
        Move();
    }

    private void Attack()
    {
        if (!Input.GetKey(KeyCode.Space)) return;
        Instantiate(bulletPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
        timer = 0;
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
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

        if (Math.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudios[1];
            if(!moveAudio.isPlaying) moveAudio.Play();
        }
        
        if(v!=0)
            return;
        
        float h = Input.GetAxisRaw("Horizontal");
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
        
        if (Math.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudios[1];
            if(!moveAudio.isPlaying) moveAudio.Play();
        }
        else
        {
            moveAudio.clip = tankAudios[0];
            if(!moveAudio.isPlaying) moveAudio.Play();
        }

    }

    private void Die()
    {
        if(immortal)
            return;
        PlayerManager.Instance.isDead = true;
        // boom
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // die
        Destroy(gameObject);
    }
}
