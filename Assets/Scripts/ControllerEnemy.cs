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
    public string WithPlayerState;//Enemyとの連動状態。捕獲、飲み込み、獣姦など、ウエポン以外でアニメ制御が必要なもの

    private float MoveAmountOneKey = 3f;
    private float MoveDiagonal = 7f / 10;
    private ControllerCharaGeneral ThisCharaGeneral;
    private ControllerAttack ThisAttack;
    public GameObject Target;
    private IEnumerator Routine;

    private void AttackHold()//捕獲    
    {
        Routine = null;
        Routine = AttackHoldCoroutine();
        StartCoroutine(Routine);
    }

    IEnumerator AttackHoldCoroutine()
    {
        ThisAttack.EquipWeapon(0, "Hold");
        IEnumerator Routine2 = ThisAttack.AttackSimpleMakeCoroutine(0);
        StartCoroutine(Routine2);
        yield return Routine2;

        if (Target != null)
        {
            SetWithPlayerState("Holding");
            while (Target != null & Target?.GetComponent<ControllerPlayer>().GetWithEnemyState() == "Holding")
            {
                Target.GetComponent<ControllerPlayer>().AddMentalPoint(-10);
                yield return new WaitForSeconds(2.0f);
            }
        }
    }

    public void AttackVoreAfterHolding()//Holdingで気力ゼロになった時player側から誘発
    {
        Routine = null;
        Routine = AttackVoreAfterHoldingCoroutine();
        StartCoroutine(Routine);
    }

    IEnumerator AttackVoreAfterHoldingCoroutine()
    {
        SetWithPlayerState("Voreing");
        if (Target != null)
        {
            SetWithPlayerState("Voreing");
            while (Target != null&Target?.GetComponent<ControllerPlayer>().GetWithEnemyState() == "Voreing")
            {
                Target.GetComponent<ControllerPlayer>().AddHitPoint(-5);
                yield return new WaitForSeconds(2.0f);
            }
        }
        yield return null;
    }



    public void AttackVore()//気力ゼロ接触時にplayer側から誘発
    {
        Routine = null;
        Routine = AttackVoreCoroutine();
        StartCoroutine(Routine);
    }
    IEnumerator AttackVoreCoroutine()
    {
        SetWithPlayerState("Voreing");
        if (Target != null)
        {
            SetWithPlayerState("Voreing");
            while (Target != null & Target?.GetComponent<ControllerPlayer>().GetWithEnemyState() == "Voreing")
            {
                Target.GetComponent<ControllerPlayer>().AddHitPoint(-5);
                yield return new WaitForSeconds(2.0f);
            }
        }
        yield return null;
    }


    private void AttackSearchRush()//サーチ＋体当たり
    {
        Routine = null;
        Routine = AttackSearchRushCoroutine();
        StartCoroutine(Routine);
    }
    IEnumerator AttackSearchRushCoroutine()
    {
        ThisAttack.EquipWeapon(0, "Search");
        IEnumerator Routine2 = ThisAttack.AttackSimpleMakeCoroutine(0);
        StartCoroutine(Routine2);
        yield return Routine2;
        yield return new WaitForSeconds(0.1f);
        if (Target != null)
        {
                //Enemyクラスが持つTargetObjectの座標に突進する。
                Vector3 TargetPosition = Target.GetComponent<ControllerCharaGeneral>().GetPosition();
                float X = TargetPosition.x;
                float Y = TargetPosition.y;

                iTween.MoveTo(gameObject, iTween.Hash(
                            "x", X,
                            "y", Y,
                            "time", 1f,
                            "easeType", "easeInOutBack",
                            "isLocal", true
                        ));
            }
        GameObject Body;
        if (gameObject.transform.Find("Body"))
        {
            Body = gameObject.transform.Find("Body").gameObject;
            Body.GetComponent<ControllerWeapon>().BodyEnable();//成否に関わらずBodyの判定を再生しておく
        }
        InitTarget();

    }



    public void SetWithPlayerState(string State)
    {
        WithPlayerState=State;
    }
    public string GetWithPlayerState()
    {
        return WithPlayerState;
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

    public void TryAddDamage(GameObject Weapon)
    {
        int DamageAmount = Weapon.GetComponent<ControllerWeapon>().HitDamageAmount;       
        float KnockBackAmount = Weapon.GetComponent<ControllerWeapon>().KnockBackAmount;
        string KnockBackDirection = Weapon.GetComponent<ControllerWeapon>().Direction;
        if (gameObject.transform.Find("Shield(Clone)"))
        {
            KnockBackAmount = 70;
            ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);
        }
        else
        {
            AddHitPoint(DamageAmount * -1);
            ThisCharaGeneral.DamagedKnockBack(KnockBackAmount, KnockBackDirection);
        }

    }

    public void AddHitPoint(int Amount)
    {
        HitPoint += Amount;
    }

    private void CheckKey()
    {
        if (Time.timeScale != 0)
        {
            if (ThisCharaGeneral != null)
            {
                if (Input.GetKey(KeyCode.Keypad4) & Input.GetKey(KeyCode.Keypad8))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal);
                }
                else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad8))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                }
                else if (Input.GetKey(KeyCode.Keypad4) & Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                }
                else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                }
                else if (Input.GetKey(KeyCode.Keypad4))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * -1, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * -1, 0);
                }
                else if (Input.GetKey(KeyCode.Keypad6))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey, 0);
                }
                else if (Input.GetKey(KeyCode.Keypad8))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey);
                }
                else if (Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey * -1);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey * -1);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AttackSearchRush();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    AttackHold();

                }
                if (Input.GetKeyDown(KeyCode.E))
                {

                    ThisAttack.EquipWeapon(0, "GunBullet");
                    ThisAttack.AttackSimpleMake(0);
                }
            }
        }
    }

    private void AddInfoToCharaGeneralAsEnemy()
    {
        //将来的にはスポーンコントローラで指定する
        HitPoint = 50;
        ThisCharaGeneral.SetCharaType("Enemy");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(true);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(true);

        ThisAttack.EquipWeapon(1, "Body");
        ThisAttack.AttackSimpleMake(1);
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
