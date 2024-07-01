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
        // Check if the pathRoute is set, has more than one point, and the enemy hasn't completed the run
        if (pathRoute != null && pathRoute.Count > 1 && !enemyRunCompleted)
        {
            // Current position of the enemy
            Vector3 currentPos = transform.position;

            // Next position based on the pathRoute and current index
            Vector3 nextPos = new Vector3(pathRoute[nextPathCellIndex].x, 0.2f, pathRoute[nextPathCellIndex].y);

            // Move the enemy towards the next position
            transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * 2);

            // Check if the enemy is close enough to the next position
            if (Vector3.Distance(currentPos, nextPos) < 0.05f)
            {
                // Check if the enemy has reached the end of the path
                if (nextPathCellIndex >= pathRoute.Count - 1)
                {
                    Debug.Log("Reached End");

                    // Mark the run as completed and destroy the enemy game object
                    enemyRunCompleted = true;
                    Destroy(gameObject);
                }
                else
                {
                    // Move to the next path cell
                    nextPathCellIndex++;
                    Debug.Log("Moving to next path index " + nextPathCellIndex);
                }
            }
        }
    }

    // Set the path route for the enemy
    public void SetRoute(List<Vector2Int> route)
    {
        Debug.Log(route.Count + " route");

        // Assign the route to the pathRoute variable
        pathRoute = route;
    }
}
