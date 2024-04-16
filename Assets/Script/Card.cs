using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx =0;
    
    public GameObject front;
    public GameObject back;

    public Animator anim;
    
    public SpriteRenderer frontImage;

    AudioSource audioSource;
    public AudioClip clip; 

    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Setting(int number){
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"team{idx}");
    }
    public void OpenCard(){
        if(!GameManager.Instance.canOpen)return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if(GameManager.Instance.firstCard==null){
            GameManager.Instance.firstCard = this;
            StartCoroutine("Wait");
        }else{
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }
    public void DestoryCard(){
        Invoke("DestoryCardInvoke",0.5f);
    }

    void DestoryCardInvoke(){
        Destroy(gameObject);
    }
    public void CloseCard(){
        Invoke("CloseCardInvoke", 0.5f);
    }
    void CloseCardInvoke(){
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    IEnumerator Wait(){
        
        yield return new WaitForSeconds(5);
        if(GameManager.Instance.firstCard!=null){
            if(GameManager.Instance.firstCard.idx == this.idx){
                GameManager.Instance.firstCard = null;
                CloseCardInvoke();
            }
        }
    }
}
