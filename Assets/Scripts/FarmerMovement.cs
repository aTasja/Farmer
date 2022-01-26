using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    public float Speed = 1f;
    public float DirtyDelayRuration = 5f;

    private GameObject player;
    private Vector3 playerPos;
    
    bool isMoving = false;

    private bool isDirty = false;
    private float dirtyDelayElapsed = 0;

    private Vector3 currentDirection = Vector3.zero;
    private Vector3 finalDirection = Vector3.zero;

    private Collider2D farmerCollider;
    private Collider2D playerCollider;

    void Start()
    {
        farmerCollider = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Bomb.OnEventDirtyFarmer += FarmerIsDirty;
    }

    private void OnDisable()
    {
        Bomb.OnEventDirtyFarmer -= FarmerIsDirty;
    }

    void Update()
    {
        if (isDirty && dirtyDelayElapsed <= DirtyDelayRuration)
            dirtyDelayElapsed += Time.deltaTime;
        else
            isDirty = false;

        if (!isDirty)
        {
            if (farmerCollider.bounds.Intersects(playerCollider.bounds))
                GameManager.GAMEOVER();

            playerPos = player.GetComponent<Transform>().position;
            float[] directions = new float[]
            {
            transform.position.x - playerPos.x,
            transform.position.y - playerPos.y,
            };

            finalDirection = RandomlyChooseDirection(directions);

            if (!isMoving) StartCoroutine(Move(transform.position + finalDirection));
        }
    }

    Vector3 RandomlyChooseDirection(float[] xANDy)
    {
        Vector3 finalDest;
        if (Random.Range(0, xANDy.Length) == 0)
        {
            finalDest =  xANDy[0] >= 0 ? Vector3.left : Vector3.right;
        }
        else
        {
            finalDest =  xANDy[1] >= 0 ? Vector3.down : Vector3.up;
        }

        // check if the final position is not empty and change the result if it's so
        Vector3 moveToPosition = transform.position + finalDest;
        if (!GameManager.IsCellClear(moveToPosition))
        {
            if (finalDest == Vector3.left || finalDest == Vector3.right)
            {
                finalDest = xANDy[1] >= 0 ? Vector3.down : Vector3.up;
            }
            else
            {
                finalDest = xANDy[0] >= 0 ? Vector3.left : Vector3.right;
            }
        }
        return finalDest;
    }

    private IEnumerator Move(Vector3 newPos)
    {
        isMoving = true;

        if (currentDirection != finalDirection)
        {
            currentDirection = finalDirection;
            gameObject.GetComponent<SpriteManager>().ChangeSprite(finalDirection);
        }

        while ((newPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
            yield return null;
        }
        transform.position = newPos;
        
        isMoving = false;
    }

    void FarmerIsDirty()
    {
        if (!isDirty)
        {
            dirtyDelayElapsed = 0;
            isDirty = true;
        }
    }
}

