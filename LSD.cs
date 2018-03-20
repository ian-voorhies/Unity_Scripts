using UnityEngine;
using UnityEngine.UI;
public class LSD:MonoBehaviour{
    public bool enable=true,auto=true;
    public type renderMode=type.SpriteRenderer;
    public enum type{Image,SpriteRenderer,Text,TextMesh}
    public Color[]colors={Color.blue,Color.green,Color.yellow,Color.red,Color.white};
    public float alpha=1.0f,delay=0.0f,duration=0.0f;
    private Text text;
    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private Image image;
    private Color color,startColor;
    private int index=0;
    private float time=0.0f;
    private bool started=false,delayed=true,paused=false,done=false,infinite=true;
    private void Start(){if(auto)go();}
    private void Update(){if(started&&!delaying()&&!isPaused()&&!isDone())onUpdate();}
    private void onGo(){
        if(duration>0)infinite=false;
        switch(renderMode){
            case type.Image:
                image=GetComponent<Image>();
                startColor=image.color;
                break;
            case type.SpriteRenderer:
                spriteRenderer=GetComponent<SpriteRenderer>();
                startColor=spriteRenderer.color;
                break;
            case type.Text:
                text=GetComponent<Text>();
                startColor=text.color;
                break;
            case type.TextMesh:
                textMesh=GetComponent<TextMesh>();
                startColor=textMesh.color;
                break;
        }
        color=new Color(startColor.r,startColor.g,startColor.b,alpha);
        started=true;
    }
    private void onUpdate(){
        if(!infinite){
            time+=Time.deltaTime;
            if(time>=duration){
                done=true;
                setColor(startColor);
                return;
            }
        }
        color=colors[index];
        color.a=alpha;
        setColor(color);
        if(index>=colors.Length-1)index=0;
        else index++;
    }
    private void setColor(Color newColor){
        switch(renderMode){
            case type.Image:if(image!=null)image.color=newColor;break;
            case type.SpriteRenderer:if(spriteRenderer!=null)spriteRenderer.color=newColor;break;
            case type.Text:if(text!=null)text.color=newColor;break;
            case type.TextMesh:if(textMesh!=null)textMesh.color=newColor;break;
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
