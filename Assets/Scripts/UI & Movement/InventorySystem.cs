/*Christian Cerezo */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem: MonoBehaviour
{
    public static InventorySystem instance = null;

    //stores item list
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    // item list
    public List<InventoryItem> inventory { get; private set; }

    public UnityEvent onInventoryChangedEvent = new UnityEvent();

    private void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        onInventoryChangedEvent.Invoke();
    }

    private void Start()
    {
        // Inventory UI is updated at start
        onInventoryChangedEvent.Invoke();
        Debug.Log("Inventory Change Event Invoked at Start()");
    }

    /// <summary>
    /// Item is added to the item list
    /// </summary>
    /// <param name="referenceData"></param>
    public void Add(InventoryItemData referenceData)
    {
        Debug.Log("Item Added Successfully");
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }

        onInventoryChangedEvent.Invoke();
        Debug.Log("Inventory Change Event Invoked at Add()");
    }

    /// <summary>
    /// Item is removed from the item list
    /// </summary>
    /// <param name="referenceData"></param>
    public void Remove(InventoryItemData referenceData)
    {
        Debug.Log("Item removed successfully");
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        onInventoryChangedEvent.Invoke();
        Debug.Log("Inventory Change Event Invoked at Remove()");
    }
}
