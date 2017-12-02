using UnityEngine;
using System.Collections.Generic;
public class Level
{
    int[,] grid;
    int[,] Grid { get { return grid; } }

    int Width  {get { return grid.GetLength(1); } }
    int Height { get { return grid.GetLength(0); } }

    Player player;
    KeyboardMapper keyboard;

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

        if (deltaX == 0 && deltaY == -1) {
			move(Vector2Int.left);
		} else if (deltaX == 0 && deltaY == 1) {
			move(Vector2Int.right);
		} else if (deltaY == 0 && deltaX == 1) {
			move(Vector2Int.down);
		} else if (deltaY == 0  && deltaX == -1) {
			move(Vector2Int.up);
		} else {
            return false; //não conseguiu mover
        }
        return true; //conseguiu mover
    }

	bool canMove(Vector2Int origin, Vector2Int target) {
		return (isGround(target.x, target.y)||isKey(target.x, target.y))&&insideGrid(target);
	}

	bool isGround(int x, int y) {
		return grid[x, y] == LevelLoader.Instance.floorId;
	}

	bool isKey(int x, int y) {
		return grid[x, y] == LevelLoader.Instance.keyId;
	}

	private void move(Vector2Int d) {
		if (canMove(player.currentPosition, player.currentPosition+d) ) {
			player.transform.position += d.ToVector3();
			grid[player.currentPosition.x, player.currentPosition.y] = player.onTopOf;
			player.currentPosition += d;
			player.onTopOf = grid[player.currentPosition.x, player.currentPosition.y];
			grid[player.currentPosition.x, player.currentPosition.y] = LevelLoader.Instance.playerId;
		}
        else if (canMoveBox(player.currentPosition,d)){
            player.transform.position += d.ToVector3();
            grid[player.currentPosition.x, player.currentPosition.y] = player.onTopOf;
            player.currentPosition += d;
            grid[player.currentPosition.x, player.currentPosition.y] = LevelLoader.Instance.playerId;
            
            Box movingBox = Box.getBoxByPosition(player.currentPosition);
            movingBox.transform.position += d.ToVector3();
            movingBox.currentPosition += d;

            player.onTopOf = movingBox.onTopOf;
            movingBox.onTopOf = grid[movingBox.currentPosition.x,movingBox.currentPosition.y];
            grid[movingBox.currentPosition.x,movingBox.currentPosition.y] = LevelLoader.Instance.boxId;
        }
	}

    bool canMoveBox (Vector2Int origin, Vector2Int direction) {
        Vector2Int target = origin + direction;
        if( grid[ target.x,target.y] == LevelLoader.Instance.boxId){
            Vector2Int boxTargetAfterBeingPushed = target + direction;
            if (!insideGrid(boxTargetAfterBeingPushed))
                return false;
            if ( validBoxCollisions ( grid[boxTargetAfterBeingPushed.x,boxTargetAfterBeingPushed.y])){
                return true;
            }
        }
        return false;
    }

    bool validBoxCollisions (int collisionId) {
        return collisionId == LevelLoader.Instance.floorId;
    }

    bool insideGrid (Vector2Int p) {
        if (p.x >= Height || p.x < 0 || p.y >= Width || p.y < 0 )
            return false;
        return true;
    }
}