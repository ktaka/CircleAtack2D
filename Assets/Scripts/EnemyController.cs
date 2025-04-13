using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int score = 10;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firePoint;

    public bool IsDead {
        get
        {
            return !gameObject.activeSelf;
        }
    }

    float horizontalLimit = 7.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move(Vector2 move)
    {
        Vector3 pos = transform.position;
        pos.x += move.x;
        pos.y += move.y;
        transform.position = pos;

        if (Mathf.Abs(move.y) > 0)
        {
            return false;
        }

        if (!IsDead && Mathf.Abs(pos.x) >= horizontalLimit)
        {
            return true; // hit the limit
        }
        else
        {
            return false;
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
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            GameManager.AddScore(score);
            GameManager.ActiveEnemyNum--;
        }
    }

}
