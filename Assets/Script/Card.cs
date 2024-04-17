using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
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


    public void Setting(int number){
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"team{idx}");
    }
    public void OpenCard(){
        if(!GameManager.Instance.canOpen)return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        StartCoroutine("FrontToBack");

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            StartCoroutine("Wait");
        }else{
            GameManager.Instance.secondCard = this;
            if(GameManager.Instance.firstCard == GameManager.Instance.secondCard){
                GameManager.Instance.secondCard = null;
                return;
            }
            GameManager.Instance.Matched();
        }


    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }
    
    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);

    }
    IEnumerator Wait(){
        yield return new WaitForSeconds(4.5f);
        if(!front.activeSelf){
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        if(front.activeSelf){
            GameManager.Instance.firstCard = null;
            CloseCardInvoke();
        }
    }
    IEnumerator FrontToBack(){
        yield return new WaitForSeconds(0.2f);
        front.SetActive(true);
        back.SetActive(false);
    }
}
