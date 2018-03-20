using UnityEngine;
public class Shrink:MonoBehaviour{
    public bool enable=true,auto=true,destroy=true;
    public dir Direction=dir.Both;
    public enum dir{Horizontal,Vertical,Both}
    public float speed=1.0f,delay=0.0f;
    private Vector3 scale;
    private float min=0.0f,time=0.0f;
    private bool started=false,delayed=true,paused=false,xDone=false,yDone=false;
	  private void Start(){if(auto)go();}
	  private void Update(){if(started&&!delaying()&&!isPaused())onUpdate();}
    private void onGo(){
        if(!enable)return;
        if(delay>0)delayed=false;
        scale=new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        if(Direction==dir.Horizontal)yDone=true;
        else if(Direction==dir.Vertical)xDone=true;
        started=true;
    }
	  private void onUpdate(){
        if(isDone()){
            if(destroy)Destroy(gameObject);
            return;
        }
        if(!xDone){
            if(scale.x>min)scale.x-=Time.deltaTime*speed;
            else{
                scale.x=min;
                xDone=true;
            }
        }
        if(!yDone){
            if(scale.y>min)scale.y-=Time.deltaTime*speed;
            else{
                scale.y=min;
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
