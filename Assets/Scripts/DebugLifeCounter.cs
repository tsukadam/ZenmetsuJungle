using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLifeCounter : MonoBehaviour
{

    ControllerEnemy ThisParent;
    
        
        // Start is called before the first frame update
    void Start()
    {
ThisParent=gameObject.transform.parent.gameObject.GetComponent<ControllerEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = ThisParent.HitPoint.ToString();
    }
}
