using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    void Start()
    {
        float randomZrotation = Random.Range(0, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomZrotation);
    }
}
