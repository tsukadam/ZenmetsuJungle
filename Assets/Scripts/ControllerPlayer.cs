using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    //Playerにアタッチする
    //Player特有の行動を制御する

    private IEnumerator Routine;

    public int HitPoint;
    public int MentalPoint;
    public int GachaPoint;
    public int SwitchHold=0;
    private float MoveAmountOneKey = 3f;
    private float MoveDiagonal = 7f / 10;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;
    public GameObject HoldingEnemy;

    public void TryHold(GameObject Weapon)//将来的に、防御などはここで挟み込む
    {
        SwitchHold = 1;
        ThisCharaGeneral.OnStun();
        HoldingEnemy = Weapon.GetComponent<ControllerWeapon>().GetBoss();
        HoldingEnemy.GetComponent<ControllerCharaGeneral>().OnStun();
    }

        public void TryDamageHitPoint(GameObject Weapon)//将来的に、防御などはここで挟み込む
    {
        int DamageAmount = Weapon.GetComponent<ControllerWeapon>().DamageAmount;
        AddHitPoint(DamageAmount*-1);
        float KnockBackAmount = Weapon.GetComponent<ControllerWeapon>().KnockBackAmount;

        string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;
        ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);
    }

    public void AddHitPoint(int Amount){
        HitPoint += Amount;
    }

    public void AddMentalPoint(int Amount)
    {
        MentalPoint += Amount;
    }
    public void AddGachaPoint(int Amount)
    {
        GachaPoint += Amount;
    }

    private void CheckHold()
    {
        if (SwitchHold == 1&GachaPoint>100) {
            SwitchHold = 0;
            ThisCharaGeneral.OffStun();
            HoldingEnemy.GetComponent<ControllerCharaGeneral>().OffStun();
            GachaPoint = 0;

        }
    }
        private void CheckKey()
    {
        if (SwitchHold == 0)
        {
            if (ThisCharaGeneral != null)
            {
                if (Input.GetKey(KeyCode.LeftArrow) & Input.GetKey(KeyCode.UpArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal);
                }
                else if (Input.GetKey(KeyCode.RightArrow) & Input.GetKey(KeyCode.UpArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                }
                else if (Input.GetKey(KeyCode.LeftArrow) & Input.GetKey(KeyCode.DownArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                }
                else if (Input.GetKey(KeyCode.RightArrow) & Input.GetKey(KeyCode.DownArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * -1, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * -1, 0);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey, 0);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey * -1);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey * -1);
                }
                else { }



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
        else if (SwitchHold == 1)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.DownArrow))
            {
                AddGachaPoint(5);
            }

        }
    }
    private void AddInfoToCharaGeneralAsPlayer()
    {
        HitPoint = 10;
        MentalPoint = 10;
        GachaPoint = 0;
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

        CheckHold();
    }
}
