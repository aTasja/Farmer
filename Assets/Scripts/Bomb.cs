using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Lifetime = 5;

    void Start() => Destroy(gameObject, Lifetime);
    
}
