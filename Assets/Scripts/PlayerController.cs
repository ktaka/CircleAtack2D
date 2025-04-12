using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firePoint;

    float horizontalLimit = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.x += horizontalInput * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);
        transform.position = pos;

        if (Input.GetButtonUp("Jump"))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
