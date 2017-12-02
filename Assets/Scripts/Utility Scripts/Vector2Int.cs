public class Vector2Int {
    public int x;
    public int y;
    public static Vector2Int Right =new Vector2Int(0, 1);
    public static Vector2Int Left = new Vector2Int(0, -1);
    public static Vector2Int Up =   new Vector2Int(-1,0);
    public static Vector2Int Down = new Vector2Int(1,0);

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void print ()
    {
        UnityEngine.Debug.Log(x + "," + y);

    }
}
