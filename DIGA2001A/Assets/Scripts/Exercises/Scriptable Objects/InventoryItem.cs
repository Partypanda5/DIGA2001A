using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    [TextArea] public string description; //allows for a larger text input in inspector
    public int value;
}