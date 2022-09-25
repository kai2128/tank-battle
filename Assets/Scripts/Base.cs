using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;
    
    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        sr.sprite = brokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
