using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWeapon : MonoBehaviour
{
    public string Type;//Gun or Rod
    public string Direction;
    public float MoveAmount;
    public int ExistTime;//存在時間、0=無限
    public int Damage;
    private IEnumerator Routine;

    private void MyDestroy()
    {
        DestroyImmediate(gameObject);
    }
    private void GunMove()
    {
        if (Type == "Gun")
        {
            if (MoveAmount == 0)
            {
                Debug.Log("GunのMoveAmountが設定されていない。10を代入");
                MoveAmount = 10;

            }
            if (Direction == "Left")
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("X", MoveAmount * -1.0f);
            }
            else if (Direction == "Right")
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("X", MoveAmount);
            }
            else if (Direction == "Up")
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("Y", MoveAmount);
            }
            else if (Direction == "Down")
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("Y", MoveAmount*-1.0f);
            }

        }
    }

    IEnumerator RodDestroyCoroutine()
{
            yield return new WaitForSeconds(ExistTime * 1.0f);
            MyDestroy();
}
    private void RodDestroy()
    {
    if (Type == "Rod")
    {
        Routine = null;
        Routine = RodDestroyCoroutine();
        StartCoroutine(Routine);
    }
    }
    private void GunDestroy()
    {
        if (Type == "Gun" & gameObject.GetComponent<ControllerMoveGeneral>().StateCollision != "Exit")
        {
            MyDestroy();
        }

    }

    void Start()
    {
        RodDestroy();
    }

    void Update()
    {
        GunDestroy();
        GunMove();
    }
}
