using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Chest has been interacted with");
    }
}