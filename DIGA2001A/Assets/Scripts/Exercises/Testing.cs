using UnityEngine;
public class Testing : MonoBehaviour
{
    public GameObject[] interactables; 
    void Start()
    {
        foreach (GameObject myobject in interactables)
        {
            IInteractable interactable = myobject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}


