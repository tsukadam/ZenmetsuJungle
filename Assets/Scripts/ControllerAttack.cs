using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAttack : MonoBehaviour
{
    //当たり判定を伴う行動を持つ全てのCharaにアタッチする
    //全ての攻撃パターンを持つ。パターンとはWeaponの生成と、Weaponからの情報の受け取りである
    //PlayerやEnemyの指示でパターンを発動する

    public string WeaponTypeDetail;
    public GameObject WeaponPrefab;
    public GameObject Canvas;
    private IEnumerator Routine;

    private void SetCanvas()
    {
        Canvas = GameObject.Find("CanvasMain");
    }
        public void EquipWeapon(string TypeDetail)
    {
        SetWeaponTypeDetail(TypeDetail);
        LoadWeaponPrefab();
    }
    public void SetWeaponTypeDetail(string TypeDetail)
    {
        WeaponTypeDetail = TypeDetail;
    }
    public string GetWeaponTypeDetail()
    {
        return WeaponTypeDetail;
    }
    public void LoadWeaponPrefab()
    {
        if (WeaponTypeDetail == "None")
        {
            Debug.Log("WeaponTypeがNone");
            WeaponPrefab = null;
        }
        else if ((GameObject)Resources.Load("prefab/" + WeaponTypeDetail) == null)
        { Debug.Log("WeaponTypeがLoadできない"); }
        else
        {
            WeaponPrefab = (GameObject)Resources.Load("prefab/" + WeaponTypeDetail);
        }
    }
    public GameObject GetWeaponPrefab()
    {
        return WeaponPrefab;
    }

   

    public void AttackSimpleMake ()//進行方向にWeaponを出現させるだけの攻撃
    {
        MakeWeapon();
    }
    public void MakeWeapon()
    {
        if (WeaponPrefab != null)
        {
            InstantiateWeapon(WeaponPrefab);
        }
    }
   
    private void InstantiateWeapon(GameObject WeaponPrefab)
    {
        GameObject Weapon = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
        InitWeapon(Weapon);
    }

    private void InitWeapon(GameObject Weapon)
    {
        string AttackDirection = gameObject.GetComponent<ControllerCharaGeneral>().GetDirection();
        Weapon.GetComponent<ControllerWeapon>().Direction = AttackDirection;
        Weapon.GetComponent<ControllerWeapon>().SetTypeDetail(WeaponTypeDetail);
        Weapon.GetComponent<ControllerWeapon>().SetBoss(gameObject);
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
    }


}
