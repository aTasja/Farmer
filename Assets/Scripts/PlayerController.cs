using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class PlayerController : MonoBehaviour
{
    public float Speed = 6;
    public GameObject BombPrefab;
    public Transform BombParent;

    private bool walking;
    private bool makeBomb;

    private Vector3 moveToPosition;
    private Vector3 currentDirection;
    private Vector3 targetDirection;

    private Vector3 previousDir = Vector3.zero;

    private void Start()
    {
        currentDirection = Vector3.zero;
        targetDirection = Vector3.zero;
        walking = false;
    }

    private void OnEnable()
    {
        GameManager.OnEventBomb += SetBomb;
    }

    private void OnDisable()
    {
        GameManager.OnEventBomb -= SetBomb;
    }

    private void Update()
    {
        if (!walking && Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 absoluteDir = Camera.main.ScreenToWorldPoint(touch.position)-transform.position;
            float x = absoluteDir.x;
            float y = absoluteDir.y;

            if (previousDir != absoluteDir)
            {
                previousDir = absoluteDir;
                Debug.Log(absoluteDir);
                if (x != 0 || y != 0)
                {
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        targetDirection = x > 0 ? Vector3.right : Vector3.left;
                    }
                    else
                    {
                        targetDirection = y > 0 ? Vector3.up : Vector3.down;
                    }

                    moveToPosition = transform.position + targetDirection;
                    if (GameManager.IsCellClear(moveToPosition))
                    {
                        StartCoroutine(MovePlayer(moveToPosition));
                    }
                }
            }
        }
    }

    private IEnumerator MovePlayer(Vector3 newPos)
    {
        walking = true;

        if (currentDirection != targetDirection)
        {
            currentDirection = targetDirection;
            gameObject.GetComponent<SpriteManager>().ChangeSprite(targetDirection);
        }

        if (makeBomb)
        {
            GameObject bomb =  Instantiate(BombPrefab, BombParent);
            bomb.transform.position = transform.position;
            makeBomb = false;
        }

        while ((newPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
            yield return null;
        }
        transform.position = newPos;

        walking = false;
    }

    private void SetBomb()
    {
        makeBomb = true;
    }
}
