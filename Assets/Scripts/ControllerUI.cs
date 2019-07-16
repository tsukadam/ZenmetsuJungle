using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    public GameObject WindowMassage;
    public Text MassageText;
    bool SwitchMassage;


    public void StartMassage(string Text)
    {
        PauseStop();
        MassageText.text = Text;
        WindowMassage.SetActive(true);
        SwitchMassage = true;

    }
    public void EndMassage()
    {
        MassageText.text = "";
        WindowMassage.SetActive(false);
        SwitchMassage = false;
        PauseGo();
        Input.ResetInputAxes();
    }


    private void CheckEndMassage()
    {
        if (Input.GetKeyDown(KeyCode.Z)&SwitchMassage==true)
        {
            EndMassage();
        }

    }
    private void UIInit() {
        WindowMassage.SetActive(false);
        MassageText.text = "";
        SwitchMassage = false;
    }


    private void PauseStop()
    {
            Time.timeScale = 0;
    }
    private void PauseGo()
    {
        Time.timeScale = 1.0f;
    }

    private void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    private void Update()
    {
        CheckEndMassage();
    }
    private void Start()
    {
        UIInit();
    }
}
