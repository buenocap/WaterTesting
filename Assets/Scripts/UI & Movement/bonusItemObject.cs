using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    static SaveObject saveData;
    private GameObject player;

    /// <summary>
    /// When bonus item is selected, it increases the move speed of the player and is destroyed
    /// </summary>
    private void OnMouseDown()
    {
        Destroy(gameObject);

        player = GameObject.Find("Player");

        // increase player movement speed
        player.GetComponent<Movement>().increaseWalkSpeed(2);
    }    

    
    
}
