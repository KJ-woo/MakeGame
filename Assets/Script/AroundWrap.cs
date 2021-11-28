using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//반대편으로 등장하게 만드는 스크립트

public class AroundWrap : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;

    public void UpdateAroundWrap()
    {
        //유니티의 Transform 클래스에 있는 position은 프로퍼티이므로 x, y, z 단일변수만 set 불가능
        Vector3 position = transform.position;

        //왼쪽끝이나 오른쪽 끝에 도달하면 반대편으로 이동
        if(position.x < stageData.LimitMin.x || position.x > stageData.LimitMax.x)
        {
            position.x *= -1;
        }
        //위쪽끝이나 밑쪽 끝에 도달하면 반대편으로 이동
        if (position.y < stageData.LimitMin.y || position.y > stageData.LimitMax.y)
        {
            position.y *= -1;
        }
        transform.position = position;
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
