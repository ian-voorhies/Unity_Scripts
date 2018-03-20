using UnityEngine;
public class Rotater:MonoBehaviour{
    public bool enable=true,auto=true;
    public float xSpeed=0.0f,ySpeed=0.0f,zSpeed=0.0f,delay=0.0f;
    private bool delayed=true,paused=true;
    private float time=0.0f;
    void Start(){if(auto)go();}
    void Update(){
	if(!enable)return;
	if(!delaying()&&!isPaused())onUpdate();
    }
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        onGo();
    }
    private void onGo(){unpause();}
    private void onUpdate(){transform.Rotate(Time.deltaTime*xSpeed,Time.deltaTime*ySpeed,Time.deltaTime*zSpeed);}
    public void pause(){paused=true;}
    public void unpause(){paused=false;}
    public bool isPaused(){return paused;}
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
