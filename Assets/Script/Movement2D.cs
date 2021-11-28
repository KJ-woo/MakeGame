using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f; //한칸 이동하는데 걸리는 소요시간
    private bool isMove = false;   //오브젝트의 이동/대기 제어 변수

    public bool MoveTo(Vector3 moveDirection)
    {
        //이동중이면 이동함수가 실행되지 않도록함
        if (isMove)
        {
            return false;
        }

        // 현재위치로부터 이동방향으로 1 단위 이동한 위치를 매개변수로 코루틴 함수 실행함.
        StartCoroutine(SmoothGridMovement(transform.position + moveDirection));

        return true;
    }

    private IEnumerator SmoothGridMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position; //현재위치 저장
        float percent = 0;

        isMove = true; //캐릭터 이동중
        while(percent < moveTime)
        {
            percent += Time.deltaTime;
            // startPosition 에서 endPosition까지 moveTime시간동안 이동
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
        isMove = false;
        //moveTime동안 while() 반복문 호출
        // while() 반복문을 호출하는 동안 isMove = true, 반복문 종료 시 isMove = flase
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
