
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    //movement mask sets what the player can click on
    public LayerMask movementMask;

    //Keeps track of what we are focused on
    public Interactable focus;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move to mouse click
                motor.MoveToPoint(hit.point);

                //Stop focusing on any objects
                removeFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus) // we must check whether the player was already focused on something
        {
            if (focus != null)
                focus.onDefocused();
            focus = newFocus;
            motor.followTarget(newFocus);
        }

        
        newFocus.onFocused(transform);
        
    }
    private void removeFocus()
    {
        if (focus != null)
            focus.onDefocused();
        focus = null;
        motor.stopFollowingTarget();
    }


}
