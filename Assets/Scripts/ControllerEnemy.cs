using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemy : MonoBehaviour
{
    public int HitPoint;
    public string EnemyType;
    private ControllerCharaGeneral ThisCharaGeneral;
    //Enemyにアタッチする
    //Enemy特有の行動を制御する
    //全てのEnemyの行動を持つ
    //スポーン時にEnemyTypeを与えられ、あるいはPrefabの名前から規定する
    //EnemyTypeに従って外見や行動を決める


    private void AddInfoToCharaGeneralAsEnemy()
    {
        //将来的にはスポーンコントローラで指定する
        HitPoint = 10;
        ThisCharaGeneral.SetCharaType("Enemy");
        ThisCharaGeneral.SetSwitchCollisionKnockBack(1);
        ThisCharaGeneral.SetSwitchDamagedKnockBack(1);
    }

    private void Start()
    {
        ThisCharaGeneral = gameObject.GetComponent<ControllerCharaGeneral>();

        AddInfoToCharaGeneralAsEnemy();
    }
}
