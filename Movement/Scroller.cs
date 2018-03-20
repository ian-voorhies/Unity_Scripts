using UnityEngine;
public class Scroller:MonoBehaviour{
    public bool enable=true,auto=true;
    public xDir xDirection=xDir.Left;
    public enum xDir{Left,Right,Disabled};
    public float leftBound=0.0f,rightBound=0.0f,xSpeed=0.0f;
    public yDir yDirection=yDir.Disabled;
    public enum yDir{Down,Up,Disabled};
    public float downBound=0.0f,upBound=0.0f,ySpeed=0.0f;
    public zDir zDirection=zDir.Disabled;
    public enum zDir{Forward,Backward,Disabled};
    public float frontBound=0.0f,backBound=0.0f,zSpeed=0.0f,delay=0.0f;
    private Vector3 startPosition=Vector3.zero;
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
    private void onGo(){
        startPosition=transform.position;
        unpause();
    }
    private void onUpdate(){transform.position=new Vector3(updateX(transform.position.x),updateY(transform.position.y),updateZ(transform.position.z));}
    private float updateX(float x){
        if(xDirection==xDir.Left)return(x>leftBound)?updateCoord(x,xSpeed,false):rightBound;
        else if(xDirection==xDir.Right)return(x<rightBound)?updateCoord(x,xSpeed,true):leftBound;
        return x;
    }
    private float updateY(float y){
        if(yDirection==yDir.Down)return(y>downBound)?updateCoord(y,ySpeed,false):upBound;
        else if(yDirection==yDir.Up)return(y<upBound)?updateCoord(y,ySpeed,true):downBound;
        return y;
    }
    private float updateZ(float z){
        if(zDirection==zDir.Forward)return(z>frontBound)?updateCoord(z,zSpeed,false):backBound;
        else if(zDirection==zDir.Backward)return(z<backBound)?updateCoord(z,zSpeed,true):frontBound;
        return z;
    }
    private float updateCoord(float value,float speed,bool inc){return((inc)?+(speed*Time.deltaTime):-(speed*Time.deltaTime))+value;}
    public Vector3 getStartPosition(){return startPosition;}
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
