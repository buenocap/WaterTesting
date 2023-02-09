using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    

    private void OnMouseDown()
    {
        GetComponent<Animator>().Play("Open");
    }
}
