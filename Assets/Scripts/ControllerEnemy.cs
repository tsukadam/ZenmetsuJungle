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
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;
    public GameObject Target;
    private IEnumerator Routine;

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


    public void AddHitPoint(int Amount)
    {
        HitPoint += Amount;
        float KnockBackAmount = ThisCharaGeneral.DamagedKnockBackAmount;
        ThisCharaGeneral.DamagedKnockBack(KnockBackAmount);
    }

    private void CheckKey()
    {
        if (ThisCharaGeneral != null)
        {
            if (Input.GetKey(KeyCode.Keypad4))
            {
                ThisCharaGeneral.AddPosition("X", MoveAmountOneKey * -1);
                ThisCharaGeneral.AddDirection("X", MoveAmountOneKey * -1);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                ThisCharaGeneral.AddPosition("X", MoveAmountOneKey);
                ThisCharaGeneral.AddDirection("X", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.Keypad8))
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmountOneKey);
                ThisCharaGeneral.AddDirection("Y", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.Keypad2))
            {
                ThisCharaGeneral.AddPosition("Y", MoveAmountOneKey * -1);
                ThisCharaGeneral.AddDirection("Y", MoveAmountOneKey * -1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                AttackSearchRush();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
               
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
