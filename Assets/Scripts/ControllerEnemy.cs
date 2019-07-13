using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemy : MonoBehaviour
{
    //Enemyにアタッチする
    //Enemy特有の行動を制御する
    //全てのEnemyの行動を持つ
    //スポーン時にEnemyTypeを与えられ、あるいはPrefabの名前から規定する
    //EnemyTypeに従って外見や行動を決める

    public int HitPoint;
    public string EnemyType;

    private float MoveAmountOneKey = 3f;
    private float MoveDiagonal = 7f / 10;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;
    public GameObject Target;
    private IEnumerator Routine;

    private void AttackHold()//捕獲    
    {
        ThisAttack.EquipWeapon("Hold");
        ThisAttack.AttackSimpleMake();

    }
    private void AttackSearchRush()//サーチ＋体当たり
    {
        ThisAttack.EquipWeapon("Search");
        ThisAttack.AttackSimpleMake();

        Routine = null;
        Routine = AttackSearchRushCoroutine();
        StartCoroutine(Routine);

    }

    IEnumerator AttackSearchRushCoroutine()
    {
        int Count = 0;
        while (Target== null&Count<30)
        {
            yield return new WaitForSeconds(0.1f);
            Count++;
        }
        if (Target != null)
        {
            ThisAttack.EquipWeapon("Rush");
            ThisAttack.AttackSimpleMake();
        }

    }


    public void SetTarget(GameObject TargetObj)
    {
        Target = TargetObj;

    }
    public void InitTarget()
    {
        Target = null;

    }

    public GameObject GetTarget()
    {
        return Target;
    }

    public void TryDamageHitPoint(GameObject Weapon)//将来的に、防御などはここで挟み込む
    {
        int DamageAmount = Weapon.GetComponent<ControllerWeapon>().DamageAmount;
        AddHitPoint(DamageAmount * -1);
        float KnockBackAmount = Weapon.GetComponent<ControllerWeapon>().KnockBackAmount;
        string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;
        ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);

    }

    public void AddHitPoint(int Amount)
    {
        HitPoint += Amount;
    }

    private void CheckKey()
    {
        if (ThisCharaGeneral != null)
        {
             if (Input.GetKey(KeyCode.Keypad4)& Input.GetKey(KeyCode.Keypad8))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal*-1, MoveAmountOneKey* MoveDiagonal);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal*-1, MoveAmountOneKey* MoveDiagonal);
            }
            else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad8))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey* MoveDiagonal, MoveAmountOneKey* MoveDiagonal);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey* MoveDiagonal, MoveAmountOneKey* MoveDiagonal);
            }
            else if (Input.GetKey(KeyCode.Keypad4) & Input.GetKey(KeyCode.Keypad2))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey* MoveDiagonal *- 1);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey* MoveDiagonal * -1);
            }
            else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad2))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey* MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey* MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey * -1,0);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey * -1,0);
            }
            else if (Input.GetKey(KeyCode.Keypad6))
            {
                ThisCharaGeneral.AddPosition(MoveAmountOneKey,0);
                ThisCharaGeneral.AddDirection(MoveAmountOneKey,0);
            }
            else if (Input.GetKey(KeyCode.Keypad8))
            {
                ThisCharaGeneral.AddPosition(0,MoveAmountOneKey);
                ThisCharaGeneral.AddDirection(0,MoveAmountOneKey);
            }
            else if (Input.GetKey(KeyCode.Keypad2))
            {
                ThisCharaGeneral.AddPosition(0, MoveAmountOneKey * -1);
                ThisCharaGeneral.AddDirection(0, MoveAmountOneKey * -1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                AttackSearchRush();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                AttackHold();

            }

        }

    }

    private void AddInfoToCharaGeneralAsEnemy()
    {
        //将来的にはスポーンコントローラで指定する
        HitPoint = 10;
        ThisCharaGeneral.SetCharaType("Enemy");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(1);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(1);
    }

    private void Update()
    {
        CheckKey();
    }

    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();
        ThisAttack = gameObject.GetComponent<ControllerAttack>();
        AddInfoToCharaGeneralAsEnemy();
    }
}
