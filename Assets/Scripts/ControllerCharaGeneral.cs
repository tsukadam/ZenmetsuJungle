using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharaGeneral : MonoBehaviour
{
    //全てのPlayer、Enemy、Weaponにアタッチする
    //移動や接触判定など、背景ではないものの基本処理を持つ

    public string CharaType;//Player、Enemy、Weapon

    public int SwitchDamagedKnockBack = 0;
    public int SwitchCollisionKnockBack = 0;

    public string StateCollision = "Exit";
    public string StateTrigger = "Exit";
    public string StateDirection = "Down";

    public float CollisionKnockBackAmount = 1f;
    public float DamagedKnockBackAmount = 100f;

    public Collider2D ObjectTriggerNow;


    public void MyDestroy()
    {
        DestroyImmediate(gameObject);
    }

    public void SetCharaType(string Type)
    {
        CharaType = Type;
    }
    public string GetCharaType()
    {
        return CharaType;
    }

    public void SetSwitchCollisionKnockBack(int State)
    {
        SwitchCollisionKnockBack = State;
    }
    public int GetSwitchCollisionKnockBack()
    {
        int Return;
        Return= SwitchCollisionKnockBack;
        return Return;
    }
    public void SetSwitchDamagedKnockBack(int State)
    {
        SwitchDamagedKnockBack = State;
    }
    public int GetSwitchDamagedKnockBack()
    {
        int Return;
        Return = SwitchDamagedKnockBack;
        return Return;
    }

    public string GetAntiDirection(){
        string Result = "";
        if (StateDirection == "Left") { Result = "Right"; }
        else if (StateDirection == "Right") { Result = "Left"; }
        else if (StateDirection == "Up") { Result = "Down"; }
        else if (StateDirection == "Down") { Result = "Up"; }
        else { Debug.Log("Directionが変になっている"); }

        return Result;
    }
    public void DamagedKnockBack(float Amount)
    {
        if (SwitchDamagedKnockBack != 0)
        {
            string AntiDirection = GetAntiDirection();
            AddForce(AntiDirection, Amount);
        }
    }
    public void CollisionKnockBack()
    {
        if (SwitchCollisionKnockBack != 0)
        {
            string AntiDirection = GetAntiDirection();
            AddForce(AntiDirection, CollisionKnockBackAmount);
        }
    }

    public void AddForce(string Direction, float Amount)
    {
        CheckRigidBody();
        Rigidbody2D Rb = this.GetComponent<Rigidbody2D>();
        Rb.AddForce(DirectionAmountToVector(Direction,Amount), ForceMode2D.Impulse);
    }
    public Vector3 DirectionAmountToVector(string Direction,float Amount)
    {
        Vector3 ResultVector=new Vector3(0,0,0);
        if (Direction == "Left") { ResultVector = new Vector3(Amount*-1f, 0, 0); }
        else if (Direction == "Right") { ResultVector = new Vector3(Amount, 0, 0); }
        else if (Direction == "Up") { ResultVector = new Vector3(0,Amount, 0); }
        else if (Direction == "Down") { ResultVector = new Vector3(0, Amount*-1f, 0); }
        else { Debug.Log("Directionの引数間違い"); }

        return ResultVector;
    }

    //オブジェクトが衝突したとき
    private void OnCollisionEnter2D(Collision2D other)
    {

      
       // Debug.Log(gameObject.name+"いわく「"+other.gameObject.name+"がぶつかってきた");
        SetCollision("Enter");
        CollisionKnockBack();
    }
    //オブジェクトが離れた時
    private void OnCollisionExit2D(Collision2D other)
    {

        SetCollision("Exit");
    }

    //オブジェクトが触れている間
    private void OnCollisionStay2D(Collision2D other)
    {

        SetCollision("Stay");
        CollisionKnockBack();
    }

    //オブジェクトがトリガーに衝突したとき
    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectTriggerNow = other;
        SetTrigger("Enter");
    }

    //オブジェクトがトリガーから離れた時
    private void OnTriggerExit2D(Collider2D other)
    {
        ObjectTriggerNow = null;
        SetTrigger("Exit");
    }

    //オブジェクトがトリガーに触れている間
    private void OnTriggerStay2D(Collider2D other)
    {
        ObjectTriggerNow = other;
        SetTrigger("Stay");
    }



    public void SetTrigger(string State)
    {
        StateTrigger = State;
    }
    public string GetTrigger()
    {
        return StateTrigger;
    }

    public void SetCollision(string State)
    {
        StateCollision = State;        
    }
    public string GetCollision()
    {
        return StateCollision;
    }
    public void CheckRectTransform()
    {
        if (gameObject.GetComponent<RectTransform>() == null)
        {
            Debug.Log("RectTransformがアタッチされていない"); }
    }
    public void CheckRigidBody()
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            Debug.Log("Rigidbody2Dがアタッチされていない");
        }
    }
    public Vector3 GetPosition()
    {
        CheckRectTransform();
            Vector3 Position=new Vector3(0,0,0);
        Position = gameObject.GetComponent<RectTransform>().localPosition;
        return Position;
    }
    public void SetPosition(Vector3 NewPosition)
    {
        CheckRectTransform();
        gameObject.GetComponent<RectTransform>().localPosition=NewPosition;
        
    }
    public void AddPosition(string Dimention,float MoveAmount)
    {
        Vector3 AddingPosition = GetPosition();

        if (Dimention == "X")
        {
            AddingPosition.x += MoveAmount;
        }
        else if (Dimention == "Y")
        {
            AddingPosition.y += MoveAmount;
        }
        else { Debug.Log("Dimentionの引数間違い"); }

        SetPosition(AddingPosition);
    }

    public void AddDirection(string Dimention, float MoveAmount)
    {
        string State = GetDirection();
        if (Dimention == "X")
        {
            if (MoveAmount > 0) { State = "Right"; }
            else if(MoveAmount<0){ State = "Left"; }
        }
        else if (Dimention == "Y")
        {
            if (MoveAmount > 0) { State = "Up"; }
            else if (MoveAmount < 0) { State = "Down"; }

        }
        else { Debug.Log("Dimentionの引数間違い"); }
        SetDirection(State);

    }
    public void SetDirection(string State)
    {
        StateDirection = State;
    }
    public string GetDirection()
    {
        return StateDirection;
    }
    
}
