/*Christian Cerezo */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Retrieves scavenger hunt items from InventorySystem
/// </summary>
public class InventoryUI : MonoBehaviour
{
    // Slot that appears in Item Bar/Full Item List
    public GameObject m_slotPrefab;

    public GameObject FullInventory;

    public int MaxSlotsBar;
    private int totalSlotsBar;
    // Start is called before the first frame update
    private void Start()
    {

        Debug.Log("InventoryUI start() initialized");
        OnUpdateInventory();
        InventorySystem.instance.onInventoryChangedEvent.AddListener(OnUpdateInventory);
    }

    /// <summary>
    /// When the item list is updated, gameObjects in both the item bar and 
    /// full item list are destroyed to prepare for an item list refresh
    /// </summary>
    private void OnUpdateInventory()
    {
        Debug.Log("OnUpdateInventory() called");
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in FullInventory.transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }


    public void DrawInventory()
    {
        totalSlotsBar = 0;
        foreach(InventoryItem item in InventorySystem.instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    /// <summary>
    /// Item is added to either the item bar or full item list as determined by the 
    /// MaxSlotsBar value: when no space exists in the item bar, items are placed
    /// set as children of the full item list
    /// </summary>
    /// <param name="item"> item to be set as either a child of the item bar or full item list</param>
    public void AddInventorySlot(InventoryItem item)
    {
        

        if (totalSlotsBar < MaxSlotsBar)
        {
            GameObject obj = Instantiate(m_slotPrefab);
            obj.transform.SetParent(transform, false);

            Slot slot = obj.GetComponent<Slot>();
            slot.Set(item);
            totalSlotsBar += 1;
        }
        else
        {
            GameObject obj = Instantiate(m_slotPrefab);
            obj.transform.SetParent(FullInventory.transform, false);

            Slot slot = obj.GetComponent<Slot>();
            slot.Set(item);
        }
    }
}
