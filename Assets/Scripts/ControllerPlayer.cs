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
    public string WithEnemyState;//Enemyとの連動状態。捕獲、飲み込み、獣姦、ネバ玉など、ウエポン以外でアニメ制御が必要なもの
    private float MoveAmountOneKey = 3f;
    private float MoveDiagonal = 7f / 10;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;
    public GameObject AttackingEnemy;


    public void CheckAttackingTrriger()//被アタック中は他の敵の判定を受けない（ピヨリ除く）
    {
        if (GetWithEnemyState() != ""& GetWithEnemyState() != "Dizzying")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

        public void CheckMentalPoint()
    {
        if (MentalPoint <= 0)
        //気力ゼロになった時、Holding中ならVoreingへ移行
        //平常時ならピヨリ
        {
            if (GetWithEnemyState() == "") {
                TryDizzy();
            }
            else if (GetWithEnemyState() == "Holding") {
                TryVoreAfterHolding();
               
            }
        }

    }

    public void TryTouch(GameObject Weapon)//接触攻撃
    {
        if(GetWithEnemyState() == "Dizzying")//ピヨリならvore処理
        {
            float KnockBackAmount = Weapon.GetComponent<ControllerWeapon>().KnockBackAmount;
            string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;
            ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);//ノックバックは先にしておく
            TryVore(Weapon);
        }
        else//それ以外はダメージ処理
        {
            TryAddDamage(Weapon);
            AttackingEnemy = Weapon.GetComponent<ControllerWeapon>().GetBoss();
            AttackingEnemy.GetComponent<ControllerEnemy>().InitTarget();

        }
    }

        public void TryDizzy()
    {
        SetWithEnemyState("Dizzying");
    }

    public void TryVoreAfterHolding()
    {
            SetGachaPoint(0);
            SetWithEnemyState("Voreing");
            ThisCharaGeneral.OffKnockBack();
            if (AttackingEnemy != null)
            {
                AttackingEnemy.GetComponent<ControllerEnemy>().AttackVoreAfterHolding();
                AttackingEnemy.GetComponent<ControllerCharaGeneral>().OffKnockBack();
            }
    }


    public void TryVore(GameObject Weapon)
    {
        SetGachaPoint(0);
        SetWithEnemyState("Voreing");
        ThisCharaGeneral.OffKnockBack();
        AttackingEnemy = Weapon.GetComponent<ControllerWeapon>().GetBoss();
        AttackingEnemy.GetComponent<ControllerEnemy>().AttackVore();

        if (AttackingEnemy != null)
        {
            AttackingEnemy.GetComponent<ControllerCharaGeneral>().OffKnockBack();
        }
    }

    

    public void ShieldKnockBack(GameObject Weapon)
    {
        float KnockBackAmount = 70;
        string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;
        ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);

    }
    public void TryHold(GameObject Weapon)
    {
        if (gameObject.transform.Find("Shield(Clone)"))
        {
            ShieldKnockBack(Weapon);
        }
        else
        {
            SetWithEnemyState("Holding");
            ThisCharaGeneral.OffKnockBack();
            AttackingEnemy = Weapon.GetComponent<ControllerWeapon>().GetBoss();
            AttackingEnemy.GetComponent<ControllerCharaGeneral>().OffKnockBack();

        }
    }

    public void TryAddDamage(GameObject Weapon)
    {

        int HitDamageAmount = Weapon.GetComponent<ControllerWeapon>().HitDamageAmount;
        int MentalDamageAmount = Weapon.GetComponent<ControllerWeapon>().MentalDamageAmount;
        float KnockBackAmount = Weapon.GetComponent<ControllerWeapon>().KnockBackAmount;
        string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;

        if (gameObject.transform.Find("Shield(Clone)"))
        {
            ShieldKnockBack(Weapon);
        }
        else {
            AddHitPoint(HitDamageAmount * -1);
            AddMentalPoint(MentalDamageAmount * -1);
            ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);
        }
    }

    public void AddHitPoint(int Amount) {
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
    public void SetGachaPoint(int Amount)
    {
        GachaPoint = Amount;
    }

    private void EndHold() {
        SetWithEnemyState("");
        ThisCharaGeneral.OnKnockBack();
        GachaPoint = 0;
        if (AttackingEnemy != null)
        {
            AttackingEnemy.GetComponent<ControllerEnemy>().SetWithPlayerState("");
            AttackingEnemy.GetComponent<ControllerEnemy>().InitTarget();
            AttackingEnemy.GetComponent<ControllerCharaGeneral>().OnKnockBack();
        }
        AttackingEnemy = null;
    }
    private void EndDizzy()
    {
        AddMentalPoint(100);
        SetWithEnemyState("");
        ThisCharaGeneral.OnKnockBack();
        GachaPoint = 0;
    }
    private void EndVore()
    {
        AddMentalPoint(100);
        SetWithEnemyState("");
        ThisCharaGeneral.OnKnockBack();
        GachaPoint = 0;
        if (AttackingEnemy != null)
        {
            AttackingEnemy.GetComponent<ControllerEnemy>().SetWithPlayerState("");
            AttackingEnemy.GetComponent<ControllerEnemy>().InitTarget();
            AttackingEnemy.GetComponent<ControllerCharaGeneral>().OnKnockBack();
        }
        AttackingEnemy = null;
    }

    private void CheckGachaPoint()
    {
        if (GachaPoint>100) {
            if(GetWithEnemyState() == "Holding") {
                EndHold();
            }
            else if (GetWithEnemyState() == "Dizzying"){
                EndDizzy();
            }
            else if (GetWithEnemyState() == "Voreing")
            {
                EndVore();
            }
        }

    }
    private void CheckKey()
    {
        if(Time.timeScale != 0) {
            if (GetWithEnemyState() == "")
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
                    ThisAttack.EquipWeapon(0, "Check");

                    ThisAttack.AttackSimpleMake(0);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    ThisAttack.EquipWeapon(1, "GunBullet");

                    ThisAttack.AttackSimpleMake(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {

                    ThisAttack.EquipWeapon(2, "Shield");
                    ThisAttack.AttackSimpleMake(2);

                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                }
            }
        else if (GetWithEnemyState() == "Holding"| GetWithEnemyState() == "Dizzying"| GetWithEnemyState() == "Voreing")
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.DownArrow))
                {
                    AddGachaPoint(5);
                 }

            }
        }
    }
    public void SetWithEnemyState(string State)
    {
        WithEnemyState = State;
    }
    public string GetWithEnemyState()
    {
        return WithEnemyState;
    }
    private void AddInfoToCharaGeneralAsPlayer()
    {
        HitPoint = 100;
        MentalPoint = 50;
        GachaPoint = 0;
        ThisCharaGeneral.SetCharaType("Player");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(true);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(true);
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
        CheckMentalPoint();
        CheckGachaPoint();
        CheckAttackingTrriger();
    }
}
