using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DestinationSprite : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(Vector3 direction)
    {
        if (direction == Vector3.up)
            _spriteRenderer.sprite = atlas.GetSprite("up");
        else if (direction == Vector3.down)
            _spriteRenderer.sprite = atlas.GetSprite("down");
        else if (direction == Vector3.left)
            _spriteRenderer.sprite = atlas.GetSprite("left");
        else if (direction == Vector3.right)
            _spriteRenderer.sprite = atlas.GetSprite("right");
    }

    public void DirtySprite()
    {
        string dirtySpriteName = "dirty_" + _spriteRenderer.sprite.name.Split('(')[0]; // geting rid of the word [(Clone)] in the name of sprite 
        Debug.Log(dirtySpriteName);
        _spriteRenderer.sprite = atlas.GetSprite(dirtySpriteName);
    }
}
