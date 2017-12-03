using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Vector2Int origin;
    public Vector2Int target;
    public Vector2Int position;
    Vector2Int direction ;

    public float interval = 1f;
    public float sum = 0;

    public void Initialize (Vector2Int origin, Vector2Int target,Vector2Int position) {
        this.origin = origin;
        this.position = position;
        this.target = target;
        int deltaX = Mathf.Clamp ( target.x - origin.x , -1 , 1 );
        int deltaY = Mathf.Clamp ( target.y - origin.y , -1 , 1);
        direction = new Vector2Int (deltaX,deltaY);
    }

    void Update () {
        sum += Time.deltaTime;
        if (sum >= interval){
            moveEnemy ();
            sum = 0;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.forward,out hit,Mathf.Infinity) ){
            if (hit.collider.tag == "PLAYER"){
                print ("MATEI O PLAYER");
            }             
        }
        Debug.DrawRay(transform.position + direction.ToVector3(), Vector3.forward, Color.black);
    }

    void flip () {
        direction *= -1;
    }

    void moveEnemy () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + direction.ToVector3(), Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "WALL") {
                flip();
             }
            else if (hit.collider.tag == "BOX")
            {
                flip();
            }
        }
        else if (!LevelLoader.Instance.LoadedLevel.insideGrid(position+direction)){
            flip();
        }
        position += direction;
        transform.position += direction.ToVector3();
    }

}
