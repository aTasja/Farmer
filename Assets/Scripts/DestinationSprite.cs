using UnityEngine;
using UnityEngine.U2D;

public class DestinationSprite : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _atlas;

    private SpriteRenderer _spriteRenderer;

    private void Start() => _spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 

    public void ChangeSprite(Vector3 direction)
    {
        if (direction == Vector3.up)
            _spriteRenderer.sprite = _atlas.GetSprite("up");
        else if (direction == Vector3.down)
            _spriteRenderer.sprite = _atlas.GetSprite("down");
        else if (direction == Vector3.left)
            _spriteRenderer.sprite = _atlas.GetSprite("left");
        else if (direction == Vector3.right)
            _spriteRenderer.sprite = _atlas.GetSprite("right");
    }

    public void SetDirtySprite() => _spriteRenderer.sprite = _atlas.GetSprite(SpriteName());

    private string SpriteName() => "dirty_" + _spriteRenderer.sprite.name.Split('(')[0];
}
