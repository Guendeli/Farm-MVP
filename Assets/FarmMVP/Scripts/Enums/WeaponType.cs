using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmWeaponType { Axe, PickAxe, Damage}
public class WeaponType : MonoBehaviour {

    [SerializeField]
    private FarmWeaponType weaponType;

    public FarmWeaponType GetWeaponType()
    {
        return weaponType;
    }
}
