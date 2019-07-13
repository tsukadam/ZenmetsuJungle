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
    public int KnockBackAmount;
    private IEnumerator Routine;
    public GameObject Boss;//Weaponを発生させた者
    public string Team;

    private ControllerCharaGeneral ThisCharaGeneral;



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


    private void StartRush()
    {
        //Enemyクラスが持つTargetObjectの座標に突進する。
        GameObject SearchedTarget = Boss.GetComponent<ControllerEnemy>().GetTarget();
        Vector3 TargetPosition = SearchedTarget.GetComponent<ControllerCharaGeneral>().GetPosition();
        float X = TargetPosition.x;
        float Y = TargetPosition.y;

        iTween.MoveTo(Boss, iTween.Hash(
                    "x", X,
                    "y", Y,
                    "time", 1f,
                    "easeType", "easeInOutBack",
                    "isLocal", true
                ));
        Boss.GetComponent<ControllerEnemy>().InitTarget();

    }
    private void TriggerRush(GameObject Target)
    {
        //自分がEnemy、相手がPlayerならば、ダメージを与える。
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Enemy"& TargetType == "Player")
        {
            Target.GetComponent<ControllerPlayer>().TryDamageHitPoint(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    private void TriggerHold(GameObject Target)
    {
        //自分がEnemy、相手がPlayerならば、
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Enemy" & TargetType == "Player")
        {
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

    private void TriggerRod(GameObject Target)
    {
        //自分がPlayer、相手がEnemyならば、ダメージを与える。
         string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
         if (Team == "Player"& TargetType == "Enemy")
         {
            Target.GetComponent<ControllerEnemy>().TryDamageHitPoint(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
         }
    }
    private void TrrigerGun(GameObject Target)
    //自分がPlayer、相手がEnemyならば、ダメージを与える。
    {
        string TargetType = Target.GetComponent<ControllerCharaGeneral>().CharaType;
        if (Team == "Player"& TargetType == "Enemy")
        {
            Target.GetComponent<ControllerEnemy>().TryDamageHitPoint(gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DestroyGun();
        }
        //ウェポン相手は無意味
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

        if (WeaponType == "Rush"| WeaponType == "Hold") { WeaponPosition = new Vector3(X, Y, 0); }
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
                if (WeaponType == "Rush")
                {
                    StartRush();
                }
        else if (WeaponType == "Gun")
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
                    else if (WeaponType == "Rush")
                    {
                        TriggerRush(Target);
                    }
                    else if (WeaponType == "Hold")
                    {
                        TriggerHold(Target);
                    }
                }
                else
                {
                    DestroyGun();
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

    IEnumerator RodDestroyCoroutine()
{
            yield return new WaitForSeconds(ExistTime * 1.0f);
        ThisCharaGeneral.MyDestroy();
}
    private void DestroyTime()
    {
    if (ExistTime != 0)
    {
        Routine = null;
        Routine = RodDestroyCoroutine();
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

    private void CheckType() {
        if (WeaponType != "Rod" & WeaponType != "Gun" & WeaponType != "Grab" & WeaponType != "Rush" & WeaponType != "Search" & WeaponType != "Hold")
        {
            Debug.Log(WeaponType+"は定義されていないWeaponType");
            //ThisCharaGeneral.MyDestroy();
        }        
    }
    private void AddInfoToCharaGeneralAsWeapon()
    {
        ThisCharaGeneral.SetCharaType("Weapon");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(0);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(0);
        if (KnockBackAmount == 0) { KnockBackAmount = 20; }
    }


    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();

        AddInfoToCharaGeneralAsWeapon();
        SetTeam();
        CheckType();
        DestroyTime();
        CheckStartAction();

    }

    private void Update()
    {
        CheckTriggerAction();
    }
}
