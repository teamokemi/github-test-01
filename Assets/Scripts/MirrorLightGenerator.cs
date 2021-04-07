using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLightGenerator : MonoBehaviour
{
    Ray ray;
    RaycastHit hit; //ヒットしたオブジェクト情報
    LineRenderer lineRenderer;
    Vector3 HitPos; //当たった場所
    public float lineWidth; //光の幅(太さ)
    public int ColorNum;

    public enum ColorType
    {
        NONE = 0,

        RED,
        GREEN,
        BLUE,

    }
    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }


    void Update()
    {

    }

    public void ReflectionLight(Vector3 Vec, Vector3 pos, float width)  //光を出す
    {
        //if (this.GetComponent<MirrorManager>().Col == true)
        //{
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            int distance = 20;  //光の長さ
            lineRenderer.startWidth = width;    //光の最初の幅(太さ)
            lineRenderer.endWidth = width;   //光の最後の幅(太さ)
            Vector3 direction = transform.forward * 10.0f;

            //Rayの作成　　　↓Rayを飛ばす原点   ↓Rayを飛ばす方向
            //ray = new Ray(transform.position, Vec * distance);

            lineRenderer.SetPosition(0, pos);   //光の初期値

            //if (Physics.Raycast(ray.origin, direction, out hit, Mathf.Infinity))
            //{
            //    HitPos = hit.point;
            //    lineRenderer.SetPosition(1, HitPos);    //光の終着点

            //    Vector3 incomingVec = hit.point - transform.position;
            //    Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);  //反射角を計算している

            //    if (hit.collider.tag == "Mirror")   //rayが当たったオブジェクトのタグがMirrorだった場合
            //    {
            //        hit.collider.GetComponent<MirrorManager>().Col = true;
            //        hit.collider.GetComponent<MirrorManager>().reflectVec = reflectVec;
            //        hit.collider.GetComponent<MirrorManager>().HitPos = hit.point;
            //    }
            //}
            //else
            //{
            //    lineRenderer.SetPosition(1, transform.position + direction);
            //}

            //色の着色
            switch (ColorNum)
            {
                case (int)ColorType.NONE:
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;
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
            }

            //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　     ↓Rayの色
            Debug.DrawLine(pos, ray.origin + Vec * distance, Color.blue);
       // }

        //if (this.GetComponent<MirrorManager>().Col != true)
        //{
        //    //光が当たっていなければ光の幅をゼロにする(見えなくなる)
        //    Debug.Log("t");
        //    lineRenderer.startWidth = 0;
        //    lineRenderer.endWidth = 0;
        //}
    }
}

