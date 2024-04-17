using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
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

    public GameObject NamePanel;
    public Text NameText;
    
    public GameObject Decreasetime;
    public GameObject canvas;
    void Awake(){
        if(Instance==null){
            Instance=this;
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
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if(time < 10f)
        {
            timeTxt.color = Color.red;
        }

        if(time<0)
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
            //?ååÍ¥¥Ìï¥?ùº.
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                GameOver();
                HighScoreSet();
            }
        }
        else{
            time -= 2;            
            Instantiate(Decreasetime,canvas.transform);
            NamePanel.SetActive(true);
            NameText.text = "?ã§?å®";
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
            case 0 : NameText.text = "Î∞ïÎ?ºÍ∑ú";break;
            case 1 : NameText.text = "?†ï?ûòÍ∑?";break;
            case 2 : NameText.text = "Í∂åÏã†?ö±"; break;
            case 3 : NameText.text = "?ïà?õÑ?†ï"; break;
            case 4 : NameText.text = "Íπ??û¨?úò"; break;
            case 5 : NameText.text = "Îß§Îãà????ãò"; break;//Îß§Îãà????ãò
            case 6 : NameText.text = "Îß§Îãà????ãò2"; break; //Îß§Îãà????ãò22
            case 7:
                score.text = "?ï®?†ïÏπ¥Îìú Î∞úÎèô!!"; 
                GameOver(); 
                break; 
                //?ï®?†ïÏπ¥Îìú
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
            //?ã´?ïÑ?ùº.
        }
        
        firstCard = null;
        secondCard = null;
    }
}
