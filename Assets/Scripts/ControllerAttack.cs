using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAttack : MonoBehaviour
{
    public string RodType;
    public string GunType;
    public GameObject RodPrefab;
    public GameObject GunPrefab;

    public void SetWeapon(string GunType,string RodType)
    {
        SetGunType(GunType);
        SetRodType(RodType);
        SetGun();
        SetRod();
    }
    public void InstantiateWeapon(GameObject WeaponPrefab)
    {
        GameObject Weapon = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
        Weapon.transform.SetParent(gameObject.transform);
        Vector3 WeaponPosition = new Vector3(0, 0, 0);

        if (Weapon.GetComponent<ControllerWeapon>() == null& gameObject.GetComponent<ControllerMoveGeneral>())
        {
            Debug.Log("WeaponのPrefabにControllerWeaponがアタッチされていない。または攻撃者にControllerMoveGeneralがアタッチされていない");
        }
        else
        {
            string AttackDirection = gameObject.GetComponent<ControllerMoveGeneral>().GetDirection();
            Weapon.GetComponent<ControllerWeapon>().Direction = AttackDirection;
            if (AttackDirection == "Left") { WeaponPosition = new Vector3(-40, 0, 0); }
            else if (AttackDirection == "Right") { WeaponPosition = new Vector3(40, 0, 0); }
            else if (AttackDirection == "Up") { WeaponPosition = new Vector3(0, 80, 0); }
            else if (AttackDirection == "Down") { WeaponPosition = new Vector3(0, -80, 0); }
        }

        if (Weapon.GetComponent<RectTransform>() == null)
        {
            Debug.Log("WeaponのPrefabにRectTransformがアタッチされていない");
        }
        else
        {
            Weapon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Weapon.GetComponent<RectTransform>().localPosition = WeaponPosition;
        }

    }
    public void SetGun()
    {
        if (GunType == "None")
        { Debug.Log("GunTypeがNone");
            GunPrefab = null;
        }
        else if ((GameObject)Resources.Load("prefab/" + GunType) == null)
        { Debug.Log("GunTypeがLoadできない"); }
        else
        {
            GunPrefab = (GameObject)Resources.Load("prefab/" + GunType);
        }
    }

    public void SetRod()
    {
        if (RodType == "None")
        { Debug.Log("RodTypeがNone");
            RodPrefab = null;
        }
        else if ((GameObject)Resources.Load("prefab/" + RodType) == null)
        { Debug.Log("RodTypeがLoadできない"); }
        else
        {
            RodPrefab = (GameObject)Resources.Load("prefab/" + RodType);
        }

    }
    public void MakeGun()
    {
        if (GunPrefab!=null) { 
            InstantiateWeapon(GunPrefab);
        }
    }
    public void MakeRod() { 
        if (RodPrefab!=null) {
            InstantiateWeapon(RodPrefab);
        }
    }

    public void SetRodType(string Type)
    {
        RodType = Type;
    }
    public string GetRodType()
    {
        return RodType;
    }
    public void SetGunType(string Type)
    {
        GunType = Type;
    }
    public string GetGunType()
    {
        return GunType;
    }

}
