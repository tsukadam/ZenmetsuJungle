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
    public int HitDamageAmount;
    public int MentalDamageAmount;
    public int KnockBackAmount;
    public int Slot;
    private IEnumerator Routine;
    public GameObject Boss;//Weaponを発生させた者
    public string Team;

    private ControllerCharaGeneral ThisCharaGeneral;


    public void SetSlot(int Num)
    {
        Slot = Num;
    }
    public int GetSlot()
    {
        return Slot;
    }


    public void SetTeam()
    {
        Team = Boss.GetComponent<ControllerCharaGeneral>().CharaType;
    }
    public string GetTeam()
    {
        return Team;
    }
    public void SetBoss(GameObject BossObj)
    {
        Boss = BossObj;
    }
    public GameObject GetBoss()
    {
        return Boss;
    }


   
   
    private void TriggerBody(GameObject Target)
    {
        //自分がEnemy、相手がPlayerならば、接触攻撃する。
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Enemy" & TargetType == "Player")
        {
            Boss.GetComponent<ControllerEnemy>().SetTarget(Target);
            Target.GetComponent<ControllerPlayer>().TryTouch(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            BodyEnable();
        }
    }
    public void BodyEnable()
    {
        Routine = null;
        Routine = BodyEnabledCoroutine();
        StartCoroutine(Routine);
    }
    IEnumerator BodyEnabledCoroutine()//bodyの当たり判定は一定時間で回復
    {
        yield return new WaitForSeconds(1.0f);
        Boss.GetComponent<ControllerAttack>().EquipWeapon(1, "Body");
        Boss.GetComponent<ControllerAttack>().AttackSimpleMake(1);
        ThisCharaGeneral.MyDestroy();
    }

        private void TriggerHold(GameObject Target)
    {
        //自分がEnemy、相手がPlayerならば、Holdを開始し、自分のEnemyクラスにTargetObjectを送る
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Enemy" & TargetType == "Player")
        {
            Boss.GetComponent<ControllerEnemy>().SetTarget(Target);
            Target.GetComponent<ControllerPlayer>().TryHold(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }


    private void TriggerSearch(GameObject Target)
    {
        //トリガー接触したら、自分がEnemy、相手がPlayerならば、自分のEnemyクラスにTargetObjectを送る
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Enemy"& TargetType == "Player")
        {
            Boss.GetComponent<ControllerEnemy>().SetTarget(Target);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    private void TriggerCheck(GameObject Target)
    {
        //トリガー接触したら、自分がPlayer、相手がMassageならば、MassageにUIを表示させる
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Player" & TargetType == "Massage")
        {
           Target.GetComponent<ControllerMassage>().WriteMassage();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    private void TriggerRod(GameObject Target)
    {
        //自分がPlayer、相手がEnemyならば、ダメージを与える。
         string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
         if (Team == "Player"& TargetType == "Enemy")
         {
            Target.GetComponent<ControllerEnemy>().TryAddDamage(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
         }
        //自分がEnemy、相手がPlayerならば、接触攻撃する。
        else if (Team == "Enemy" & TargetType == "Player")
        {
            Boss.GetComponent<ControllerEnemy>().SetTarget(Target);
            Target.GetComponent<ControllerPlayer>().TryTouch(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DestroyGun();
        }
    }
    private void TrrigerGun(GameObject Target)
    //自分がPlayer、相手がEnemyならば、ダメージを与える。
    {
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Player"& TargetType == "Enemy")
        {
            Target.GetComponent<ControllerEnemy>().TryAddDamage(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DestroyGun();
        }
        //自分がEnemy、相手がPlayerならば、ダメージを与える。
        else if (Team == "Enemy" & TargetType == "Player")
        {
            Target.GetComponent<ControllerPlayer>().TryAddDamage(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DestroyGun();
        }
        else if (TargetType == "Weapon")
        {
        }
        else
        {
            DestroyGun();
        }
    }

    public Vector3 GetWeaponPosition()
    {
        string AttackDirection = Boss.GetComponent<ControllerCharaGeneral>().GetDirection();
        Vector3 WeaponPosition = new Vector3(0, 0, 0);
        float X;
        float Y;
        float PaddingX = 40f;//横方向の空間
        float PaddingY = 80f;//縦方向の空間

        if (WeaponType == "Gun")
        {
            Vector3 BossPosition = Boss.GetComponent<ControllerCharaGeneral>().GetPosition();
            X = BossPosition.x;
            Y = BossPosition.y;
        }
        else
        {
            X = 0;
            Y = 0;
        }

        if (WeaponType == "Body"| WeaponType == "Shield" | WeaponType == "Search") { WeaponPosition = new Vector3(X, Y, 0); }
        else
        {
            if (AttackDirection == "Left") { WeaponPosition = new Vector3(X - PaddingX, Y, 0); }
            else if (AttackDirection == "Right") { WeaponPosition = new Vector3(X + PaddingX, Y, 0); }
            else if (AttackDirection == "Up") { WeaponPosition = new Vector3(X, Y + PaddingY, 0); }
            else if (AttackDirection == "Down") { WeaponPosition = new Vector3(X, Y - PaddingY, 0); }
            else if (AttackDirection == "LeftDown") { WeaponPosition = new Vector3(X - PaddingX, Y - PaddingY, 0); }
            else if (AttackDirection == "LeftUp") { WeaponPosition = new Vector3(X - PaddingX, Y + PaddingY, 0); }
            else if (AttackDirection == "RightUp") { WeaponPosition = new Vector3(X + PaddingX, Y + PaddingY, 0); }
            else if (AttackDirection == "RightDown") { WeaponPosition = new Vector3(X + PaddingX, Y - PaddingY, 0); }
        }
        return WeaponPosition;
    }

    private void CheckStartAction()
    {
               if (WeaponType == "Gun")
        {
            MoveGun();
        }

    }
    private void CheckTriggerAction()
    {
        if (CheckTrigger())
        {
            if (ThisCharaGeneral.ObjectTriggerNow != null)
            {
                GameObject Target = ThisCharaGeneral.ObjectTriggerNow.gameObject;
                if (Target.GetComponent<ControllerCharaGeneral>())
                {
                    if (WeaponType == "Gun")
                    {
                        TrrigerGun(Target);
                    }
                    else if (WeaponType == "Rod")
                    {
                        TriggerRod(Target);
                    }
                    else if (WeaponType == "Search")
                    {
                        TriggerSearch(Target);
                    }
                    else if (WeaponType == "Body")
                    {
                        TriggerBody(Target);
                    }
                    else if (WeaponType == "Hold")
                    {
                        TriggerHold(Target);
                    }
                    else if (WeaponType == "Check")
                    {
                        TriggerCheck(Target);
                    }
                }
                else
                {
                }

            }
            else//接触したがターゲットの情報が取得できなかった時
            {
                DestroyGun();
            }
        }
        else//接触していない時
        {
              
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

    private void DestroyShield()
    {
        if (WeaponType == "Shield"&CheckSlotKeyDown()==false)
        {
            ThisCharaGeneral.MyDestroy();
        }

    }

    private bool CheckSlotKeyDown()
    {
        bool Result = false;
        if (Slot == 1& Input.GetKey(KeyCode.A))
        {
            Result = true;
        }
        else if (Slot == 2 & Input.GetKey(KeyCode.S))
        {
            Result = true;
        }
        else
        {
            Result = false;
        }
        return Result;
    }
        private void MoveGun()
    {
        if (WeaponType == "Gun")
        {
            float X=0;
            float Y=0;
            if (MoveAmount == 0)
            {
                Debug.Log("GunのMoveAmountが設定されていない。100を代入");
                MoveAmount = 2000;

            }

            Vector3 MoveVector=ThisCharaGeneral.GetVectorFromDirectionAndAmount(Direction,MoveAmount);
            X = MoveVector.x;
            Y = MoveVector.y;
            iTween.MoveBy(gameObject, iTween.Hash(
                        "x", X,
                        "y", Y,
                        "time", 2f,
                        "easeType", "linear",
                        "isLocal", true
                    ));

        }
    }

    IEnumerator DestroyRodCoroutine()
{
            yield return new WaitForSeconds(ExistTime * 1.0f);
        ThisCharaGeneral.MyDestroy();
}
    private void TimeDestroy()
    {
    if (ExistTime != 0)
    {
        Routine = null;
        Routine = DestroyRodCoroutine();
        StartCoroutine(Routine);
    }
    }
    private void DestroyGun()
    {
        if (WeaponType == "Gun")
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

   
    private void AddInfoToCharaGeneralAsWeapon()
    {
        ThisCharaGeneral.SetCharaType("Weapon");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(false);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(false);
        if (KnockBackAmount == 0) { KnockBackAmount = 20; }
    }


    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();

        AddInfoToCharaGeneralAsWeapon();
        SetTeam();
        TimeDestroy();
        CheckStartAction();

    }

    private void Update()
    {
        DestroyShield();
        CheckTriggerAction();
    }
}
