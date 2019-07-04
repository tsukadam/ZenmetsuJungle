using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    //Playerにアタッチする
    //Player特有の行動を制御する

    private IEnumerator Routine;

    public float MoveAmountOneKey=10f;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;



    private void SetPlayerWeapon(string WeaponType)
    {
        ThisAttack.EquipWeapon(WeaponType);
    }
    private void MakePlayerWeapon()
    {
        ThisAttack.MakeWeapon();
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
                MakePlayerWeapon();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ThisAttack.GetWeaponTypeDetail() != "GunBullet")
                {
                    SetPlayerWeapon("GunBullet");
                }
                else
                {
                    SetPlayerWeapon("RodSword");
                }
            }

        }

    }
    private void AddInfoToCharaGeneralAsPlayer()
    {
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
