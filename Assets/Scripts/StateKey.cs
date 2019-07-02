using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKey : MonoBehaviour
{
    [SerializeField]
    private int stateLeft=0;
    [SerializeField]
    private int stateRight = 0;
    [SerializeField]
    private int stateTop = 0;
    [SerializeField]
    private int stateBottom = 0;


    public void SetStateLeft(int Input)
    {
        this.stateLeft = Input;

    }
    public int GetStateLeft()
    {
        return stateLeft;
    }
    public void SetStateRight(int Input)
    {
        this.stateRight = Input;

    }
    public int GetStateRight()
    {
        return stateRight;
    }
    public void SetStateTop(int Input)
    {
        this.stateTop = Input;

    }
    public int GetStateTop()
    {
        return stateTop;
    }
    public void SetStateBottom(int Input)
    {
        this.stateBottom = Input;

    }
    public int GetStateBottom()
    {
        return stateBottom;
    }

}