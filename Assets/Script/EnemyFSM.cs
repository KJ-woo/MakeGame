using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private float delayTime = 3.0f;
    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.right;
    private Direction direction = Direction.Right;
    private Direction nextDirection = Direction.None;

    private Movement2D movement2D;
    private AroundWrap aroundWrap;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        tileLayer = 1 << LayerMask.NameToLayer("Tile");

        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //�̵������� ���Ƿ� ����
        SetMoveDirectionByRandom();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        if (hit.transform == null)
        {
            //MoveTo()�Լ��� �̵������� �Ű������� ������ �̵��Ѵ�.
            movement2D.MoveTo(moveDirection);
            //ȭ������� �����ԵǸ� �ݴ����� ������.
            aroundWrap.UpdateAroundWrap();
        }
        else
        {
            SetMoveDirectionByRandom();
        }
        // MoveTo()�Լ��� �̵������� �Ű������� ������ �̵��Ѵ�.

    }

    private void SetMoveDirection(Direction direction)
    {
        //�̵� ���� ����
        this.direction = direction;
        // Vector3 Ÿ���� �̵� ���� �� ����
        moveDirection = Vector3FromEnum(this.direction);
        //�̵����⿡ ���� �̹��� ����
        spriteRenderer.sprite = images[(int)this.direction];

        //��� �ڷ�ƾ ����
        StopAllCoroutines();
        //���� �ð����� ���� �������� �̵��� ��� ������ �ٲٵ��� ��
        StartCoroutine("SetMoveDirectionByTime");
    }
    private void SetMoveDirectionByRandom()
    {
        //�̵����� ���Ƿ� ����
        direction = (Direction)Random.Range(0, (int)Direction.Count);
        //Vector3 Ÿ���� �̵����� �� ����
        //direction�� ���� �̵� ���� �����ͷ� ����� �� �����Ƿ� Vector3FromEnum() �޼ҵ忡��
        //���� ���͸� �����Ѵ�.

        SetMoveDirection(direction);
        
        //moveDirection = Vector3FromEnum(direction);
        //�̵� ���⿡ ���� �̹����� �����Ѵ�.
        //spriteRenderer.sprite = images[(int)direction];
    }

    private IEnumerator SetMoveDirectionByTime()
    {
        yield return new WaitForSeconds(delayTime);

        //���� �̵����� R or L �̸� direction%2�� 0����
        //���� ���� �̵� ����(nextDirection)�� Up(1) or Down(3)���� ������

        // ���� �̵������� Up or Down�Ͻ� direction%2�� 1��
        // ���� �̵������� Right(0) or Left(2)�� ������

        int dir = Random.Range(0, 2);
        nextDirection = (Direction)(dir * 2 + 1 - (int)direction);

        StartCoroutine("CheckBlockedNextMoveDirection");
    }
    private Vector3 Vector3FromEnum(Direction state)
    {
        Vector3 direction = Vector3.zero;

        switch (state)
        {
            case Direction.Up: direction = Vector3.up; break;
            case Direction.Left: direction = Vector3.left; break;
            case Direction.Right: direction = Vector3.right; break;
            case Direction.Down: direction = Vector3.down; break;
        }

        return direction;
    }
}
