using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public Enemy enemyObject;
    private List<Vector2Int> pathRoute;
    public float spawnInterval = 5f;
    public int maxEnemyCount = 10;
    private Vector3 startSpawnPosition;

    void Start()
    {
        //start coroutine only if pathRoute is set
        if(pathRoute != null && pathRoute.Count > 0)
        {
            //get the starting position from the pathRoute
            Vector2Int startPosition = pathRoute[0];
            //Assign startSpawnPosition 
            startSpawnPosition = new Vector3(startPosition.x, 0.2f, startPosition.y);
            StartCoroutine(SpawnEnemies());
        }
        
        // fix enemy start instead of hardcode
    }

    IEnumerator SpawnEnemies()
    {
        // Loop to spawn the specified number of enemies
        for (int i = 0; i < maxEnemyCount; i++)
        {
            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);

            // Instantiate the enemy object at position
            var enemy = Instantiate(enemyObject, startSpawnPosition, Quaternion.identity);

            // Set the path route for the spawned enemy
            enemy.SetRoute(pathRoute);
        }
    }

    // Set the path cells for the enemies
    public void SetPathCells(List<Vector2Int> pathCells)
    {
        // Assign the path cells to the pathRoute variable
        this.pathRoute = pathCells;
    }
}
