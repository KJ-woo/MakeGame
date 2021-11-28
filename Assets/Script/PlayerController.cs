using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.right;//방향을나타내는변수 , 오른쪽으로 이동으로 디폴트설정함
    private Direction direction = Direction.Right;

    private Movement2D movement2D;
    private AroundWrap aroundWrap;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        tileLayer = 1 << LayerMask.NameToLayer("Tile");
        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1.방향키 입력으로 이동방향 설정
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            moveDirection = Vector2.up;
            direction = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
            direction = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
            direction = Direction.Down;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);//첫번째 매개변수는 발사되는 광선위치
        //두번째 : 광선의 발사 방향 세번째 : 광선의 길이 네번째 : 광선이 충돌되는 레이어 종류
        if (hit.transform == null)
        {
            //MoveTo() 함수에 이동방향을 매개변수로 전달해 이동한다.
            bool movePossible = movement2D.MoveTo(moveDirection);
            if (movePossible)
            {
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }
            aroundWrap.UpdateAroundWrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            // 아이템 획득 처리 (현재는 아이템파괴만 함)
            Destroy(collision.gameObject);
        }

        if ( collision.CompareTag("Enemy"))
        {
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
