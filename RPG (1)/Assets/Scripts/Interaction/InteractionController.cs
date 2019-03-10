using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private InteractableObject selectedObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            InteractableObject objectController = collision.gameObject.GetComponent<InteractableObject>();
            objectController.Highlight();

            selectedObject = objectController;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            InteractableObject objectController = collision.gameObject.GetComponent<InteractableObject>();
            objectController.StopHighlight();

            selectedObject = null;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedObject.Interact();
        }
    }
}
