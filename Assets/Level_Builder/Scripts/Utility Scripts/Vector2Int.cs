using UnityEngine;

public class Vector2Int {
    public int x;
    public int y;
    public static Vector2Int right =new Vector2Int(0, 1);
    public static Vector2Int left = new Vector2Int(0, -1);
    public static Vector2Int up =   new Vector2Int(-1,0);
    public static Vector2Int down = new Vector2Int(1,0);
    public static Vector2Int zero = new Vector2Int(0,0);

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void print ()
    {
        UnityEngine.Debug.Log(x + "," + y);
    }
    public override string ToString() {
        return (x + "," + y);
    }

	public Vector3 ToVector3() {
		return new Vector3(y, -x, 0);
	}

	public bool isEqual( Vector2Int v2) {
		if (v2 != null) {
			return x == v2.x && y == v2.y;
		} else {
			return false;
		}
	}
	public static Vector2Int operator +(Vector2Int v1, Vector2Int v2) {
		return new Vector2Int(v1.x + v2.x, v1.y + v2.y);
	}

	public static Vector2Int operator -(Vector2Int v1) {
		return new Vector2Int(-v1.x , -v1.y);
	}

	public static Vector2Int operator -(Vector2Int v1, Vector2Int v2) {
		return v1 + (-v2);
	}
	public static Vector2Int operator *(Vector2Int v1, int c) {
		return new Vector2Int(v1.x*c, v1.y*c);
	}
	public static Vector2Int operator /(Vector2Int v1, int c) {
		return new Vector2Int(v1.x * 1/c, v1.y * 1/c);
	}
}
