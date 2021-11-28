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
        //이동방향을 임의로 설정
        SetMoveDirectionByRandom();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        if (hit.transform == null)
        {
            //MoveTo()함수에 이동방향을 매개변수로 전달해 이동한다.
            movement2D.MoveTo(moveDirection);
            //화면밖으로 나가게되면 반대편에서 등장함.
            aroundWrap.UpdateAroundWrap();
        }
        else
        {
            SetMoveDirectionByRandom();
        }
        // MoveTo()함수에 이동방향을 매개변수로 전달해 이동한다.

    }

    private void SetMoveDirection(Direction direction)
    {
        //이동 방향 설정
        this.direction = direction;
        // Vector3 타입의 이동 방향 값 설정
        moveDirection = Vector3FromEnum(this.direction);
        //이동방향에 맞춰 이미지 변경
        spriteRenderer.sprite = images[(int)this.direction];

        //모든 코루틴 중지
        StopAllCoroutines();
        //일정 시간동안 같은 방향으로 이동할 경우 방향을 바꾸도록 함
        StartCoroutine("SetMoveDirectionByTime");
    }
    private void SetMoveDirectionByRandom()
    {
        //이동방향 임의로 설정
        direction = (Direction)Random.Range(0, (int)Direction.Count);
        //Vector3 타입의 이동방향 값 설정
        //direction은 실제 이동 방향 데이터로 사용할 수 없으므로 Vector3FromEnum() 메소드에서
        //방향 벡터를 설정한다.

        SetMoveDirection(direction);
        
        //moveDirection = Vector3FromEnum(direction);
        //이동 방향에 맞춰 이미지를 변경한다.
        //spriteRenderer.sprite = images[(int)direction];
    }

    private IEnumerator SetMoveDirectionByTime()
    {
        yield return new WaitForSeconds(delayTime);

        //현재 이동방향 R or L 이면 direction%2는 0으로
        //따라서 다음 이동 방향(nextDirection)이 Up(1) or Down(3)으로 설정함

        // 현재 이동방향이 Up or Down일시 direction%2는 1로
        // 다음 이동방향은 Right(0) or Left(2)로 설정함

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
