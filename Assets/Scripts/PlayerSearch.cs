using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    public GameObject Mirror; //PlayerMirrorそのものが入る変数
    public GameObject Player; //PlayerMirrorそのものが入る変数

    public float RotateSpeed;
    public int Color;
    void Start()
    {
        Mirror = transform.root.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        RotateSpeed = 1.0f;
        Color = (int)LightManager.ColorType.NONE;
    }

    void Update()
    {
        //Mirror.GetComponent<MirrorManager>().ColorName = Color;
    }

    private void OnTriggerStay(Collider collison)
    {
        if (collison.gameObject.tag == "Player")
        {
            float angle = Input.GetAxisRaw("Vertical") * RotateSpeed;
            if (Input.GetKey("joystick button 1"))
            {
                Mirror.transform.Rotate(Vector3.forward, angle);
            }
            //Yが押されたら
            if (Input.GetKeyDown("joystick button 3"))
            {
                if (Mirror.GetComponent<MirrorManager>().GlassUse != true)
                {               
                    GameObject Glass = (GameObject)Resources.Load("Glass");
                    Mirror.GetComponent<MirrorManager>().ColorName = Player.GetComponent<PlayerController>().Color;
                    Color = Player.GetComponent<PlayerController>().ColorCnt;

                    if (Color == 1)
                    {
                        Glass = Instantiate(Glass, new Vector3(Mirror.transform.position.x, Mirror.transform.position.y, Mirror.transform.position.z - 0.51f), Quaternion.identity);
                        Glass.transform.localEulerAngles = Mirror.transform.localEulerAngles;
                        Glass.transform.parent = Mirror.transform;
                        Glass.GetComponent<GlassManager>().ColorType = Player.GetComponent<PlayerController>().ColorCnt;
                        Glass.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                        Mirror.GetComponent<MirrorManager>().GlassUse = true;
                    }
                    if (Color == 2)
                    {
                        Glass = Instantiate(Glass, new Vector3(Mirror.transform.position.x, Mirror.transform.position.y, Mirror.transform.position.z - 0.51f), Quaternion.identity);
                        Glass.transform.localEulerAngles = Mirror.transform.localEulerAngles;
                        Glass.transform.parent = Mirror.transform;
                        Glass.GetComponent<GlassManager>().ColorType = Player.GetComponent<PlayerController>().ColorCnt;
                        Glass.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                        Mirror.GetComponent<MirrorManager>().GlassUse = true;
                    }
                    if (Color == 3)
                    {
                        Glass = Instantiate(Glass, new Vector3(Mirror.transform.position.x, Mirror.transform.position.y, Mirror.transform.position.z - 0.51f), Quaternion.identity);
                        Glass.transform.localEulerAngles = Mirror.transform.localEulerAngles;
                        Glass.transform.parent = Mirror.transform;
                        Glass.GetComponent<GlassManager>().ColorType = Player.GetComponent<PlayerController>().ColorCnt;
                        Glass.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                        Mirror.GetComponent<MirrorManager>().GlassUse = true;
                    }
                }
            }

            //ガラス削除用
            if (Input.GetKeyDown("joystick button 2"))
            {
                if (Mirror.GetComponent<MirrorManager>().GlassUse == true)
                {
                    Mirror.GetComponent<MirrorManager>().GlassUse = false;
                    Mirror.GetComponent<MirrorManager>().ColorName = (int)LightManager.ColorType.NONE;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collison)
    {
        if (collison.gameObject.tag == "Player")
        {
            //if (Input.GetKeyDown("joystick button 3"))
            //{
            //    Mirror.GetComponent<MirrorManager>().ColorName = Player.GetComponent<PlayerController>().Color;
            //    Color = Player.GetComponent<PlayerController>().Color;
            //}
        }
    }

    private void OnTriggerExit(Collider collison)
    {

    }
}
