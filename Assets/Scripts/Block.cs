using UnityEngine;

public class Block : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        Destroy(gameObject);
    }
}