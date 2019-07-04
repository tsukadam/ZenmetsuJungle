using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAttack : MonoBehaviour
{
    //当たり判定を伴う行動を持つ全てのCharaにアタッチする
    //PlayerやEnemyの指示でWeaponを生成する
    //攻撃の詳細はWeaponが持つ。攻撃パターンはPlayerやEnemyが持つ

    public string WeaponTypeDetail;
    public GameObject WeaponPrefab;
 
    public void EquipWeapon(string TypeDetail)
    {
        SetWeaponTypeDetail(TypeDetail);       
        LoadWeaponPrefab();
    }
    public void InstantiateWeapon(GameObject WeaponPrefab)
    {
        GameObject Weapon = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
        Weapon.transform.SetParent(gameObject.transform);
        Vector3 WeaponPosition = new Vector3(0, 0, 0);

        if (Weapon.GetComponent<ControllerWeapon>() == null& gameObject.GetComponent<ControllerCharaGeneral>())
        {
            Debug.Log("WeaponのPrefabにControllerWeaponがアタッチされていない。または攻撃者にControllerCharaGeneralがアタッチされていない");
        }
        else
        {
            string AttackDirection = gameObject.GetComponent<ControllerCharaGeneral>().GetDirection();
            Weapon.GetComponent<ControllerWeapon>().Direction = AttackDirection;
            Weapon.GetComponent<ControllerWeapon>().SetTypeDetail(WeaponTypeDetail);
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
    public void LoadWeaponPrefab()
    {
        if (WeaponTypeDetail == "None")
        { Debug.Log("SeaponTypeがNone");
            WeaponPrefab = null;
        }
        else if ((GameObject)Resources.Load("prefab/" + WeaponTypeDetail) == null)
        { Debug.Log("SeaponTypeがLoadできない"); }
        else
        {
            WeaponPrefab = (GameObject)Resources.Load("prefab/" + WeaponTypeDetail);
        }
    }
    public GameObject GetWeaponPrefab()
    {
        return WeaponPrefab;
    }


    public void MakeWeapon()
    {
        if (WeaponPrefab!=null) { 
            InstantiateWeapon(WeaponPrefab);
        }
    }

    public void SetWeaponTypeDetail(string TypeDetail)
    {
        WeaponTypeDetail = TypeDetail;
    }
    public string GetWeaponTypeDetail()
    {
        return WeaponTypeDetail;
    }

}
