/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;


    private void Start()
    {
        //GameObject.Find("Inventory");

        //upon being initialized, item is added to the scavenger hunt item list
        InventorySystem.instance.Add(referenceItem);
        Debug.Log("Item Add() Attempt");
        
    }
    

    /// <summary>
    /// When an item is selected, it's data is removed from the InventorySystem and corresponding gameObject is destroyed
    /// </summary>
    public void OnHandlePickupItem()
    {
        Debug.Log(referenceItem.displayName);
        InventorySystem.instance.Remove(referenceItem);

        //Remove item from save file
        for (int i = 0; i < GameManager.SaveData.Items.Count; i++)
        {
            if (GameManager.SaveData.Items[i].objectID == gameObject.name)
            {
                GameManager.SaveData.Items.RemoveAt(i);
            }
        }

        SaveManager.Save(GameManager.SaveData);
        Destroy(gameObject);

        //check if no more scavenger hunt items exist - Christian
        if(GameManager.SaveData.Items.Count == 0)
        {
            //if all items have been found, trigger end - Christian
            GameManager.isEndGame = true;
        }
    }
}
