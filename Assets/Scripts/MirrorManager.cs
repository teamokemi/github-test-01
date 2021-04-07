using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit; //ヒットしたオブジェクト情報
    LineRenderer lineRenderer;
    public Vector3 HitPos; //当たった場所
    public float lineWidth; //光の幅(太さ)
    public bool Col;    //光が当たっているかどうか
    public Vector3 incomingVec;
    public Vector3 reflectVec;  //反射角を計算している
    public int HitColor;
    public Transform LightTopPos;
    public bool GlassUse;   //ガラスがセットできるか
    public bool GlassSet;   //ガラスがセットされているか

    public int ColorName;

    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        Col = false;
        ray = new Ray(transform.position, transform.forward * 10);
        LightTopPos = transform.GetChild(0);

        HitColor = (int)LightManager.ColorType.NONE;
        ColorName = (int)LightManager.ColorType.NONE;
        GlassUse = false;
    }

    void Update()
    {
        //ColorName = HitColor;

        //if (GlassUse == true)
        //{
        //    if (HitColor == ColorName)
        //    {
        //        ColorName = HitColor;
        //    }

        //    if (HitColor != ColorName)
        //    {
        //        HitColor = ColorName;
        //        ColorName = HitColor;
        //    }
        //}

        Debug.DrawLine(transform.position, ray.origin + transform.forward * 10, Color.blue);

        if (Col == true)
        {
            GetComponent<LightManager>().LightCheck(lineWidth, HitPos, ray, lineRenderer, reflectVec, "Mirror", HitPos, LightTopPos, Col, ColorName);
        }
        if (Col == false)
        {
            GetComponent<LightManager>().LightCheck(0, HitPos, ray, lineRenderer, reflectVec, "Mirror", HitPos, LightTopPos, Col , ColorName);
        }

        //transform.GetChild(0).transform.position = LightTopPos;
    }
}
