using UnityEngine;
using UnityEngine.UI;
public class TextWave:MonoBehaviour{
    public bool enable=true,auto=true,ignoreSpaces=true;
    public type renderMode=type.Text;
    public enum type{Text,TextMesh}
    public dir Direction=dir.Right;
    public enum dir{Right,Left}
    public Color color=new Color(0.0f,0.0f,0.0f,1.0f);
    public float frameRate=0.1f,delay=0.0f,duration=0.0f;
    private Text text;
    private TextMesh textMesh;
    private string opencolor,closecolor="</color>",startText;
    private int frame=0;
    private float time=0.0f,frameTime=0.0f;
    private bool started=false,delayed=true,paused=false,done=false,infinite=true;
    private void Start(){if(auto)go();}
    private void Update(){if(started&&!delaying()&&!isPaused()&&!isDone())onUpdate();}
    private void onGo(){
        if(duration>0)infinite=false;
        switch(renderMode){
            case type.Text:
                text=GetComponent<Text>();
                startText=text.text;
                break;
            case type.TextMesh:
                textMesh=GetComponent<TextMesh>();
                startText=textMesh.text;
                break;
        }
        opencolor="<color=#"+((Color32)color).r.ToString("X2")+((Color32)color).g.ToString("X2")+((Color32)color).b.ToString("X2")+((Color32)color).a.ToString("X2")+">";
        started=true;
        if(Direction==dir.Left){frame=startText.Length-1;}
    }
    private void onUpdate(){
        if(!infinite){
            time+=Time.deltaTime;
            if(time>=duration){
                done=true;
                setText(startText);
                return;
            }
        }
	if(frameTime<frameRate)frameTime+=Time.deltaTime;
        else{
            frameTime=0.0f;
            if(Direction==dir.Right){
                if(frame>=startText.Length-1)frame=0;
                else frame++;
                if(ignoreSpaces){
                    while(startText[frame]==' ')
                        frame++;
                }
            }
            else{
                if(frame<=0)frame=startText.Length-1;
                else frame--;
                if(ignoreSpaces){
                    while(startText[frame]==' ')
                        frame--;
                }
            }
        }
        setText(startText.Insert(frame+1,closecolor).Insert(frame,opencolor));
    }
    private void setText(string newText){
        switch(renderMode){
            case type.Text:if(text!=null)text.text=newText;break;
            case type.TextMesh:if(textMesh!=null)textMesh.text=newText;break;
        }
    }
    public void go(){
        if(!enable)return;
        if(delay>0)delayed=false;
        onGo();
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
