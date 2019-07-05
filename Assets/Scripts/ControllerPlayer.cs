using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    //Playerにアタッチする
    //Player特有の行動を制御する

    private IEnumerator Routine;

    public int HitPoint;
    private float MoveAmountOneKey = 3f;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;

    public void AddHitPoint(int Amount){
        HitPoint += Amount;
        float KnockBackAmount = ThisCharaGeneral.DamagedKnockBackAmount;
        ThisCharaGeneral.DamagedKnockBack(KnockBackAmount);
    }

    private void CheckKey()
    {
        if (ThisCharaGeneral != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ThisCharaGeneral.AddPosition("X", MoveAmountOneKey * -1);
                ThisCharaGeneral.AddDirection("X", MoveAmountOneKey * -1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ThisCharaGeneral.AddPosition("X", MoveAmountOneKey);
                ThisCharaGeneral.AddDirection("X", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmountOneKey);
                ThisCharaGeneral.AddDirection("Y", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmountOneKey * -1);
                ThisCharaGeneral.AddDirection("Y", MoveAmountOneKey * -1);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ThisAttack.AttackSimpleMake();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ThisAttack.GetWeaponTypeDetail() != "GunBullet")
                {
                    ThisAttack.EquipWeapon("GunBullet");
                }
                else
                {
                    ThisAttack.EquipWeapon("RodSword");
                }
            }

        }

    }
    private void AddInfoToCharaGeneralAsPlayer()
    {
        HitPoint = 10;
        ThisCharaGeneral.SetCharaType("Player");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(1);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(1);
    }

    private void Start()
    {
            ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();
        ThisAttack = gameObject.GetComponent<ControllerAttack>();

        AddInfoToCharaGeneralAsPlayer();
    }
    private void Update()
    {
        CheckKey();
    }
}
