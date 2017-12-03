using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Vector2Int origin;
    public Vector2Int target;
    public Vector2Int position;
    public Sprite hole;
    public Sprite enemy;
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
/*        if(direction.x==0&&direction.y==0){
            GetComponentInChildren<SpriteRenderer>().sprite=hole;
        }else{
            GetComponentInChildren<SpriteRenderer>().sprite=enemy;
        }
*/    }

    void Update () {
        sum += Time.deltaTime;
        if (sum >= interval){
            moveEnemy ();
            sum = 0;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.forward,out hit,Mathf.Infinity) ){
            if (hit.collider.tag == "PLAYER"){
                if(direction.x==0&&direction.y==0){
                    Sound.Instance.Play(0, (int)Sound.soundEvents.HOLE);
                }else{
                    Sound.Instance.Play(0, (int)Sound.soundEvents.DEATH);
                }
                GameManager.Instance.PlayerDied ();
            }             
        }
    }

    void flip () {
        direction *= -1;
    }

    void moveEnemy () {
        RaycastHit hit;
        bool shouldFlip = !LevelLoader.Instance.LoadedLevel.insideGrid(position + direction);
        
        if (Physics.Raycast(transform.position + direction.ToVector3(), Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "WALL" || hit.collider.tag == "BOX") {
                shouldFlip = true;
             }
        }

        if (shouldFlip){
            flip ();
            bool outOfBounds = !LevelLoader.Instance.LoadedLevel.insideGrid(position + direction);
            if (outOfBounds)
                return;
            RaycastHit hit2;
            if (Physics.Raycast(transform.position + direction.ToVector3(), Vector3.forward, out hit2, Mathf.Infinity))
                if (hit2.collider.tag == "WALL" || hit2.collider.tag == "BOX")
                   return;
        }

        position += direction;
        transform.position += direction.ToVector3();
    }

}
