using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLifeCounter : MonoBehaviour
{

    GameObject ThisParent;
    
        
        // Start is called before the first frame update
    void Start()
    {
            ThisParent = gameObject.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
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
}
