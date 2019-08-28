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

    public Animator ThisAnimFront;
    public Animator ThisAnimBack;

    private Vector2 MoveNow;
    public Vector2 MoveLast;


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
        OffCollider();
        yield return Routine2;
        yield return new WaitForSeconds(1.0f);

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
    public void EndHold()//player側から誘発
    {
        SetWithPlayerState("");
        InitTarget();
        ThisCharaGeneral.OnKnockBack();
        AnimateEndHold();
       OnCollider();
    }
    public void EndVore()//player側から誘発
    {
        SetWithPlayerState("");
        InitTarget();
        ThisCharaGeneral.OnKnockBack();
        AnimateEndVore();
        AnimateEndHold();
        OnCollider();
    }

    public void OffCollider()
    {
        ThisCharaGeneral.OffCollider();

        GameObject Body;
        if (gameObject.transform.Find("Body(Clone)"))
        {
            Body = gameObject.transform.Find("Body(Clone)").gameObject;
            Body.GetComponent<ControllerCharaGeneral>().MyDestroy();//Bodyの判定を消す
        }

    }

    public void OnCollider()
    {
        ThisCharaGeneral.OnCollider();

        ThisAttack.EquipWeapon(1, "Body");
        ThisAttack.AttackSimpleMake(1);
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
        OffCollider();

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
        ThisCharaGeneral.OffCollider();

        SetWithPlayerState("Voreing");
        yield return new WaitForSeconds(10.0f);

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
        if (gameObject.transform.Find("Body(Clone)"))
        {
            Body = gameObject.transform.Find("Body(Clone)").gameObject;
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
                    SetMoveNow(-1f, 1f);
                }
                else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad8))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal);
                    SetMoveNow(1f, 1f);
                }
                else if (Input.GetKey(KeyCode.Keypad4) & Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal * -1, MoveAmountOneKey * MoveDiagonal * -1);
                    SetMoveNow(-1f, -1f);
                }
                else if (Input.GetKey(KeyCode.Keypad6) & Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * MoveDiagonal, MoveAmountOneKey * MoveDiagonal * -1);
                    SetMoveNow(1f, -1f);
                }
                else if (Input.GetKey(KeyCode.Keypad4))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey * -1, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey * -1, 0);
                    SetMoveNow(-1f, 0);
                }
                else if (Input.GetKey(KeyCode.Keypad6))
                {
                    ThisCharaGeneral.AddPosition(MoveAmountOneKey, 0);
                    ThisCharaGeneral.AddDirection(MoveAmountOneKey, 0);
                    SetMoveNow(1f, 0);
                }
                else if (Input.GetKey(KeyCode.Keypad8))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey);
                    SetMoveNow(0, 1f);
                }
                else if (Input.GetKey(KeyCode.Keypad2))
                {
                    ThisCharaGeneral.AddPosition(0, MoveAmountOneKey * -1);
                    ThisCharaGeneral.AddDirection(0, MoveAmountOneKey * -1);
                    SetMoveNow(0, -1f);
                }
                else if (Input.GetKey(KeyCode.Keypad2)==false& Input.GetKey(KeyCode.Keypad4) == false & Input.GetKey(KeyCode.Keypad6) == false & Input.GetKey(KeyCode.Keypad8) == false)
                {

                    SetMoveNow(0, 0);
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

    public void AnimateHold()
    {
        ThisAnimFront.SetBool("Hold", true);
        ThisAnimBack.SetBool("Hold", true);
    }
    public void AnimateEndHold()
    {
        ThisAnimFront.SetBool("Hold", false);
        ThisAnimBack.SetBool("Hold", false);
    }

    public void AnimateVore()
    {
        ThisAnimFront.SetBool("Vore", true);
        ThisAnimBack.SetBool("Vore", true);
    }
    public void AnimateEndVore()
    {
        ThisAnimFront.SetBool("Vore", false);
        ThisAnimBack.SetBool("Vore", false);
    }


        public void AnimateWalk()
    {
        SetMoveLast();
        ThisAnimFront.SetFloat("Dir_X", MoveNow.x);
        ThisAnimFront.SetFloat("Dir_Y", MoveNow.y);
        ThisAnimFront.SetFloat("LastMove_X", MoveLast.x);
        ThisAnimFront.SetFloat("LastMove_Y", MoveLast.y);
        ThisAnimFront.SetFloat("Input", MoveNow.magnitude);
        ThisAnimBack.SetFloat("Dir_X", MoveNow.x);
        ThisAnimBack.SetFloat("Dir_Y", MoveNow.y);
        ThisAnimBack.SetFloat("LastMove_X", MoveLast.x);
        ThisAnimBack.SetFloat("LastMove_Y", MoveLast.y);
        ThisAnimBack.SetFloat("Input", MoveNow.magnitude);
    }
    public void SetMoveNow(float X, float Y)
    {
        MoveNow = new Vector2(X, Y);
    }
    public void SetMoveLast()
    {
        if (Mathf.Abs(MoveNow.x) > 0.5f& Mathf.Abs(MoveNow.y) <= 0.5f)
        {
            MoveLast.x = MoveNow.x;
            MoveLast.y = 0;
        }

        else if (Mathf.Abs(MoveNow.x) > 0.5f & Mathf.Abs(MoveNow.y) > 0.5f)
        {
            MoveLast.x = MoveNow.x;
            MoveLast.y = MoveNow.y;
        }

        else if (Mathf.Abs(MoveNow.x) <= 0.5f & Mathf.Abs(MoveNow.y) > 0.5f)
        {
            MoveLast.x = 0;
            MoveLast.y = MoveNow.y;
        }
        else if (Mathf.Abs(MoveNow.x) <= 0.5f & Mathf.Abs(MoveNow.y) <= 0.5f)
        {
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
        AnimateWalk();
    }

    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();
        ThisAttack = gameObject.GetComponent<ControllerAttack>();
        AddInfoToCharaGeneralAsEnemy();
        ThisAnimFront = this.transform.Find("ImageFront").gameObject.GetComponent<Animator>();
        ThisAnimBack = this.transform.Find("ImageBack").gameObject.GetComponent<Animator>();

    }
}
