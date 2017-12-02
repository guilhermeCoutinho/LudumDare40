using UnityEngine;
using System.Collections.Generic;
public class LevelData
{
    int[,] grid;
    int[,] Grid { get { return grid; } }

    int Width  {get { return grid.GetLength(1); } }
    int Height { get { return grid.GetLength(0); } }

    public Vector2Int playerPositionInGrid;
    public Vector2Int endPositionInGrid;
    public List<Vector2Int> enemiesPositionInGrid;

    public LevelData(int rowCount, int colCount)
    {
        grid = new int[rowCount, colCount];
        enemiesPositionInGrid = new List<Vector2Int>();
    }

    public void setCell(int x, int y, int v)
    {
        if (v != (int)CellElement.Type.EMPTY)
            grid[x, y] = (int)CellElement.Type.FLOOR;
        else
            grid[x, y] = v;
        switch (v)
        {
            case (int)CellElement.Type.INITIAL_POSITION:
                playerPositionInGrid = new Vector2Int(x,y);
                break;
            case (int)CellElement.Type.ENEMY_POSITION:
                enemiesPositionInGrid.Add(new Vector2Int(x, y));
                break;
            case (int)CellElement.Type.END_POSITION:
                endPositionInGrid = new Vector2Int(x, y);
                break;
        }
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

    public bool MovePlayer (Vector2Int direction)
    {
        Vector2Int newPos = new Vector2Int
            (playerPositionInGrid.x + direction.x,
            playerPositionInGrid.y + direction.y);
        
        if (!isFloor(newPos))
            return false;

        playerPositionInGrid.x = newPos.x;
        playerPositionInGrid.y = newPos.y;
        if (isEnemyInPosition(playerPositionInGrid))
            return true;
//        moveEnemies();
        if (isEnemyInPosition(playerPositionInGrid))
            return true;
        return false;
    }

    void moveEnemies ()
    {
        bool validForX = true;
        bool validForY = true;
        bool validGridValue = true;
        Vector2Int[] directions = { Vector2Int.Right,
            Vector2Int.Left,Vector2Int.Up,Vector2Int.Down };
        for(int i = 0; i < directions.Length; i++)
        {
            Vector2Int evalPosition = playerPositionInGrid;
            while (true)
            {
                evalPosition.x += directions[i].x;
                evalPosition.y += directions[i].y;
                validForX = evalPosition.x < Height && evalPosition.x >= 0;
                validForY = evalPosition.y < Width && evalPosition.y >= 0;
                if (!validForX || !validForY)
                    break;
                validGridValue = grid[evalPosition.x, evalPosition.y] != (int)CellElement.Type.EMPTY;
                if (!validGridValue)
                    break;
                moveEnemy(directions[i] , evalPosition);

            }
        }
    }
    /* 
     *  move the enemy in direction that are currently in position
     */
    void moveEnemy (Vector2Int direction , Vector2Int position)
    {
        foreach (Vector2Int pos in enemiesPositionInGrid)
        {
            if (pos.Equals(position))
            {
                pos.x += direction.x;
                pos.y += direction.y;
            }
        }
    }

    bool isEnemyInPosition (Vector2Int position) {
        foreach (Vector2Int pos in enemiesPositionInGrid){
            if (pos.Equals(position))
                return true;
        }
        return false;
    }

    bool insideGrid (Vector2Int p) {
        if (p.x >= Height || p.x < 0 || p.y >= Width || p.y < 0 )
            return false;
        return true;
    }

    bool isFloor (Vector2Int p) {
        if (!insideGrid(p))
            return false;
        return grid[p.x, p.y] == (int)CellElement.Type.FLOOR;
    }
}