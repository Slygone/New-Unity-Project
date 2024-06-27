using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   private List<Vector2Int> pathRoute;
   int nextPathCellIndex;
   bool enemyRunCompleted;


    void Update()
    {
        if (pathRoute != null && pathRoute.Count > 1 && !enemyRunCompleted)
        {
            Vector3 currentPos = transform.position;
            Vector3 nextPos = new Vector3(pathRoute[nextPathCellIndex].x, 0.2f, pathRoute[nextPathCellIndex].y);
            transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * 2);
            if (Vector3.Distance(currentPos, nextPos) < 0.05f)
            {
                if (nextPathCellIndex >= pathRoute.Count - 1)
                {
                    Debug.Log("Reached End");
                    enemyRunCompleted = true;
                    Destroy(gameObject);
                }
                else
                {
                    nextPathCellIndex++;
                    Debug.Log("Moving to next path index " + nextPathCellIndex);
                }
            }
        }
    }
    public void SetRoute(List<Vector2Int> route)
    {
        Debug.Log(route.Count + "rotue");
        pathRoute = route;
    }
}
