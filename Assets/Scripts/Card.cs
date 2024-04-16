using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0; //아래 넘버 로 세팅된 함수 숫자를 가져와서 idx에 적용함

    public GameObject front;
    public GameObject back;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number; //보드에서 겟컴포넌트로 세팅된 i를 가져옴(숫자)
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");//프론트 스프라이트 = 스프라이트 리소스를 로드함 (이름은 르탄(idx)<idx로 숫자를 정함
    }

    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip); //Oneshot 오디오가 겹치지않음
        anim.SetBool("ifOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        //fistCard가 비었다면,
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            //fistCard에 내 정보를 넘겨준다.
        }
        //firstCard가 비어있지 않다면,
        else
        {
            GameManager.Instance.secondCard = this;
            //secondCaed에 내정보를 넘겨준다.
            GameManager.Instance.Matched();
            //Marched 함수를 호출해 준다.
        }

    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }
    
    void CloseCardInvoke()
    {
        anim.SetBool("ifOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
