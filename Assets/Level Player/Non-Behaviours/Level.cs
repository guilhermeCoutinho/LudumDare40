using UnityEngine;
using System.Collections.Generic;
public class Level
{
    public LevelData levelInfo;
    int[,] grid;
    public int[,] Grid { get { return grid; } }

    public int Width  {get { return grid.GetLength(1); } }
    public int Height { get { return grid.GetLength(0); } }

    public Player player;
    public List<Enemy> enemies;
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
        return new Vector3(j - (Width - 1) / 2f, (Height - 1 - i) - Height / 2f, 0);
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

    public void setEnemyReference (Enemy enemy) {
        if (enemies == null )
            enemies = new List<Enemy>();
        enemies.Add (enemy);
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
        if(!insideGrid(target))return false;
        bool notColliding = isGround(target.x, target.y) || isKey(target.x, target.y)
        || isPressurePlate(target)||isFinishTile(target);
        Sound.Instance.Play(4, (int)Sound.soundEvents.STEPS);
        return notColliding;
	}

    bool isFinishTile(Vector2Int target){
        return grid[target.x, target.y] == LevelLoader.Instance.victoryPlateId;
    }

    bool isPressurePlate (Vector2Int target ){
        return grid[target.x, target.y] == LevelLoader.Instance.pressurePlateId;
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
    		Sound.Instance.Play(3, (int)Sound.soundEvents.PUSHBOX);
        }
	}

    bool canMoveBox (Vector2Int playerPosition, Vector2Int direction) {
        Vector2Int target = playerPosition + direction;
        Vector2Int boxTargetAfterBeingPushed = target + direction;
        if (!insideGrid(boxTargetAfterBeingPushed))
            return false;
        if( grid[ target.x,target.y] == LevelLoader.Instance.boxId){
            if ( validBoxCollisions ( grid[boxTargetAfterBeingPushed.x,boxTargetAfterBeingPushed.y])){
                if (!boxWillCollideWithEnemy(boxTargetAfterBeingPushed))
                    return true;
            }
        }
        return false;
    }

    // HOTFIX
    bool boxWillCollideWithEnemy (Vector2Int boxPositionAfterBeingPushed) 
    {
        if (enemies != null) {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.position.isEqual(boxPositionAfterBeingPushed))
                    return true;
            }
        }
        return false;
    }

    bool validBoxCollisions (int collisionId) {
        return
        collisionId == LevelLoader.Instance.floorId||
        collisionId == LevelLoader.Instance.pressurePlateId||
        collisionId == LevelLoader.Instance.victoryPlateId
        ;
    }

    public bool insideGrid (Vector2Int p) {
        if (p.x >= Height || p.x < 0 || p.y >= Width || p.y < 0 )
            return false;
        return true;
    }
}