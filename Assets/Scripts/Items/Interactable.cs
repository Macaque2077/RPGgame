using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public Transform interactionTransform; //this will control the from where we interact with objects (i.e. may only want to interact with certain objects from infront

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false; //to stop interactions happening every frame

    public virtual void Interact() // actual method for interacting
    {
        //this method is meant to be overwritten
        //added if statement so it does not constantly write in the console
        if (!hasInteracted){
            Debug.Log("interacting with " + transform.name);
        }
    }
    //check whether the player has already interacted if not checks the players distance from the object
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    //for when focused on an object
    public void onFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    //for not focused
    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius); //show a sphere around the interactable to show interactive distance
    }
}
