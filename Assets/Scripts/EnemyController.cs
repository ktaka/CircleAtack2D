using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firePoint;

    float horizontalLimit = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 move)
    {
        Vector3 pos = transform.position;
        pos.x += move.x;
        pos.y += move.y;
        if (Mathf.Abs(pos.x) >= horizontalLimit)
        {
            // notice to manager
        }
        else
        {
            transform.position = pos;
        }
    }

    public void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
