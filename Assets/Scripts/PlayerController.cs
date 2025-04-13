using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firePoint;

    float horizontalLimit = 8.0f;
    float blinkInterval = 0.1f;
    int remainNum = 2;

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
            GameManager.IsGameOver = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            remainNum--;
            if (remainNum == 0)
            {
                GameManager.IsGameOver = true;
                Destroy(gameObject);
            }
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkInterval);
            EnableChildrenRenderer(false );
            yield return new WaitForSeconds(blinkInterval);
            EnableChildrenRenderer(true);
        }
    }

    void EnableChildrenRenderer(bool enable)
    {
        foreach (Renderer r in GetComponentsInChildren<SpriteRenderer>())
        {
            r.enabled = enable;
        }
    }
}
