using UnityEngine;
public class LightSwitch : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("LightSwitch has been interacted with");
    }
}
