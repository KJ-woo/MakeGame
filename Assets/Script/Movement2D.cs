using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f; //��ĭ �̵��ϴµ� �ɸ��� �ҿ�ð�
    private bool isMove = false;   //������Ʈ�� �̵�/��� ���� ����

    public bool MoveTo(Vector3 moveDirection)
    {
        //�̵����̸� �̵��Լ��� ������� �ʵ�����
        if (isMove)
        {
            return false;
        }

        // ������ġ�κ��� �̵��������� 1 ���� �̵��� ��ġ�� �Ű������� �ڷ�ƾ �Լ� ������.
        StartCoroutine(SmoothGridMovement(transform.position + moveDirection));

        return true;
    }

    private IEnumerator SmoothGridMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position; //������ġ ����
        float percent = 0;

        isMove = true; //ĳ���� �̵���
        while(percent < moveTime)
        {
            percent += Time.deltaTime;
            // startPosition ���� endPosition���� moveTime�ð����� �̵�
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
        isMove = false;
        //moveTime���� while() �ݺ��� ȣ��
        // while() �ݺ����� ȣ���ϴ� ���� isMove = true, �ݺ��� ���� �� isMove = flase
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
