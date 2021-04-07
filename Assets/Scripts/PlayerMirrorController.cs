using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrorController : MonoBehaviour
{
    private Vector3 MirrorPos;
    Ray ray;
    RaycastHit hit; //ヒットしたオブジェクト情報
    public Transform PlayerMirror;
    public GameObject LightTop;

    public Transform Mirror;

    public GameObject Player; //Playerそのものが入る変数
    public GameObject Lantern; //Lanternそのものが入る変数
    public int distance;        //光を出す場合の鏡からの距離
    public bool Reflection;     //反射フラグ
    Vector3 Dir;                //プレイヤーの鏡の向き
    public bool Col;    //当たっているかどうか

    LineRenderer lineRenderer;
    Color cRed = new Color(1, 0, 0, 0);
    PlayerController script; //PlayerControllerが入る変数

    public GameObject Mirrors; //Mirrorそのものが入る変数

    //回転させるスピード
    public float rotateSpeed;

    //float lazerDistance = 10f;
    //float lazerStartPointDistance = 0.15f;
    public float lineWidth;
    public Vector3 reflectVec;  //反射角を計算している
    public Vector3 HitPos;  //反射角を計算している
    public Transform LightHitPos;
    public Vector3 LightTopPos;
    public int ColorName;

    void Start()
    {
        Mirrors = GameObject.Find("Mirror");
        MirrorPos = GameObject.Find("Player").transform.position;
        MirrorPos.x = GameObject.Find("Player").transform.position.x + 1.0f;

        LightTop = GameObject.Find("LightTop");

        transform.position = MirrorPos;

        Player = GameObject.Find("Player"); //Playerをオブジェクトの名前から取得して変数に格納する
        script = Player.GetComponent<PlayerController>(); //Playerの中にあるPlayerControllerを取得して変数に格納する

        //Dir = transform.forward;
        //Rayの飛ばせる距離
        distance = 20;

        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;

        LightHitPos = transform.GetChild(0);

        Reflection = false;

        LightTopPos = LightHitPos.transform.position;
        ColorName = (int)LightManager.ColorType.NONE;
    }

    void Update()
    {
        //Col = false;

        // transformを取得
        Transform myTransform = this.transform;

        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;

        myTransform.eulerAngles = worldAngle; // 回転角度を設定

        //回転させる角度
        float angle = Input.GetAxisRaw("Vertical") * rotateSpeed;

        //プレイヤー位置情報
        Vector3 playerPos = Player.transform.position;

        //LightTop.transform.position = HitPos;

        //Bボタンを押している場合は操作できる
        if (Input.GetKey("joystick button 1"))
        {
            if (script.DirRight == true)
            {
                //鏡を回転させる
                transform.RotateAround(playerPos, Vector3.forward, angle);
            }

            if (script.DirRight != true)
            {
                //鏡を回転させる
                transform.RotateAround(playerPos, Vector3.forward, -angle);
            }
        }


        //キーボード操作用
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (script.DirRight == true)
            {
                //鏡を回転させる
                transform.RotateAround(playerPos, Vector3.forward, angle);
            }

            if (script.DirRight != true)
            {
                //鏡を回転させる
                transform.RotateAround(playerPos, Vector3.forward, -angle);
            }
        }

        //LightReflect();
            if (Col == true)
        {
            ray = new Ray(transform.position, transform.forward * distance);
            GetComponent<LightManager>().LightCheck(lineWidth, transform.position, ray, lineRenderer, transform.forward, "Player", HitPos, LightHitPos, Col, ColorName);
        }


        if (Col == false)
        {
            ray = new Ray(transform.position, transform.right * 0);
            GetComponent<LightManager>().LightCheck(0, transform.position, ray, lineRenderer, transform.forward, "Player", HitPos, LightHitPos, Col, ColorName);
        }

        Debug.DrawLine(transform.position, ray.origin + transform.forward * distance, Color.blue);
        //LightHitPos.position = LightTopPos;
    }
    void LateUpdate()
    {
        //    MirrorPos = GameObject.Find("Player").transform.position;
        //    if(script.Right == true)
        //    {
        //        MirrorPos.x = GameObject.Find("Player").transform.position.x + 0.5f;
        //    }
        //    if (script.Right == false)
        //    {
        //        MirrorPos.x = GameObject.Find("Player").transform.position.x - 0.5f;
        //    }

        //    transform.position = MirrorPos;
    }

    public void LightReflect(float width)
    {
        //if(Col == true)
        //{
        //    GetComponent<LightManager>().LightCheck(lineWidth, transform);
        //}
        //lineRenderer.startWidth = width;    //光の幅(太さ)
        //Vector3 direction = transform.forward * 10.0f;

        ////Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        //ray = new Ray(transform.position, transform.forward * distance);

        //lineRenderer.SetPosition(0, transform.position);    //光の初期点

        //if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))    //rayが何かに当たっていた場合
        //{
        //    lineRenderer.SetPosition(1, hit.point); //光の終着点

        //    Vector3 incomingVec = hit.point - transform.position;
        //    Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);  //反射角を計算している

        //    Debug.DrawRay(hit.point, reflectVec, Color.green);  //反射したrayを描画

        //    if (hit.collider.tag == "Mirror")   //rayが当たったオブジェクトのタグがMirrorだった場合
        //    {
        //        hit.collider.GetComponent<MirrorLightGenerator>().ReflectionLight(reflectVec, hit.point, lineWidth); //鏡から光を生成
        //        hit.collider.GetComponent<MirrorManager>().Col = true; //鏡に当たっているフラグをtrueに
        //        hit.collider.GetComponent<MirrorManager>().reflectVec = reflectVec;
        //        hit.collider.GetComponent<MirrorManager>().HitPos = hit.point;
        //    }
        //    if (hit.collider.tag != "Mirror")
        //    {
        //        Mirror.GetComponent<MirrorLightGenerator>().ReflectionLight(reflectVec, hit.point, 0); //鏡から光を生成
        //        Mirror.GetComponent<MirrorManager>().Col = false; //鏡に当たっているフラグをfalseに
        //    }
        //}
        //else
        //{
        //    lineRenderer.SetPosition(1, transform.position + direction);    //当たっていなかった場合光は進み続ける
        //}

        ////Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        //Debug.DrawLine(transform.position, ray.origin + transform.forward * distance, Color.blue);
    }
}
