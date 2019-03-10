

using UnityEngine;

public abstract class InteractableObject: MonoBehaviour
{
   

    public GameObject selectionCircle;

    public void Highlight() { selectionCircle.SetActive(true); }

    public void StopHighlight() { selectionCircle.SetActive(false); }

    public abstract void Interact();
   
}
