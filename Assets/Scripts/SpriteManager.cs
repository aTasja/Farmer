using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(Vector3 direction)
    {
        if (direction == Vector3.up)
            spriteRenderer.sprite = atlas.GetSprite("up");
        else if (direction == Vector3.down)
            spriteRenderer.sprite = atlas.GetSprite("down");
        else if (direction == Vector3.left)
            spriteRenderer.sprite = atlas.GetSprite("left");
        else if (direction == Vector3.right)
            spriteRenderer.sprite = atlas.GetSprite("right");
    }

    public void DirtySprite()
    {
        string dirtySpriteName = "dirty_" + spriteRenderer.sprite.name.Split('(')[0]; // geting rid of the word [(Clone)] in the name of sprite 
        Debug.Log(dirtySpriteName);
        spriteRenderer.sprite = atlas.GetSprite(dirtySpriteName);
    }
}
