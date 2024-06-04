using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject enemyObject;
    private List<Vector2Int> pathRoute;
    private GameObject enemyInstance;
    int nextPathCellIndex;
    bool enemyRunCompleted;

    void Start()
    {
        // fix enemy start instead of hardcode
        enemyInstance = Instantiate(enemyObject, new Vector3(0,0.2f,5),Quaternion.identity);
        nextPathCellIndex = 1;
        enemyRunCompleted = false;
    }

    void Update()
    {
        if (pathRoute != null && pathRoute.Count > 1 && !enemyRunCompleted)
        {
            Vector3 currentPos = enemyInstance.transform.position;
            Vector3 nextPos = new Vector3(pathRoute[nextPathCellIndex].x, 0.2f, pathRoute[nextPathCellIndex].y);
            enemyInstance.transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * 2);
            if(Vector3.Distance(currentPos, nextPos) < 0.05f)
            {
                if(nextPathCellIndex >= pathRoute.Count - 1)
                {
                    Debug.Log("Reached End");
                    enemyRunCompleted = true;
                    Destroy(enemyInstance);
                }
                else
                {
                    nextPathCellIndex++;
                    Debug.Log("Moving to next path index " + nextPathCellIndex);
                }
            }
        }
    }
    public void SetPathCells(List<Vector2Int> pathCells)
    {
        this.pathRoute = pathCells;
    }
}
