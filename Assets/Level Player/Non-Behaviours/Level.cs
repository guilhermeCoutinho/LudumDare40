using UnityEngine;
using System.Collections.Generic;
public class Level
{
    int[,] grid;
    int[,] Grid { get { return grid; } }

    int Width  {get { return grid.GetLength(1); } }
    int Height { get { return grid.GetLength(0); } }

    Vector2Int playerPosition ;

    public Level(int rowCount, int colCount)
    {
        grid = new int[rowCount, colCount];
    }

    public void setCell(int x, int y, int v)
    {
        grid[x, y] = v;
    }

    public Vector3 getWorldPosition (Vector2Int positionInGrid)
    {
        return getWorldPosition(positionInGrid.x, positionInGrid.y);
    }
    public Vector3 getWorldPosition (int i , int j)
    {
        return new Vector3(j - Width / 2, (Height - 1 - i) - Height / 2, 0);
    }

    public void printGrid()
    {
        string debug = "";
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                debug += grid[i, j];
            }
            debug += "\n";
        }
        Debug.Log(debug);
    }

    public bool MovePlayer (Vector2Int playerInput)
    {
        int deltaX = playerInput.x - playerPosition.x ;
        int deltaY = playerInput.y - playerPosition.y ;
        
        if (deltaX == -1 && deltaY == 0){
            // moveLeft 
        }
        else if (deltaX == 1 && deltaY ==0){
            // moveRight
        }
        else if (deltaY == -1 && deltaX == 0){
            // move up
        }
        else if (deltaY==1 && deltaX ==0){
            // move down
        }else {
            return false;
        }   
        return true;
    }

    void moveEnemies ()
    {
    }

    bool isEnemyInPosition (Vector2Int position) {
        return false;
    }

    bool insideGrid (Vector2Int p) {
        if (p.x >= Height || p.x < 0 || p.y >= Width || p.y < 0 )
            return false;
        return true;
    }

    bool isFloor (Vector2Int p) {
        return false;
    }
}