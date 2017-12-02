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
}
