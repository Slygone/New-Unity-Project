using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public Enemy enemyObject;
    private List<Vector2Int> pathRoute;
    public float spawnInterval = 5f;
    public int maxEnemyCount = 10;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        // fix enemy start instead of hardcode
        
    }
   

    IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < maxEnemyCount; i++)
        {
            yield return new WaitForSeconds(spawnInterval);
            var enemy = Instantiate(enemyObject, new Vector3(0, 0.2f, 5), Quaternion.identity);
            enemy.SetRoute(pathRoute);
        }   
    }
    void Update()
    {
        
    }

   
    public void SetPathCells(List<Vector2Int> pathCells)
    {
        this.pathRoute = pathCells;
    }
}
