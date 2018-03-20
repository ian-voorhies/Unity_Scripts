using UnityEngine;
using UnityEngine.UI;
public class Rainbow:MonoBehaviour{
    public bool enable=true,auto=true;
    public type renderMode=type.SpriteRenderer;
    public enum type{Image,SpriteRenderer,Text,TextMesh}
    public float alpha=1.0f,speed=3.25f,delay=0.0f,duration=0.0f;
    private Text text;
    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private Image image;
    private Color color,startColor;
    private byte currentColor=0;
    private float upper=1.0f,lower=0.0f,red=1.0f,green=0.0f,blue=0.0f,time=0.0f;
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
        color=new Color(red,green,blue,alpha);
        started=true;
    }
	  private void onUpdate(){
        if(!started||paused||delaying()||done)return;
        if(!infinite){
            time+=Time.deltaTime;
            if(time>=duration){
                done=true;
                setColor(startColor);
                return;
            }
        }
        color.r=red;
        color.g=green;
        color.b=blue;
        setColor(color);
        switch(currentColor){
            case 0:
                if(green>=upper){
                    currentColor++;
                    return;
                }
                green+=Time.deltaTime*speed;
                break;
            case 1:
                if(red<=lower){
                    currentColor++;
                    return;
                }
                red-=Time.deltaTime*speed;
                break;
            case 2:
                if(blue>=upper){
                    currentColor++;
                    return;
                }
                blue+=Time.deltaTime*speed;
                break;
            case 3:
                if(green<=lower){
                    currentColor++;
                    return;
                }
                green-=Time.deltaTime*speed;
                break;
            case 4:
                if(red>=upper){
                    currentColor++;
                    return;
                }
                red+=Time.deltaTime*speed;
                break;
            case 5:
                if(blue<=lower){
                    currentColor=0;
                    return;
                }
                blue-=Time.deltaTime*speed;
                break;
        }
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
