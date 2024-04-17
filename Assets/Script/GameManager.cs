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
    public GameObject ClearPanel;

    float time=300f;

    public Card firstCard;
    public Card secondCard;

    public GameObject endTxt;
    public GameObject ClearTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip fail;
    public AudioClip gameover;
    public AudioClip clear;
    public AudioClip no;
    public AudioClip CardBGM;

    public int cardCount = 0;

    public GameObject NamePanel;
    public Text NameText;
    
    public GameObject Decreasetime;
    public GameObject canvas;

    public bool canOpen = true;
    string key = "highScore";
    public Text score;

    void Awake(){
        if(Instance==null){
            Instance=this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Setting();
    }

    private void Setting()
    {
        audioSource = GetComponent<AudioSource>();
        timeTxt = GameObject.Find("TimeTxt").GetComponent<Text>();
        endPanel = GameObject.Find("EndPanel");
        endTxt = GameObject.Find("EndTxt");
        score = GameObject.Find("Score").GetComponent<Text>();
        endPanel.SetActive(false);
        ClearPanel = GameObject.Find("ClearPanel");
        ClearTxt = GameObject.Find("ClearTxt");
        ClearPanel.SetActive(false);
        NamePanel = GameObject.Find("NamePanel");
        NameText = GameObject.Find("NameTxt").GetComponent<Text>();
        NamePanel.SetActive(false);
        canvas = GameObject.Find("Canvas");
        
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
            GameOver();
        }
    }

    public void Matched()
    {
        canOpen = false;
        if (firstCard.idx == secondCard.idx)
        {
            //?��괴해?��.
            audioSource.PlayOneShot(clip);
            if(cardCount>4){
                firstCard.DestroyCard();
                secondCard.DestroyCard();
            }else{
                firstCard.DestroyCardInvoke();
                secondCard.DestroyCardInvoke();
            }

            NamePanel.SetActive(true);
            CheckNickName();
            cardCount -= 2;

            if(cardCount == 0)
            {
                GameClear();
            }
        }
        else{
            time -= 2;
            audioSource.PlayOneShot(fail);
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

    private void GameOver()
    {
        NamePanel.SetActive(false);
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        audioSource.PlayOneShot(gameover);
    }

    private void GameOver2()
    {
        NamePanel.SetActive(false);
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }

    private void GameClear()
    {
        NamePanel.SetActive(false);
        Time.timeScale = 0f;
        ClearPanel.SetActive(true);
        audioSource.PlayOneShot(clear);

        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best < time)
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

        //GameClear.SetActive(true);
        audioSource.PlayOneShot(clear);

        NamePanel.SetActive(false);
        ClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    private void CheckNickName()
    {
        switch (firstCard.idx)
        {
            case 0: NameText.text = "박민규"; break;
            case 1: NameText.text = "정래규"; break;
            case 2: NameText.text = "권신욱"; break;
            case 3: NameText.text = "안후정"; break;
            case 4: NameText.text = "김재휘"; break;
            case 5: NameText.text = "매니저님"; break;//매니저님
            case 6: NameText.text = "매니저님2"; break; //매니저님22
            case 7:
                score.text = "함정카드 발동!!";
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                GameOver2();
                audioSource.PlayOneShot(no);
                audioSource.PlayOneShot(CardBGM);
                if (audioManager != null)
                {
                    audioManager.StopMusicPlay();
                }
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

}
