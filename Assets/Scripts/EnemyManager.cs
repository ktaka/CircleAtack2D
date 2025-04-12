using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    int row = 3;
    int collum = 3;

    float rowDelta = 1.0f;
    float collumDelta = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemys();
    }

    void SpawnEnemys()
    {
        Vector3 pos = new Vector3(0, 0, 0);

        for (int i = 0; i < row; i++)
        {
            pos.x += rowDelta;
            Instantiate(enemy, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
