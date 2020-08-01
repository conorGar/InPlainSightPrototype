using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PickUp : MonoBehaviour
{

    //Player presses 'Pickup' button when in a certain radius of an Apple to add it to their 'points'
    bool canPickUp;
    Item_Apple currentHighlightedItem;


    private void Update()
    {
        //if spacebar is pressed and there is a value for currentHighlightedItem
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp)
        {
            Pickup();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !canPickUp)
        {
            SceneManager.Instance.pickupPrompt.gameObject.SetActive(true);
            currentHighlightedItem = other.GetComponent<Item_Apple>();
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            SceneManager.Instance.pickupPrompt.gameObject.SetActive(false);
            canPickUp = false;
        }
    }


    void Pickup()
    {
        SceneManager.Instance.RemoveItem(currentHighlightedItem);
        //Increase points or whatever...
        canPickUp = false;
    }
}
