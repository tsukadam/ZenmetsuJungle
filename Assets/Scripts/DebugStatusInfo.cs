using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugStatusInfo : MonoBehaviour
{

    GameObject ThisParent;
    string ThisName;

    // Start is called before the first frame update
    void Start()
    {
        ThisParent = gameObject.transform.parent.gameObject;
        ThisName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisName == "MentalPoint")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerPlayer>().MentalPoint.ToString();

            }
        }
        else if (ThisName == "GachaPoint")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerPlayer>().GachaPoint.ToString();

            }
        }
        else if (ThisName == "HitPoint")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerPlayer>().HitPoint.ToString();

            }
            else if (ThisParent.GetComponent<ControllerEnemy>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerEnemy>().HitPoint.ToString();

            }
        }
        else if (ThisName == "Weapon0")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[0].ToString();

            }
            else if (ThisParent.GetComponent<ControllerEnemy>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[0].ToString();

            }
        }
        else if (ThisName == "Weapon1")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[1].ToString();

            }
            else if (ThisParent.GetComponent<ControllerEnemy>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[1].ToString();

            }
        }
        else if (ThisName == "Weapon2")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[2].ToString();

            }
            else if (ThisParent.GetComponent<ControllerEnemy>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerAttack>().WeaponTypeDetail[2].ToString();

            }
        }
        else if (ThisName == "WithState")
        {
            if (ThisParent.GetComponent<ControllerPlayer>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerPlayer>().GetWithEnemyState().ToString();

            }
            else if (ThisParent.GetComponent<ControllerEnemy>())
            {
                this.GetComponent<Text>().text = ThisParent.GetComponent<ControllerEnemy>().GetWithPlayerState().ToString();

            }
        }
    }
}
