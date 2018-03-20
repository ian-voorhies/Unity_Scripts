using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene:MonoBehaviour{
    public bool enable=true,auto=true;
    public string scene;
    public float delay=0.0f;
    private bool delayed=true,done=false;
    private float time=0.0f;
    void Start(){if(auto)go();}
    void Update(){
        if(!enable)return;
	if(!delaying()&&!done)onUpdate();
    }
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        else onGo();
    }
    private void onGo(){switchScene(scene);}
    private void onUpdate(){switchScene(scene);}
    private void switchScene(string scene){
        SceneManager.LoadScene(scene);
        done=true;
    }
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
