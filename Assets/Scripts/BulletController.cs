using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float velocity = 2.0f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityY = velocity;
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
