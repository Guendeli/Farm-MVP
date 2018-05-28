using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Invector.vMelee;
using Invector.vCharacterController.vActions;
using Devdog.InventoryPro;

public class WeaponEquipListener : MonoBehaviour {

    private vCollectMeleeControl meleeControl;
    public GameObject targetWeapon;
    public CharacterUI characterUIWindow;
    public Transform rootBone;

    private EquipmentType weaponCategory;
	// Use this for initialization
	void Start () {
        meleeControl = GetComponent<vCollectMeleeControl>();
        characterUIWindow.OnAddedItem += CreateAndEquip;
	}
	

    void CreateAndEquip(IEnumerable<InventoryItemBase> items, uint amout, bool camefromCollection)
    {
         var weaponSlot = characterUIWindow.Select(o => o.item != null && o.item.category.name == "Weapon");
        Debug.Log(weaponSlot.GetType());
        StartCoroutine(delayedEquip(0.2f));
    }

    IEnumerator delayedEquip(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rootBone.childCount > 0)
        {
            GameObject weapon = rootBone.GetChild(0).gameObject;
            if (weapon != null && weapon.tag == "Weapon")
            {
                vCollectableStandalone collectable = weapon.GetComponentInChildren<vCollectableStandalone>();
                meleeControl.HandleCollectableInput(collectable);
            } else
            {
                weaponCategory = null;
            }
        }
    }


}
