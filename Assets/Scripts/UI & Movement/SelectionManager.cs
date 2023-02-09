/*Christian Cerezo */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectionManager : MonoBehaviour
{
    //The selectable tag is applied to objects that a player can select via raycast
    [SerializeField] private string selectableTag = "Selectable";

    [SerializeField] private List<Image> Crosshairs;
    Graphic m_Graphic;

    private Transform _selection;

    public FixedButton selectionButton;

    private void Start()
    {
        //check whether device is handheld before locking cursor
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Device is handheld");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Device is NOT handheld");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_selection != null)
        {
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        float rayLength = 500f;
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        RaycastHit hit;

        //crosshairs become green when a selectable item IS in focus
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            //defines what happens when the item that is collided with is "selectable"
            if (selection.CompareTag(selectableTag))
            {
                foreach (Image crossHair in Crosshairs)
                {
                    m_Graphic = crossHair.GetComponent<Graphic>();
                    m_Graphic.color = Color.green;
                }

                _selection = selection;
            }
            else
            {
                foreach (Image crossHair in Crosshairs)
                {
                    m_Graphic = crossHair.GetComponent<Graphic>();
                    m_Graphic.color = Color.white;
                }
            }

        }

        //defines what happens when an item is selected
        if ((Input.GetKeyDown("e") || selectionButton.Pressed) && _selection)
        {
            Debug.Log("Mouse is down, _selection is true");


            // checks if an item has an ItemObject component before deleting from items
            if (hit.transform.gameObject.TryGetComponent<ItemObject>(out ItemObject item))
            {
                //item is deleted from item list
                Debug.Log(item.referenceItem.displayName);
                item.OnHandlePickupItem();
            }
        }
    }
}
