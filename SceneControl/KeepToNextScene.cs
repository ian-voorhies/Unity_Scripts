using UnityEngine;
public class KeepToNextScene:MonoBehaviour{
    public bool enable=true;
    void Start(){}
    void Update(){}
    void Awake(){if(enable)DontDestroyOnLoad(gameObject);}
}
