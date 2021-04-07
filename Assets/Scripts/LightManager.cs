using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit; //ヒットしたオブジェクト情報
    //LineRenderer lineRenderer;
    public Vector3 HitPos; //当たった場所
    public float lineWidth; //光の幅(太さ)
    public Vector3 incomingVec;
    public Vector3 reflectVec;  //反射角を計算している
    public GameObject PlayerMirror; //PlayerMirrorそのものが入る変数

    public enum ColorType
    {
        NONE = 0,
        RED,
        GREEN,
        BLUE,
        YELLOW,
        WATER,
        PURPLE,
    }

    void Start()
    {
        gameObject.GetComponent<LineRenderer>();
        //lineRenderer.startWidth = lineWidth;
    }

    void Update()
    {

        
    }

    public void LightCheck(float width, Vector3 Pos, Ray ray, LineRenderer lineRenderer, Vector3 Vec, string name, Vector3 HitPos, Transform LPos, bool bUse, int ColorName)
    {
        int distance = 20;  //光の長さ
        lineRenderer.startWidth = width;    //光の幅(太さ)
        Vector3 direction = transform.forward * 10.0f;

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        ray = new Ray(Pos, Vec * distance);

        lineRenderer.SetPosition(0, Pos);    //光の初期点

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))    //rayが何かに当たっていた場合
        {
            lineRenderer.SetPosition(1, hit.point); //光の終着点
            LPos.position = hit.point;  //光の先端のオブジェクトのポジションをrayが当たったポジションに変える

            if (bUse == false)
            {
                lineRenderer.SetPosition(1, Pos); //光の終着点
                LPos.position = new Vector3(1000.0f, 1000.0f, 1000.0f);
            }
            //Debug.Log("t");

            Vector3 incomingVec = hit.point - Pos;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);  //反射角を計算している

            Debug.DrawRay(hit.point, reflectVec, Color.green);  //反射したrayを描画

            //rayが当たったオブジェクトのタグがPlayerMirrorだった場合
            if (hit.collider.tag == "PlayerMirror")
            {
                hit.collider.GetComponent<PlayerMirrorController>().reflectVec = reflectVec;    //反射角を与えている
                hit.collider.GetComponent<PlayerMirrorController>().HitPos = hit.point;         //光の先端の場所を与えている
                hit.collider.GetComponent<PlayerMirrorController>().ColorName = ColorName;      //色を指定
            }

            //rayが当たったオブジェクトのタグがMirrorだった場合
            if (hit.collider.tag == "Mirror")
            {
                hit.collider.GetComponent<MirrorManager>().reflectVec = reflectVec;    //反射角を与えている
                hit.collider.GetComponent<MirrorManager>().HitPos = hit.point;         //光の先端の場所を与えている
                if (hit.collider.GetComponent<MirrorManager>().GlassUse == true)
                {
                    hit.collider.GetComponent<MirrorManager>().HitColor = hit.collider.GetComponent<MirrorManager>().ColorName;      //色を指定
                }
                if (hit.collider.GetComponent<MirrorManager>().GlassUse != true)
                {
                    hit.collider.GetComponent<MirrorManager>().ColorName = ColorName;      //色を指定
                }

            }

            //色
            switch (ColorName)
            {
                case (int)ColorType.NONE:
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;

                    lineRenderer.startColor = new Color(1, 1, 1, 0.5f);
                    lineRenderer.endColor = new Color(1, 1, 1, 0.5f);
                    break;
                case (int)ColorType.RED:
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                    break;
                case (int)ColorType.GREEN:
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.green;
                    break;
                case (int)ColorType.BLUE:
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.blue;
                    break;
                case (int)ColorType.YELLOW:
                    lineRenderer.startColor = Color.yellow;
                    lineRenderer.endColor = Color.yellow;
                    break;
                case (int)ColorType.WATER:
                    lineRenderer.startColor = Color.cyan;
                    lineRenderer.endColor = Color.cyan;
                    break;
                case (int)ColorType.PURPLE:
                    lineRenderer.startColor = Color.magenta;
                    lineRenderer.endColor = Color.magenta;
                    break;
                default:
                    break;
            }

            //lineRenderer.numCornerVertices  = 2;
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + direction);    //当たっていなかった場合光は進み続ける
            //lineRenderer.numCornerVertices  = 2;
        }

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        //Debug.DrawLine(transform.position, ray.origin + transform.forward * distance, Color.blue);
    }
}
