using UnityEngine;
public class SelfDestruct:MonoBehaviour{
    public bool enable=true,auto=true;
    public float delay=0.0f;
    private float time=0.0f;
    private bool started=false;
    void Start(){if(auto)go();}
    public void go(){
        if(!enable)return;
        started=true;
    }
    void Update(){
        if(!started||!enable)return;
        time+=Time.deltaTime;
        if(time>=delay)Destroy(gameObject);
    }
}
