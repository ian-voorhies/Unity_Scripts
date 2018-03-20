using UnityEngine;
using UnityEngine.UI;
public class FadeOut:MonoBehaviour{
    public bool enable=true,destroy=true,auto=false;
    public type renderMode=type.SpriteRenderer;
    public enum type{Image,SpriteRenderer,Text,TextMesh}
    public float speed=3.0f,delay=0.0f;
    private Text text;
    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private Image image;
    private Color color;
    private float alpha=1.0f,time=0.0f;
    private bool started=false,delayed=true,paused=false,done=false;
	  private void Start(){if(auto)go();}
	  private void Update(){if(started&&!delaying()&&!isPaused())onUpdate();}
    public void onGo(){
        switch(renderMode){
            case type.Image:
                image=GetComponent<Image>();
                color=image.color;
                break;
            case type.SpriteRenderer:
                spriteRenderer=GetComponent<SpriteRenderer>();
                color=spriteRenderer.color;
                break;
            case type.Text:
                text=GetComponent<Text>();
                color=text.color;
                break;
            case type.TextMesh:
                textMesh=GetComponent<TextMesh>();
                color=textMesh.color;
                break;
        }
        started=true;
    }
    private void onUpdate(){
        if(isDone()){
            if(destroy)Destroy(gameObject);
            return;
        }
	    alpha-=speed*Time.deltaTime;
        switch(renderMode){
            case type.Image:if(image!=null)color=image.color;break;
            case type.SpriteRenderer:if(spriteRenderer!=null)color=spriteRenderer.color;break;
            case type.Text:if(text!=null)color=text.color;break;
            case type.TextMesh:if(textMesh!=null)color=textMesh.color;break;
        }
        color.a=alpha;
        setColor(color);
        if(alpha<=0){
            done=true;
            if(destroy)Destroy(gameObject);
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
