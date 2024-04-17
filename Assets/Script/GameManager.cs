using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;
    public GameObject GameClear;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip fail;
    public AudioClip gameover;
    public AudioClip clear;

    public int cardCount = 0;
    float time = 0.0f;
    float endtime = 30.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > endtime)
        {
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);
            audioSource.PlayOneShot(gameover);
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            //파괴해라.
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                GameClear.SetActive(true);
                Time.timeScale = 0.0f;
                audioSource.PlayOneShot(clear);
            }
        }
        else
        {
            audioSource.PlayOneShot(fail);
            firstCard.CloseCard();
            secondCard.CloseCard();
            //닫아라.
        }
        
        firstCard = null;
        secondCard = null;
    }
}
