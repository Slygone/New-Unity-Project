using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    // Dimensions of the path grid
    private int width, height;

    // List holding the path cells
    private List<Vector2Int> pathCells;

    // List holding the generated route
    private List<Vector2Int> route;

    // Constructor to initialize width and height
    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    // Method to generate a path
    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();
        int y = (int)(height / 2);
        int x = 0;

        // Generate path until the width limit is reached
        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));

            bool validMove = false;

            // Attempt to make a valid move
            while (!validMove)
            {
                //Roll to move in direction
                int move = Random.Range(0, 3);

                // Move right
                if (move == 0 || x % 2 == 0 || x > (width - 2))
                {
                    x++;
                    validMove = true;
                }
                // Move up
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < (height - 2))
                {
                    y++;
                    validMove = true;
                }
                // Move down
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    // Method to generate a route based on the path
    public List<Vector2Int> GenerateRoute()
    {
        Vector2Int direction = Vector2Int.right;
        route = new List<Vector2Int>();
        Vector2Int currentCell = pathCells[0];

        // Generate route until the width limit is reached
        while (currentCell.x < width)
        {
            route.Add(new Vector2Int(currentCell.x, currentCell.y));

            // Check and move in the current direction or change direction if needed
            if (CellIsTaken(currentCell + direction))
            {
                currentCell += direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell += direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell += direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell += direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.left) && direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
                currentCell += direction;
            }
            else
            {
                // Nowhere to go, return the route

                Debug.Log("Path Size = " + route.Count);
                return route;
            }
        }

        return route;
    }

    // Method to generate crossroads in the path
    public bool GenerateCrossroads()
    {
        for (int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int pathCell = pathCells[i];

            // Check if a crossroad can be created
            if (pathCell.x > 3 && pathCell.x < width - 4 && pathCell.y > 2 && pathCell.y < height - 3)
            {
                // Check if cells are empty to form a crossroad
                if (CellIsEmpty(pathCell.x, pathCell.y + 3) && CellIsEmpty(pathCell.x + 1, pathCell.y + 3) && CellIsEmpty(pathCell.x + 2, pathCell.y + 3)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y + 2) && CellIsEmpty(pathCell.x, pathCell.y + 2) && CellIsEmpty(pathCell.x + 1, pathCell.y + 2) && CellIsEmpty(pathCell.x + 2, pathCell.y + 2) && CellIsEmpty(pathCell.x + 3, pathCell.y + 2)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y + 1) && CellIsEmpty(pathCell.x, pathCell.y + 1) && CellIsEmpty(pathCell.x + 1, pathCell.y + 1) && CellIsEmpty(pathCell.x + 2, pathCell.y + 1) && CellIsEmpty(pathCell.x + 3, pathCell.y + 1)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y) && CellIsEmpty(pathCell.x + 2, pathCell.y) && CellIsEmpty(pathCell.x + 3, pathCell.y)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y - 1) && CellIsEmpty(pathCell.x + 2, pathCell.y - 1))
                {
                    // Insert new path cells to form a crossroad
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y + 1), new Vector2Int(pathCell.x + 2, pathCell.y + 2), new Vector2Int(pathCell.x + 1, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 1) });
                    return true;
                }
                // Check if cells are empty to form another type of crossroad
                if (CellIsEmpty(pathCell.x + 1, pathCell.y + 1) && CellIsEmpty(pathCell.x + 2, pathCell.y + 1)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y) && CellIsEmpty(pathCell.x + 2, pathCell.y) && CellIsEmpty(pathCell.x + 3, pathCell.y)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y - 1) && CellIsEmpty(pathCell.x, pathCell.y - 1) && CellIsEmpty(pathCell.x + 1, pathCell.y - 1) && CellIsEmpty(pathCell.x + 2, pathCell.y - 1) && CellIsEmpty(pathCell.x + 3, pathCell.y - 1)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y - 2) && CellIsEmpty(pathCell.x, pathCell.y - 2) && CellIsEmpty(pathCell.x + 1, pathCell.y - 2) && CellIsEmpty(pathCell.x + 2, pathCell.y - 2) && CellIsEmpty(pathCell.x + 3, pathCell.y - 2)
                    && CellIsEmpty(pathCell.x, pathCell.y - 3) && CellIsEmpty(pathCell.x + 1, pathCell.y - 3) && CellIsEmpty(pathCell.x + 2, pathCell.y - 3))
                {
                    // Insert new path cells to form another crossroad
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y - 1), new Vector2Int(pathCell.x + 2, pathCell.y - 2), new Vector2Int(pathCell.x + 1, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 1) });
                    return true;
                }
            }
        }
        return false;
    }

    // Method to check if a cell is empty
    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    // Method to check if a cell is taken
    public bool CellIsTaken(Vector2Int cell)
    {
        return pathCells.Contains(cell);
    }

    // Overloaded method to check if a cell is taken using x and y coordinates
    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    // Method to get the neighbor value of a cell
    public int getCellNeighbourValue(int x, int y)
    {
        int returnValue = 0;

        if (CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }

        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }

        if (CellIsTaken(x + 1, y))
        {
            returnValue += 4;
        }

        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }

        return returnValue;
    }
}
