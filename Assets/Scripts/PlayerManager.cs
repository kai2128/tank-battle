using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // player props
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead = false;
    public bool isDefeat = false;

    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeText;
    public GameObject isDefeatUI;
    
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Recover()
    {
        if (lifeValue < 0)
        {
            // game over, back to main menu
            isDefeat = true;
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke(nameof(ReturnToMainMenu), 3);
            return;
        }
        if(isDead)
            Recover();
        playerScoreText.text = playerScore.ToString();
        playerLifeText.text = lifeValue.ToString();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
