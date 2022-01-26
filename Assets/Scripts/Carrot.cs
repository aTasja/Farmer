using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public delegate void CarrotHarvestedAction();
    public static event CarrotHarvestedAction OnEventCattorHarvested;

    // Start is called before the first frame update
    void Start()
    {
        float randomZrotation = Random.Range(0, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomZrotation);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (OnEventCattorHarvested != null)
                OnEventCattorHarvested();
            Destroy(gameObject);
        }
    }
}
