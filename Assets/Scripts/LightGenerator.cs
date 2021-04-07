using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGenerator : MonoBehaviour
{
    public GameObject PlayerMirror; //PlayerMirrorそのものが入る変数
    public float LineWidth; //光の幅(太さ)

    void Start()
    {
        PlayerMirror = GameObject.Find("PlayerMirror");
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {   //ランタンの範囲にプレイヤーミラーがあった場合光を生成
            //PlayerMirror.GetComponent<PlayerMirrorController>().LightReflect(LineWidth);
            PlayerMirror.GetComponent<PlayerMirrorController>().Col = true;
            PlayerMirror.GetComponent<PlayerMirrorController>().ColorName = (int)LightManager.ColorType.NONE;
        }
    }

    private void OnTriggerExit(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {
            PlayerMirror.GetComponent<PlayerMirrorController>().Col = false;
            //PlayerMirror.GetComponent<PlayerMirrorController>().LightReflect(0);
        }
    }
}