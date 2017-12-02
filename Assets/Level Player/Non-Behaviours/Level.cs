using UnityEngine;
using System.Collections.Generic;
public class Level
{
    int[,] grid;
    int[,] Grid { get { return grid; } }

    int Width  {get { return grid.GetLength(1); } }
    int Height { get { return grid.GetLength(0); } }

    Player player;

    public Level(int rowCount, int colCount)
    {
        grid = new int[rowCount, colCount];
    }

    public void setCell(int x, int y, int v)
    {
        grid[x, y] = v;
    }

    public void setPlayer (Player player) {
        this.player = player;
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
        int deltaX = playerInput.x - player.currentPosition.x ;
        int deltaY = playerInput.y - player.currentPosition.y ;

        if (deltaX == 0 && deltaY == -1){
            // moveLeft
            player.transform.position += Vector3.left;
            player.currentPosition.y -= 1;
        }
        else if (deltaX == 0 && deltaY == 1){
            // moveRight
            player.transform.position += Vector3.right;
            player.currentPosition.y += 1;
        }
        else if (deltaY == 0 && deltaX == 1){
            // MOVE DOWN
            player.transform.position += Vector3.down;
            player.currentPosition.x += 1;
        }
        else if (deltaY == 0  && deltaX == -1){
            player.transform.position += Vector3.up;
            player.currentPosition.x -= 1;
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