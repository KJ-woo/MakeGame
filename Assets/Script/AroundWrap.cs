using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ݴ������� �����ϰ� ����� ��ũ��Ʈ

public class AroundWrap : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;

    public void UpdateAroundWrap()
    {
        //����Ƽ�� Transform Ŭ������ �ִ� position�� ������Ƽ�̹Ƿ� x, y, z ���Ϻ����� set �Ұ���
        Vector3 position = transform.position;

        //���ʳ��̳� ������ ���� �����ϸ� �ݴ������� �̵�
        if(position.x < stageData.LimitMin.x || position.x > stageData.LimitMax.x)
        {
            position.x *= -1;
        }
        //���ʳ��̳� ���� ���� �����ϸ� �ݴ������� �̵�
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
