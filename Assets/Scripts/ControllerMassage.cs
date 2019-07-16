using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMassage : MonoBehaviour
{
    //チェックでMassageを出すものにアタッチする
    //Massage内容をインスペクタで個別に入力しておく
    //チェックされるとメッセージUIを呼び出す

        public string MassageText;
    private ControllerCharaGeneral ThisCharaGeneral;
    private IEnumerator Routine;
    private GameObject ControllerGame;

    public void WriteMassage()
    {
        ControllerGame = GameObject.Find("ControllerGame");
        ControllerGame.GetComponent<ControllerUI>().StartMassage(MassageText);

    }
    private void AddInfoToCharaGeneralAsMassage()
    {
        ThisCharaGeneral.SetCharaType("Massage");
    }


    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();
        AddInfoToCharaGeneralAsMassage();
    }
}
