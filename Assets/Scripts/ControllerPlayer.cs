using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public int SwitchBlockKeyZ = 0;
    private IEnumerator Routine;

    public float MoveAmountOneKey=10f;

    private void SetPlayerWeapon(string GunType,string RodType)
    {
        gameObject.GetComponent<ControllerAttack>().SetWeapon(GunType,RodType);
    }
    private void MakePlayerGun()
    {
        string Direction = gameObject.GetComponent<ControllerMoveGeneral>().GetDirection();
        gameObject.GetComponent<ControllerAttack>().MakeGun();
    }
    private void MakePlayerRod()
    {
        string Direction = gameObject.GetComponent<ControllerMoveGeneral>().GetDirection();
        gameObject.GetComponent<ControllerAttack>().MakeRod();
    }

    private void CheckKey()
    {
        if (gameObject.GetComponent<ControllerMoveGeneral>() != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("X", MoveAmountOneKey * -1);
                gameObject.GetComponent<ControllerMoveGeneral>().AddDirection("X", MoveAmountOneKey * -1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("X", MoveAmountOneKey);
                gameObject.GetComponent<ControllerMoveGeneral>().AddDirection("X", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("Y", MoveAmountOneKey);
                gameObject.GetComponent<ControllerMoveGeneral>().AddDirection("Y", MoveAmountOneKey);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                gameObject.GetComponent<ControllerMoveGeneral>().AddPosition("Y", MoveAmountOneKey * -1);
                gameObject.GetComponent<ControllerMoveGeneral>().AddDirection("Y", MoveAmountOneKey * -1);
            }
            if (Input.GetKey(KeyCode.Z)&SwitchBlockKeyZ==0)
            {
                SetPlayerWeapon("GunBullet", "None");
                MakePlayerGun();
                MakePlayerRod();
                Routine = null;
                Routine = BlockKeyZCoroutine();
                StartCoroutine(Routine);
            }
        }

    }
    IEnumerator BlockKeyZCoroutine()
    {
        SwitchBlockKeyZ = 1;
        yield return new WaitForSeconds(0.5f);
        SwitchBlockKeyZ = 0;
    }

    private void Start()
    {
    }
    private void Update()
    {
        CheckKey();
    }
}
