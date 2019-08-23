using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class ControllerXray : MonoBehaviour
{
    public Animator ThisAnim;

    public void DisAppearXray()
    {
        ThisAnim.SetBool("Vore", false);
    }


    public void AppearXray()
    {


        ThisAnim.SetBool("Vore", true);

    }
    // Start is called before the first frame update
    void Start()
    {
        ThisAnim = GetComponent<Animator>();
        DisAppearXray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
