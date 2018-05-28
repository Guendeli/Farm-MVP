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

    public FarmWeaponType equippedWeaponType { get; private set; }
	// Use this for initialization
	void Start () {
        meleeControl = GetComponent<vCollectMeleeControl>();
        if(rootBone.childCount == 0)
        {
            equippedWeaponType = FarmWeaponType.Damage;
        }
        characterUIWindow.OnAddedItem += CreateAndEquip;
	}
	

    void CreateAndEquip(IEnumerable<InventoryItemBase> items, uint amout, bool camefromCollection)
    {
        var weaponSlot = characterUIWindow.Select(o => o.item != null && o.item.category.name == "Weapon");
        if (weaponSlot != null)
        {
            Debug.Log(weaponSlot.GetType());
        }
        StartCoroutine(DelayedEquip(0.2f));
    }

    IEnumerator DelayedEquip(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rootBone.childCount > 0)
        {
            GameObject weapon = rootBone.GetChild(0).gameObject;
            if (weapon != null && weapon.tag == "Weapon")
            {
                vCollectableStandalone collectable = weapon.GetComponentInChildren<vCollectableStandalone>();
                equippedWeaponType = weapon.GetComponent<WeaponType>().GetWeaponType();
                meleeControl.HandleCollectableInput(collectable);
            } else
            {
                // Unarmed Deals Damage
                equippedWeaponType = FarmWeaponType.Damage;
            }
        }
    }


}
