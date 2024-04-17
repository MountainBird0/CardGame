using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text timeTxt;
    public GameObject endPanel;

    public int carCount = 0;
    float time=30;

    public Card firstCard;
    public Card secondCard;

    AudioSource audioSource;

    public AudioClip clip;

    public bool canOpen = true;
    string key = "highScore";
    public Text score;

    public GameObject NamePanel;
    public Text NameText;
    
    public GameObject Decreasetime;
    public GameObject canvas;
    void Awake(){
        if(Instance==null){
            Instance=this;
        }
        Application.targetFrameRate=60;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time<0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        NamePanel.SetActive(false);
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }

    public void Matched(){
        canOpen=false;
        if(firstCard.idx==secondCard.idx){
            audioSource.PlayOneShot(clip);
            firstCard.DestoryCard();
            secondCard.DestoryCard();

            NamePanel.SetActive(true);
            CheckNickName();
            carCount-=2;

            if(carCount ==0)
            {
                GameOver();
                HighScoreSet();
            }
        }
        else{
            time -= 2;            
            Instantiate(Decreasetime,canvas.transform);
            NamePanel.SetActive(true);
            NameText.text = "실패";
            firstCard.CloseCard();
            secondCard.CloseCard();
            CloseText();
        }
        firstCard=null;
        secondCard=null;
    }

    private void CheckNickName()
    {
        switch(firstCard.idx){
            case 0 : NameText.text = "박민규";break;
            case 1 : NameText.text = "정래규";break;
            case 2 : NameText.text = "권신욱"; break;
            case 3 : NameText.text = "안후정"; break;
            case 4 : NameText.text = "김재휘"; break;
            case 5 : NameText.text = "매니저님"; break;//매니저님
            case 6 : NameText.text = "매니저님2"; break; //매니저님22
            case 7:
                score.text = "함정카드 발동!!"; 
                GameOver(); 
                break; 
                //함정카드
        }
        CloseText();
    }


    public void CloseText()
    {
        Invoke("CloseTextInvoke", 0.5f);
    }
    void CloseTextInvoke()
    {
        NamePanel.SetActive(false);
        canOpen = true;
    }


    private void HighScoreSet()
    {
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best > time)
            {
                PlayerPrefs.SetFloat(key, time);
                score.text = time.ToString("N2");
            }
            else
            {
                score.text = best.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            score.text = time.ToString("N2");
        }
    }
}
