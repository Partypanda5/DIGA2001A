using UnityEngine;
public class Testing : MonoBehaviour
{
    public InventoryItem item;
    void Start()
    {
        Debug.Log("Item: " + item.itemName);
        Debug.Log("Description: " + item.description);
        Debug.Log("Value: " + item.value);
    }
}


