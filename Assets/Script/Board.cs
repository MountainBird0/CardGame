using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    int[] arr;
    void Start()
    {
        arr = new int[] {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7};
        //arr= arr.OrderBy(x=>Random.Range(0f,7f)).ToArray();


        RandomArray();
        for(int i=0 ;i <16; i++){
            GameObject go = Instantiate(card, this.transform);

            float x = (i%4)*1.4f-2.1f;
            float y = (i/4)*1.4f-3.0f;
            go.transform.position = new Vector2(x,y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.carCount = arr.Length-2;
    }
    
    private void RandomArray(){
        int num;
        for (int i = 0; i < arr.Length; i++)
        {
            int random = Random.Range(0, arr.Length);
            num = arr[random];
            arr[random] = arr[i];
            arr[i] = num;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
