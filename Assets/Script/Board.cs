using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray(); //arr((기존에 배열에 다시 넣음) = arr(위 배열에서 쓸수있는것).OrderBy(정렬을함) >어떻게 정렬 할 것인지(x => 랜덤으로 순회를 한다 0 ~ 7f).ToArrat (배열한다)

        //for는 반복문
        for (int i = 0; i < 16; i++) //i = 0;(초기값), i < 16;(i가 16보다 작으면 괄호 안에있는 로직을 계속해서 실행함), 로직이 실행하면 i의 값을 하나씩 증가시킴
                                     //i 가 0 16보다 작은 중괄호 실행 i가 ++ 함 1이됨 여전히 16보다 작음 다시 중괄호 실행 ++하고 2가됨 여전히 16보다 작기에 16이 될때까지 반복함
        {                            //이를 이용해 i++하면 16번 실행이지만 i+=2 하면 2씩이라 절반인 8번만 실행 함
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);//카드 컴포넌트를 가져와 i로 세팅함
            //i 는 for에서 설정한것으로 인해 1씩 계속해서 늘어남
            //그렇게 15번 까지 늘어난 i를 위 아래 4칸이니 4로 나누어 나머지는 x값에 몫은 y값에 대입 그리고 해당 값에 각각 서로 카드의 거리인 1.4만큼 곱해서 배열
            // 거기에 왼쪽 아래 카드가 기준이기 때문에 화면에 드러오도록 방향을 맞추기 위해 각각 -2.1f 와 -3.0f 빼줌
        }  

        GameManager.Instance.cardCount = arr.Length;
    }

}
