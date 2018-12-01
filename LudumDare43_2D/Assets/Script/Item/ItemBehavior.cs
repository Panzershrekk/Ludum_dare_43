using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public Item item;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<PlayerBehavior>().inventory.GetNumberOfItem() < Inventory.numItemSlots)
            {
                col.GetComponent<PlayerBehavior>().PickUpItem(item);
                Destroy(this.gameObject);
            }
        }
    }
}
