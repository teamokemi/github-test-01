using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{

    public int ColorType;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collison)
    {
        if (collison.gameObject.tag == "Player")
        {
            //ガラス削除用
            if (Input.GetKeyDown("joystick button 2"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
