using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.InventoryPro;

public class DropItemBehaviour : MonoBehaviour
{
    public GameObject lootObject;
    
    public void OnDrop()
    {
        lootObject.transform.SetParent(null);
        lootObject.SetActive(true);
        Destroy(gameObject);
    }
    
}
