using UnityEngine;
public class SlideIn:MonoBehaviour{
    public bool enable=true,auto=true;
    public xDir xDirection=xDir.Left;
    public enum xDir{Left,Right,Center}
    public yDir yDirection=yDir.Center;
    public enum yDir{Top,Bottom,Center}
    public float speed=1.0f,delay=0.0f;
    private Vector3 start,pos;
    private float time=0.0f;
    private bool started=false,delayed=true,paused=false,xDone=false,yDone=false;
    private void Start(){if(auto)go();}
    private void Update(){if(started&&!delaying()&&!isPaused()&&!isDone())onUpdate();}
    private void onGo(){
        Vector3 size=GetComponent<Collider2D>().bounds.size;
	start=transform.position;
        float dist=(start-Camera.main.transform.position).z;
        float x=start.x,y=start.y;
        if(xDirection==xDir.Left)x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x)-size.x;
        else if(xDirection==xDir.Right)x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x*-1)+size.x;
        else xDone=true;
        if(yDirection==yDir.Top)y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y*-1)+size.y;
        else if(yDirection==yDir.Bottom)y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y)-size.y;
        else yDone=true;
        pos=new Vector3(x,y,transform.position.z);
        transform.position=pos;
        started=true;
    }
    void onUpdate(){
        if(!xDone){
            if(xDirection==xDir.Left){
                if(pos.x<start.x)pos.x+=speed*Time.deltaTime;
                else{
                    pos.x=start.x;
                    xDone=true;
                }
            }
            else{
                if(pos.x>start.x)pos.x-=speed*Time.deltaTime;
                else{
                    pos.x=start.x;
                    xDone=true;
                }
            }
        }
        else{
            if(yDirection==yDir.Top){
                if(pos.y>start.y)pos.y-=speed*Time.deltaTime;
                else{
                    pos.y=start.y;
                    yDone=true;
                }
            }
            else{
                if(pos.y<start.y)pos.y+=speed*Time.deltaTime;
                else{
                    pos.y=start.y;
                    yDone=true;
                }
            }
        }
        transform.position=pos;
    }
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        onGo();
    }
    public void pause(){paused=true;}
    public void unpause(){paused=false;}
    public bool isPaused(){return paused;}
    public bool isDone(){return xDone&&yDone;}
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
