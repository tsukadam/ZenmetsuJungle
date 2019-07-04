using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWeapon : MonoBehaviour
{
    //全てのWeapon（当たり判定）Prefabにアタッチする
    //全ての攻撃の詳細を持つ
    //アタッチされたPrefabの名前と設定されたWeaponTypeにより、どの攻撃処理をどう行うか決定する
    //接触した相手や自分の属性判定を行い、移動やダメージ処理を行う

    public string WeaponType;//Gun飛び道具、Rod近接武器、Rush突進、Grab掴みなどの大枠
    public string WeaponTypeDetail;//GunBulletなど一意に決まる名前
    public string Direction;
    public float MoveAmount;
    public int ExistTime;//存在時間、0=無限
    public int DamageAmount;
    private IEnumerator Routine;
    public GameObject Parent;//Weaponを発生させた親
    public string ParentType;

    private ControllerCharaGeneral ThisCharaGeneral;


    public void SetParentType()
    {
        ParentType = Parent.GetComponent<ControllerCharaGeneral>().CharaType;
    }
    public string GetParentType()
    {
        return ParentType;
    }
    public void SetParent()
    {
        Parent = transform.parent.gameObject;
    }
    public GameObject GetParent()
    {
        return Parent;
    }

    public void RodAction()
    {
        //トリガー接触したら、自分がPlayer、相手がEnemyならば、ダメージを与える。
        {
            if (WeaponType == "Rod")
            {
                if (CheckTrigger())
                {
                    if (ThisCharaGeneral.ObjectTriggerNow != null)
                    {
                        GameObject Target = ThisCharaGeneral.ObjectTriggerNow.gameObject;
                      
                        if (Target.GetComponent<ControllerCharaGeneral>())
                        {
                            string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
                          
                            if (TargetType == "Enemy" & ParentType == "Player")
                            {
                                Target.GetComponent<ControllerEnemy>().HitPoint -= DamageAmount;
                                gameObject.GetComponent<BoxCollider2D>().enabled=false;
                            }
                        }
                    }
                }
                else
                {
                }
            }
        }
    }
    public void GunAction()
    //接触したら、自分がPlayer、相手がEnemyならば、ダメージを与えて消える。
    //接触時以外は移動する。
    {
        if (WeaponType == "Gun")
        {
            if (CheckTrigger())
            {
                if (ThisCharaGeneral.ObjectTriggerNow != null)
                {
                    GameObject Target = ThisCharaGeneral.ObjectTriggerNow.gameObject;
                   
                    if (Target.GetComponent<ControllerCharaGeneral>())
                    {
                        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
                       
                        if (TargetType == "Enemy" & ParentType == "Player")
                        {
                            Target.GetComponent<ControllerEnemy>().HitPoint -= DamageAmount;                          
                        }
                    }
                }

                GunDestroy();
            }
            else
            {
                GunMove();
            }
        }
    }

        public void SetTypeDetail(string Detail)
    {
        WeaponTypeDetail = Detail;
    }
    public string GetTypeDetail()
    {
        return WeaponTypeDetail;
    }

    private void GunMove()
    {
        if (WeaponType == "Gun")
        {
            if (MoveAmount == 0)
            {
                Debug.Log("GunのMoveAmountが設定されていない。10を代入");
                MoveAmount = 10;

            }
            if (Direction == "Left")
            {
                ThisCharaGeneral.AddPosition("X", MoveAmount * -1.0f);
            }
            else if (Direction == "Right")
            {
                ThisCharaGeneral.AddPosition("X", MoveAmount);
            }
            else if (Direction == "Up")
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmount);
            }
            else if (Direction == "Down")
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmount*-1.0f);
            }

        }
    }

    IEnumerator RodDestroyCoroutine()
{
            yield return new WaitForSeconds(ExistTime * 1.0f);
        ThisCharaGeneral.MyDestroy();
}
    private void RodDestroy()
    {
    if (WeaponType == "Rod")
    {
        Routine = null;
        Routine = RodDestroyCoroutine();
        StartCoroutine(Routine);
    }
    }
    private void GunDestroy()
    {
        if (WeaponType == "Gun" & CheckTrigger())
        {
            ThisCharaGeneral.MyDestroy();
        }

    }

    private bool CheckCollision()
    {
        bool Return= ThisCharaGeneral.StateCollision != "Exit";
        return Return;
    }
    private bool CheckTrigger()
    {
        bool Return = ThisCharaGeneral.StateTrigger != "Exit";
        return Return;
    }

    private void CheckType() {
        if (WeaponType != "Rod" & WeaponType != "Gun" & WeaponType != "Grab" & WeaponType != "Rush")
        {
            Debug.Log(WeaponType+"は定義されていないWeaponType");
            ThisCharaGeneral.MyDestroy();
        }        
    }
    private void AddInfoToCharaGeneralAsWeapon()
    {
        ThisCharaGeneral.SetCharaType("Weapon");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(0);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(0);
    }


    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();

        AddInfoToCharaGeneralAsWeapon();
        SetParent();
        SetParentType();
        CheckType();
        RodDestroy();
    }

    private void Update()
    {
        GunAction();
        RodAction();
    }
}
