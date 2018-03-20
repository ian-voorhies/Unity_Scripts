using UnityEngine;
public class Anchor:MonoBehaviour{
    public bool enable=true,auto=true;
    public xPos xPosition=xPos.Left;
    public enum xPos{Left,Right,Center}
    public yPos yPosition=yPos.Top;
    public enum yPos{Top,Bottom,Center}
    public float offsetScaleX=0.1f,offsetScaleY=0.1f,delay=0.0f;
    private float time=0.0f;
    private bool delayed=true,paused=false,done=false;
    private void Start(){if(auto)go();}
    private void FixedUpdate(){if(!delaying()&&!isPaused()&&!isDone())onUpdate();}
    private void onGo(){
        float x=0.0f,y=0.0f,dist=(transform.position-Camera.main.transform.position).z;
        Vector3 size=GetComponent<Collider2D>().bounds.size;
        if(xPosition==xPos.Right)x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x*-1)-(size.x*offsetScaleX+size.x/2);
        else if(xPosition==xPos.Left)x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x)+(size.x*offsetScaleX+size.x/2);
        if(yPosition==yPos.Top)y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y*-1)-(size.y*offsetScaleY+size.y/2);
        else if(yPosition==yPos.Bottom)y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y)+(size.y*offsetScaleY+size.y/2);
        transform.position=new Vector3(x,y,transform.position.z);
        done=true;
    }
    private void onUpdate(){onGo();}
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        else onGo();
    }
    public void pause(){paused=true;}
    public void unpause(){paused=false;}
    public bool isPaused(){return paused;}
    public bool isDone(){return done;}
    private bool delaying(){
        if(delayed)return false;
        time+=Time.deltaTime;
        if(time>=delay){
            time=0.0f;
            delayed=true;
        }
        return true;
    }
}
