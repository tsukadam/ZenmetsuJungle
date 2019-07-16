using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAttack : MonoBehaviour
{
    //当たり判定を伴う行動を持つ全てのCharaにアタッチする
    //全ての攻撃パターンを持つ。パターンとはWeaponの生成と、Weaponからの情報の受け取りである
    //PlayerやEnemyの指示でパターンを発動する

    public string[] WeaponTypeDetail;
    public GameObject[] WeaponPrefab;
    public GameObject Canvas;
    private IEnumerator Routine;

    private void InitArray()
    {
        WeaponTypeDetail = new string[3] { "","", "" };
        WeaponPrefab = new GameObject[3];
    }

    private void SetCanvas()
    {
        Canvas = GameObject.Find("CanvasMain");
    }
        public void EquipWeapon(int Slot, string TypeDetail)
    {
        SetWeaponTypeDetail(Slot, TypeDetail);
        LoadWeaponPrefab(Slot);
    }

    public void SetWeaponTypeDetail(int Slot, string TypeDetail)
    {
            WeaponTypeDetail[Slot] = TypeDetail;
    }

    public string GetWeaponTypeDetail(int Slot)
    {
        string Result = WeaponTypeDetail[Slot];
        return Result;
    }
    public void LoadWeaponPrefab(int Slot)
    {

        if (WeaponTypeDetail[Slot] == "None")
        {
            Debug.Log("WeaponTypeがNone");
            WeaponPrefab[Slot] = null;
        }
        else if ((GameObject)Resources.Load("prefab/" + WeaponTypeDetail[Slot]) == null)
        { Debug.Log("WeaponTypeがLoadできない"); }
        else
        {
            WeaponPrefab[Slot] = (GameObject)Resources.Load("prefab/" + WeaponTypeDetail[Slot]);
        }
    }
    public GameObject GetWeaponPrefab(int Slot)
    {
        GameObject Result = WeaponPrefab[Slot];
        return Result;
    }


    public IEnumerator AttackSimpleMakeCoroutine(int Slot)//進行方向にWeaponを出現させるだけの攻撃
    {
        MakeWeapon(Slot);
        yield return null;
    }



    public void AttackSimpleMake (int Slot)//進行方向にWeaponを出現させるだけの攻撃
    {
        MakeWeapon(Slot);
    }
    public void MakeWeapon(int Slot)
    {

        if (WeaponPrefab[Slot] != null)
        {
            InstantiateWeapon(Slot, WeaponPrefab[Slot]);
        }
    }
   
    private void InstantiateWeapon(int Slot, GameObject WeaponPrefab)
    {
        GameObject Weapon = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
        InitWeapon(Slot, Weapon);
    }

    private void InitWeapon(int Slot, GameObject Weapon)
    {
        string AttackDirection = gameObject.GetComponent<ControllerCharaGeneral>().GetDirection();
        Weapon.GetComponent<ControllerWeapon>().Direction = AttackDirection;
        Weapon.GetComponent<ControllerWeapon>().SetTypeDetail(WeaponTypeDetail[Slot]);
        Weapon.GetComponent<ControllerWeapon>().SetBoss(gameObject);
        Weapon.GetComponent<ControllerWeapon>().SetSlot(Slot);
        SetWeaponStructurallyParent(Weapon);
        SetWeaponPosition(Weapon);
        SetWeaponScale(Weapon);

    }

    private void SetWeaponStructurallyParent(GameObject Weapon)
    {
        if (Weapon.GetComponent<ControllerWeapon>().WeaponType == "Gun") {
            Weapon.transform.SetParent(Canvas.transform);
        }
        else {
            Weapon.transform.SetParent(gameObject.transform);
        }
    }
    private void SetWeaponScale(GameObject Weapon)
    {
        Weapon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    private void SetWeaponPosition(GameObject Weapon)
    {
        Vector3 WeaponPosition=Weapon.GetComponent<ControllerWeapon>().GetWeaponPosition();
       Weapon.GetComponent<ControllerCharaGeneral>().SetPosition(WeaponPosition);

    }

    private void Start()
    {
        SetCanvas();
        InitArray();
    }


}
