using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTopManager : MonoBehaviour
{
    public GameObject PlayerMirror; //PlayerMirrorそのものが入る変数
    public GameObject Mirror; //Mirrorそのものが入る変数
    //public PlayerMirrorController script;
    void Start()
    {

    }


    void Update()
    {
       // transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    private void OnTriggerStay(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        { 
            PlayerMirror.GetComponent<PlayerMirrorController>().Col = true;
        }

        if (collison.gameObject.tag == "Mirror")
        {
            collison.GetComponent<MirrorManager>().Col = true;
        }
    }

    private void OnTriggerExit(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {
            PlayerMirror.GetComponent<PlayerMirrorController>().Col = false;
        }
        if (collison.gameObject.tag == "Mirror")
        {
            collison.GetComponent<MirrorManager>().Col = false;
        }
    }
}
