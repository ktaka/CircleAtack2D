using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyPrefab;

    [System.Serializable]
    struct SpeedTable {
        [SerializeField] public int num;
        [SerializeField] public float speed;
    }

    [SerializeField]
    SpeedTable[] speedTable;

    int row = 11;
    int column;

    float rowDelta = 1.0f;
    float columnDelta = -1.0f;

    float startPosX = 0.0f;
    float startPosY = 0.0f;

    float fireInterval = 1.0f;
    float moveInterval = 1.7f;
    Vector2 move;
    Vector2 moveForNextFrame;
    EnemyController[,] enemys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        column = enemyPrefab.Length;
        move = new Vector2();
        moveForNextFrame = new Vector2(rowDelta, 0);

        startPosX = -rowDelta * ((row + 1) / 2.0f);

        enemys = new EnemyController[column, row];

        SpawnEnemys();
        StartCoroutine(MovingLoop());
        StartCoroutine(FiringLoop());
    }

    void SpawnEnemys()
    {
        Vector3 pos = new Vector3(0, startPosY, 0);

        for (int i = 0; i < column; i++)
        {
            pos.x = startPosX;
            for (int j = 0; j < row; j++)
            {
                pos.x += rowDelta;
                GameObject obj = Instantiate(enemyPrefab[i], pos, Quaternion.identity);
                enemys[i, j] = obj.GetComponent<EnemyController>();
            }
            pos.y -= columnDelta;
        }
        GameManager.ActiveEnemyNum = column * row;
    }

    // Update is called once per frame
    void Update()
    {
    }

    float GetSpeed()
    {
        float speed = moveInterval;
        foreach (SpeedTable st in speedTable)
        {
            if (GameManager.ActiveEnemyNum <= st.num)
            {
                speed = st.speed;
                break;
            }
        }

        return speed;
    }

    IEnumerator MovingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetSpeed());
            if (GameManager.IsGameOver)
            {
                break;
            }
            MoveEnemys();
        }
    }

    void MoveEnemys()
    {
        bool needNewLine = false;
        move = moveForNextFrame;
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                bool didHitLimit = enemys[i, j].Move(move);
                if (didHitLimit)
                {
                    needNewLine = true;
                }
            }
        }

        if (needNewLine)
        {
            moveForNextFrame.x = 0;
            moveForNextFrame.y = columnDelta;
            rowDelta *= -1.0f;
        } else
        {
            moveForNextFrame.x = rowDelta;
            moveForNextFrame.y = 0;
        }
    }

    IEnumerator FiringLoop()
    {
        while (true)
        {
            fireInterval = Random.Range(1.0f, fireInterval);
            yield return new WaitForSeconds(fireInterval);
            if (GameManager.IsGameOver)
            {
                break;
            }
            Fire();
        }
    }

    void Fire()
    {
        EnemyController fireEnemy = null;
        int fireRow = Random.Range(0, row);
        for (int i = 0; i < column; i++)
        {
            if (enemys[i, fireRow].IsDead == false)
            {
                fireEnemy = enemys[i, fireRow];
                break;
            }
        }
        if (fireEnemy != null)
        {
            fireEnemy.Fire();
        }
    }
}
