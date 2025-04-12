using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    int row = 3;
    int collum = 1;

    float rowDelta = 1.0f;
    float collumDelta = 1.0f;

    Vector2 move;
    EnemyController[] enemys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = new Vector2(rowDelta, 0);

        enemys = new EnemyController[row * collum];
        SpawnEnemys();
    }

    void SpawnEnemys()
    {
        Vector3 pos = new Vector3(0, 0, 0);

        for (int i = 0; i < row; i++)
        {
            pos.x += rowDelta;
            GameObject obj = Instantiate(enemy, pos, Quaternion.identity);
            enemys[i] = obj.GetComponent<EnemyController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("MoveEnemys", 1.0f, 1.0f);
    }

    void MoveEnemys()
    {
        for (int i = 0; i < row; i++)
        {
            enemys[i].Move(move);
        }
    }
}
