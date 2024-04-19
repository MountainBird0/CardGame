using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DecreaseTime : MonoBehaviour
{
    Rigidbody2D rigid;
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("wait");
    }
    void Update(){
        transform.Translate(new Vector3(1,1,0)*4);
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
