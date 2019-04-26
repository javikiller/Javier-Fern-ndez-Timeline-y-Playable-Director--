using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]

public class TimelineActivator : MonoBehaviour {

    public PlayableDirector playableDirector;
    public string playerTAG;
    public Transform interactionLocation;
    public bool autoActivate = false;

    public bool interact{get;set;}

    [Header("Activation Zone Events")]
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    [Header("Timeline Events")]
    public UnityEvent OnTimeLineStart;
    public UnityEvent OnTimeLineEnd;

    private bool isPlaying;
    private bool playerIsnide;
    private Transform playerTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerIsnide = true;
            playerTransform = other.transform;
            OnPlayerEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerIsnide = false;
            playerTransform = null;
            OnPlayerExit.Invoke();
        }
    }

    private void PlayTimeLine()
    {
        if(playerTransform && interactionLocation)
        {
            playerTransform.SetPositionAndRotation(interactionLocation.position, interactionLocation.rotation);
        }

        if (autoActivate)
        {
            playerIsnide = false;
        }

        if (playableDirector)
        {
            playableDirector.Play();
        }

        isPlaying = true;
        interact = false;

        StartCoroutine(waitForTimeLinetoEnd());
    }

    private IEnumerator waitForTimeLinetoEnd()
    {
        OnTimeLineStart.Invoke();

        float timeLineDuration = (float)playableDirector.duration;

        while (timeLineDuration > 0)
        {
            timeLineDuration -= Time.deltaTime;
            yield return null;

        }
        if(timeLineDuration < 0)
        {
            playableDirector.Stop();
        }

        isPlaying = false;

        OnTimeLineEnd.Invoke();
    }
    // Use this for initialization
    void Start ()
    {
        playableDirector.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(playerIsnide && !isPlaying)
        {
            if(interact || autoActivate)
            {
                PlayTimeLine();
            }
        }
	}
}
