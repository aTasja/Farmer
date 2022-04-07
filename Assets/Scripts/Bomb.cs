using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Lifetime = 5;

    public delegate void DirtyFarmerAction();
    public static event DirtyFarmerAction OnEventDirtyFarmer;
    
    private bool _collided;
    private GameObject _victim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchBomb());
        _collided = false;
    }

    IEnumerator LaunchBomb()
    {
        yield return new WaitForSeconds(Lifetime);

        if (_collided) {
            _victim.GetComponent<DestinationSprite>().DirtySprite();
            if (OnEventDirtyFarmer != null)
                OnEventDirtyFarmer();
        }
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Farmer")
        {
            _collided = true;
            _victim = col.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        _collided = false;
        _victim = null;
    }
}
