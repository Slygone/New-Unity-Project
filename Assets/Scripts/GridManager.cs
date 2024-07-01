using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Width and height of the grid
    public int gridWidth = 16;
    public int gridHeight = 8;

    // Minimum length of the generated path
    public int minPathLength = 30;
    private EnemyWaveManager waveManager;

    // Arrays holding the path and scenery cell objects
    public GridCellObject[] pathCellObjects;
    public GridCellObject[] sceneryCellObjects;

    // Reference to PathGenerator
    private PathGenerator pathGenerator;

    void Start()
    {
        // Initialize the PathGenerator with grid dimensions
        pathGenerator = new PathGenerator(gridWidth, gridHeight);

        // Get the EnemyWaveManager component attached to the same GameObject
        waveManager = GetComponent<EnemyWaveManager>();

        // Generate an initial path and get its size
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;

        // Regenerate path if it's shorter than the minimum required length
        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            while (pathGenerator.GenerateCrossroads()) ;
            pathSize = pathCells.Count;
        }
        waveManager.SetPathCells(pathCells);

        // Start the coroutine to create the grid
        StartCoroutine(CreateGrid(pathCells));
    }

    // Coroutine to create the grid
    IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        // Lay the path cells first
        yield return LayPathCells(pathCells);

        // Lay the scenery cells
        yield return LaySceneryCells();

        // Set the path cells for the wave manager
        waveManager.SetPathCells(pathGenerator.GenerateRoute());
    }

    // Coroutine to lay the path cells
    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        // Iterate through each path cell
        foreach (Vector2Int pathCell in pathCells)
        {
            // Get the neighbor value for the current path cell
            int neighborValue = pathGenerator.getCellNeighbourValue(pathCell.x, pathCell.y);

            // Get the path tile prefab based on the neighbor value
            GameObject pathTile = pathCellObjects[neighborValue].cellPrefab;

            // Instantiate the path tile at the specified position and rotation
            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, pathCellObjects[neighborValue].yRotation, 0f, Space.Self);

            // Wait for a short duration before laying the next path cell
            yield return new WaitForSeconds(0.015f);
        }

        yield return null;
    }

    // Coroutine to lay the scenery cells
    IEnumerator LaySceneryCells()
    {
        Debug.Log("Lay Scenery");

        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomSceneryCellIndex = Random.Range(0, sceneryCellObjects.Length);
                    Instantiate(sceneryCellObjects[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);

                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        yield return null;
    }
}
