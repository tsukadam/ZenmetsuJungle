using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemyEvent : MonoBehaviour
{
    public GameObject Xray;
    public GameObject Parent;
    public GameObject Player;


    public void DisAppearXray()
    {
        Xray.GetComponent<ControllerXray>().DisAppearXray();
    }

    public void MovePlayerToEnemyAsHebiHold()
    {
        Vector3 ParentPosition = Parent.GetComponent<ControllerCharaGeneral>().GetPosition();
        float X = ParentPosition.x;
        float Y = ParentPosition.y;
        float Z = ParentPosition.z;
        Vector3 XrayPosition = new Vector3(X - 50, Y, Z);

        Player.GetComponent<RectTransform>().localPosition = XrayPosition;

    }
    public void MoveXrayToEnemy()
    {
        Vector3 ParentPosition = Parent.GetComponent<ControllerCharaGeneral>().GetPosition();
        float X = ParentPosition.x;
        float Y = ParentPosition.y;
        float Z = ParentPosition.z;
        Vector3 XrayPosition = new Vector3(X - 100, Y, Z);

        Xray.GetComponent<RectTransform>().localPosition = XrayPosition;
    }

    public void AppearXray()
    {
        MoveXrayToEnemy();
        Xray.GetComponent<ControllerXray>().AppearXray();

    }

    // Start is called before the first frame update
    void Start()
    {
        Xray = GameObject.Find("Xray");
        Parent = this.transform.parent.gameObject;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
