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

    private float CollisionKnockBackAmount = 50f;
    public float DamagedKnockBackAmount = 1000f;

    public Collider2D ObjectTriggerNow;


    public void OnStun()
    {
    SwitchDamagedKnockBack = 0;
    SwitchCollisionKnockBack = 0;
}
    public void OffStun()
    {
        SwitchDamagedKnockBack = 1;
        SwitchCollisionKnockBack = 1;
    }


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
        else if (StateDirection == "LeftDown") { Result = "RightUp"; }
        else if (StateDirection == "LeftUp") { Result = "RightDown"; }
        else if (StateDirection == "RightDown") { Result = "LeftUp"; }
        else if (StateDirection == "RightUp") { Result = "LeftDown"; }
        else { Debug.Log("Directionが変になっている"); }

        return Result;
    }

    public string GetAntiDirection(string Direction)
    {
        string Result = "";
        if (Direction == "Left") { Result = "Right"; }
        else if (Direction == "Right") { Result = "Left"; }
        else if (Direction == "Up") { Result = "Down"; }
        else if (Direction == "Down") { Result = "Up"; }
        else if (Direction == "LeftDown") { Result = "RightUp"; }
        else if (Direction == "LeftUp") { Result = "RightDown"; }
        else if (Direction == "RightDown") { Result = "LeftUp"; }
        else if (Direction == "RightUp") { Result = "LeftDown"; }
        else { Debug.Log("Directionが変になっている"); }

        return Result;
    }

    public void DamagedKnockBack(float Amount,string FromDirection)
    {
        if (SwitchDamagedKnockBack != 0)
        {
           
            AddForceTween(FromDirection, Amount);
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

    public Vector3 GetVectorFromDirectionAndAmount(string Direction,float MoveAmount) {
        Vector3 Result;
        float X=0;
        float Y=0;
        float MoveDiagonal = 0.7f;

        if (Direction == "Left")
        {
            X = MoveAmount * -1f;
            Y = 0;
        }
        else if (Direction == "Right")
        {
            X = MoveAmount*1f;
            Y = 0;
        }
        else if (Direction == "Up")
        {
            Y = MoveAmount*1f;
            X = 0;
        }
        else if (Direction == "Down")
        {
            Y = MoveAmount * -1f;
            X = 0;
        }
        else if (Direction == "LeftUp")
        {
            X = MoveAmount * MoveDiagonal * -1f;
            Y = MoveAmount * MoveDiagonal*1f;
        }
        else if (Direction == "RightUp")
        {
            X = MoveAmount * MoveDiagonal*1f;
            Y = MoveAmount * MoveDiagonal*1f;
        }
        else if (Direction == "LeftDown")
        {
            X = MoveAmount * MoveDiagonal * -1f;
            Y = MoveAmount * MoveDiagonal * -1f;
        }
        else if (Direction == "RightDown")
        {
            X = MoveAmount * MoveDiagonal*1f;
            Y = MoveAmount * MoveDiagonal * -1f;
        }
        Result = new Vector3(X, Y, 0);
        return Result;
    }

    public void AddForceTween(string Direction, float Amount)
    {
        float X=0;
        float Y=0;
        Vector3 MoveVector=GetVectorFromDirectionAndAmount(Direction,Amount);
        X = MoveVector.x;
        Y = MoveVector.y;
       
        iTween.MoveBy(gameObject, iTween.Hash(
                     "x", X,
                     "y", Y,
                     "time", 0.2f,
                     "easeType", "easeOutCubic",
                     "isLocal", true
                 ));
    }
        public void AddForce(string Direction, float Amount)
    {
        CheckRigidBody();
        Rigidbody2D Rb = this.GetComponent<Rigidbody2D>();
        Rb.AddForce(DirectionAmountToVector(Direction,Amount), ForceMode2D.Impulse);
    }
    public void AddForce(Vector3 Direction)
    {
        CheckRigidBody();
        Rigidbody2D Rb = this.GetComponent<Rigidbody2D>();
        Rb.AddForce(Direction, ForceMode2D.Impulse);
    }

    public Vector3 DirectionAmountToVector(string Direction,float Amount)
    {
        Vector3 ResultVector=new Vector3(0,0,0);
        if (Direction == "Left") { ResultVector = new Vector3(Amount*-1f, 0, 0); }
        else if (Direction == "Right") { ResultVector = new Vector3(Amount, 0, 0); }
        else if (Direction == "Up") { ResultVector = new Vector3(0,Amount, 0); }
        else if (Direction == "Down") { ResultVector = new Vector3(0, Amount*-1f, 0); }
        else if (Direction == "LeftUp") { ResultVector = new Vector3(Amount*-1f, Amount, 0); }
        else if (Direction == "LeftDown") { ResultVector = new Vector3(Amount*-1f, Amount*-1f, 0); }
        else if (Direction == "RightUp") { ResultVector = new Vector3(Amount, Amount, 0); }
        else if (Direction == "RightDown") { ResultVector = new Vector3(Amount, Amount * -1f, 0); }
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
    public void AddPosition(float MoveAmountX, float MoveAmountY)
    {
        Vector3 AddingPosition = GetPosition();

            AddingPosition.x += MoveAmountX;
            AddingPosition.y += MoveAmountY;

        SetPosition(AddingPosition);
    }

    public void AddDirection(float MoveAmountX, float MoveAmountY)
    {
        string State = GetDirection();
        if (MoveAmountX != 0& MoveAmountY == 0)
        {
            if (MoveAmountX > 0) { State = "Right"; }
            else if(MoveAmountX<0){ State = "Left"; }
        }
        else if (MoveAmountX == 0 & MoveAmountY != 0)
        {
            if (MoveAmountY > 0) { State = "Up"; }
            else if (MoveAmountY < 0) { State = "Down"; }

        }
        else if (MoveAmountX != 0 & MoveAmountY != 0)
        {
            if (MoveAmountY > 0&MoveAmountX>0) { State = "RightUp"; }
            else if (MoveAmountY > 0 & MoveAmountX < 0) { State = "LeftUp"; }
            else if (MoveAmountY < 0 & MoveAmountX > 0) { State = "RightDown"; }
            else if (MoveAmountY < 0 & MoveAmountX < 0) { State = "LeftDown"; }

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
