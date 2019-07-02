using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKey : MonoBehaviour
{
    public GameObject StateGame;
    public GameObject Player;


    public void MovePlayerLeft()
    {
        Vector3 PositionPlayer1 = Player.GetComponent<RectTransform>().localPosition;
        PositionPlayer1.x-=100f;
        Player.GetComponent<RectTransform>().localPosition = PositionPlayer1;


    }
    void Start()
    {
        MovePlayerLeft();
    }

    void Update()
    {
        
    }
}
