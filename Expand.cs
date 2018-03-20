using UnityEngine;
public class Expand:MonoBehaviour{
    public bool enable=true,auto=true;
    public dir Direction=dir.Both;
    public enum dir{Horizontal,Vertical,Both}
    public float maxSize=1.0f,speed=1.0f,delay=0.0f;
    private Vector3 scale;
    private float min=0.0f,maxX,maxY,time=0.0f;
    private bool started=false,delayed=true,paused=false,xDone=false,yDone=false;
	private void Start(){if(auto)go();}
	private void Update(){if(started&&!delaying()&&!isPaused()&&!isDone())onUpdate();}
    private void onGo(){
        scale=new Vector3(min,min,transform.localScale.z);
        maxX=transform.localScale.x;
        maxY=transform.localScale.y;
        if(Direction==dir.Horizontal){
            scale.y=maxY;
            maxX*=maxSize;
            yDone=true;
        }
        else if(Direction==dir.Vertical){
            scale.x=maxX;
            maxY*=maxSize;
            xDone=true;
        }
        transform.localScale=scale;
        started=true;
    }
    private void onUpdate(){
        if(!xDone){
            if(scale.x<maxX)scale.x+=Time.deltaTime*speed;
            else{
                scale.x=maxX;
                xDone=true;
            }
        }
        if(!yDone){
            if(scale.y<maxY)scale.y+=Time.deltaTime*speed;
            else{
                scale.y=maxY;
                yDone=true;
            }
        }
        transform.localScale=scale;
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
