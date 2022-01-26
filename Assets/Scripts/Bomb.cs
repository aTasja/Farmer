using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Lifetime = 5;

    public delegate void DirtyFarmerAction();
    public static event DirtyFarmerAction OnEventDirtyFarmer;
    
    private bool collided;
    private GameObject victim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchBomb());
        collided = false;
    }

    IEnumerator LaunchBomb()
    {
        yield return new WaitForSeconds(Lifetime);

        if (collided) {
            victim.GetComponent<SpriteManager>().DirtySprite();
            if (OnEventDirtyFarmer != null)
                OnEventDirtyFarmer();
        }
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Farmer")
        {
            collided = true;
            victim = col.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        collided = false;
        victim = null;
    }
}
