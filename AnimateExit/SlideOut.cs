using UnityEngine;
public class SlideOut:MonoBehaviour{
    public bool enable=true,auto=true,destroy=true;
    public xDir xDirection=xDir.Left;
    public enum xDir{Left,Right,Center}
    public yDir yDirection=yDir.Center;
    public enum yDir{Top,Bottom,Center}
    public float speed=1.0f,delay=0.0f;
    private Vector3 goal,pos;
    private bool started=false,delayed=true,paused=false,xDone=false,yDone=false;
    private float time=0.0f;
    public void Start(){if(auto)go();}
    public void Update(){if(started&&!delaying()&&!isPaused())onUpdate();}
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        onGo();
    }
    private void onGo(){
        if(!enable)return;
        if(delay>0)delayed=false;
        Vector3 size=GetComponent<Collider2D>().bounds.size;
        goal=transform.position;
        float dist=(goal-Camera.main.transform.position).z;
        if(xDirection==xDir.Left)goal.x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x)-size.x;
        else if(xDirection==xDir.Right)goal.x=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x*-1)+size.x;
        else xDone=true;
        if(yDirection==yDir.Top)goal.y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y*-1)+size.y;
        else if(yDirection==yDir.Bottom)goal.y=(Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y)-size.y;
        else yDone=true;
        pos=transform.position;
        started=true;
    }
    private void onUpdate(){
        if(isDone()){
            if(destroy)Destroy(gameObject);
            return;
        }
        if(!xDone){
            if(xDirection==xDir.Left){
                if(pos.x>goal.x)pos.x-=speed*Time.deltaTime;
                else{
                    pos.x=goal.x;
                    xDone=true;
                }
            }
            else{
                if(pos.x<goal.x)pos.x+=speed*Time.deltaTime;
                else{
                    pos.x=goal.x;
                    xDone=true;
                }
            }
        }
        if(!yDone){
            if(yDirection==yDir.Top){
                if(pos.y<goal.y)pos.y+=speed*Time.deltaTime;
                else{
                    pos.y=goal.y;
                    yDone=true;
                }
            }
            else{
                if(pos.y>goal.y)pos.y-=speed*Time.deltaTime;
                else{
                    pos.y=goal.y;
                    yDone=true;
                }
            }
        }
        transform.position=pos;
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
