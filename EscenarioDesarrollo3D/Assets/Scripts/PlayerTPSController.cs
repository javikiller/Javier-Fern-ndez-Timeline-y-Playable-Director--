using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTPSController : MonoBehaviour {

    public Camera cam;
    public UnityEvent onInteractionInput;
    private InputData input;
    private CharacterAnimBasedMovement characterMovement;

    public bool onInteractionZone { get; set; }


    void Start ()
    {
        characterMovement = GetComponent<CharacterAnimBasedMovement>();
	}
	    
	void Update ()
    {
        input.GetInput();

        if (onInteractionZone)
        {
            onInteractionInput.Invoke();
        }

        characterMovement.MoveCharacter(input.hMovement, input.vMovement, cam, input.jump, input.dash, input.press);

        Cursor.visible = false;
    }
}
