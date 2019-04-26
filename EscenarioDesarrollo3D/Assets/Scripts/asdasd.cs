using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class asdasd : MonoBehaviour {
    
    public PlayableDirector playableDirector;
    
    public bool active = false;
    public float timeLineDuration;
    public bool lul;

    // Use this for initialization
    void Start ()
    {
        timeLineDuration = 0;
        playableDirector.Play();
        active = true;
        lul = true;
    }
	
	// Update is called once per frame
	void Update ()
    {        
        if (active)
        {
            
            timeLineDuration += Time.deltaTime;
        }
        if(timeLineDuration > 15.5f && lul)
        {
            active = false;
            
            lul = false;
        }
        if (timeLineDuration > 14.5f && lul)
        {
            
            playableDirector.Stop();
            
        }
    }
}
