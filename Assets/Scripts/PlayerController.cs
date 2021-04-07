using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed; //プレイヤーに加えるスピード用変数
    public float JumpForce; //プレイヤーのジャンプ力用変数
    private Rigidbody rb;
    public bool DirRight;   //右を向いているかどうかのフラグ
    private bool Jump;  //ジャンプが有効かどうかのフラグ
    public GameObject ObjectLight;

    public int Color;
    public int ColorCnt;
    public Text CntTex;


    void Start()
    {
        rb= GetComponent<Rigidbody>();
        Jump = false;   //初期は一応false
        DirRight = true;    //右を向いているかどうか
        Color = (int)LightManager.ColorType.RED;
        ColorCnt = 1;
        SetCnt();
    }

    void Update()
    {
        //LRスティックの角度を取得
        float lsh = Input.GetAxisRaw("L_Stick_H");
        float lsv = Input.GetAxisRaw("L_Stick_V");

        //角度のLog表示
        if ((lsh != 0) || (lsv != 0))
        {
            //Debug.Log("L stick:" + lsh + "," + lsv);
        }

        // transformを取得
        Transform myTransform = this.transform;

        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;

        //Bボタンを押していない場合は移動できる
        if (!Input.GetKey("joystick button 1"))
        {
            if (Input.GetKey(KeyCode.D))   //Dキーが押されたかどうかを判定している
            {
                DirRight = true;
                rb.AddForce(transform.right * MoveSpeed);       //右へMoveSpeed分、力を加えている
                worldAngle.y = 0.0f;
            }

            if (lsh >= 0.1)   //Dキーが押されたかどうかを判定している
            {
                DirRight = true;
                rb.AddForce(transform.right * MoveSpeed);       //右へMoveSpeed分、力を加えている
                worldAngle.y = 0.0f;
            }

            if (Input.GetKey(KeyCode.A))   //Aキーが押されたかどうかを判定している
            {
                DirRight = false;
                rb.AddForce(transform.right * MoveSpeed);       ///右へMoveSpeed分、力を加えている(キャラクターが回転して右の向きが変わっているためMoveSpeedでおｋ)
                worldAngle.y = 180.0f;
            }

            if (lsh <= -0.1)   //Aキーが押されたかどうかを判定している
            {
                DirRight = false;
                rb.AddForce(transform.right * MoveSpeed);       ///右へMoveSpeed分、力を加えている(キャラクターが回転して右の向きが変わっているためMoveSpeedでおｋ)
                worldAngle.y = 180.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown("joystick button 5"))
        {
            ColorCnt += 1;
            if(ColorCnt >= 4)
            {
                ColorCnt = 1;
            }
        }

        //今の色の表示(debug用)
        SetCnt();

        //色
        switch (ColorCnt)
        {
            case 1:
                Color = (int)LightManager.ColorType.RED;
                break;
            case 2:
                Color = (int)LightManager.ColorType.GREEN;
                break;
            case 3:
                Color = (int)LightManager.ColorType.BLUE;
                break;
            default:
                break;
        }

        if (Jump == true && Input.GetKeyDown("joystick button 0"))   //ジャンプが有効であり、スペースキーが押されたら瞬間
        {
            rb.AddForce(transform.up * JumpForce);       //上へJumpForce分、力を加えている
        }
        if (Jump == true && Input.GetKeyDown(KeyCode.Space))   //ジャンプが有効であり、スペースキーが押されたら瞬間
        {
            rb.AddForce(transform.up * JumpForce);       //上へJumpForce分、力を加えている
        }

        // 回転角度を設定
        myTransform.eulerAngles = worldAngle;

        //if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        //{
            //左右の入力が無ければx軸の速度減衰を行う
            rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z);
        //}
    }

    private void OnCollisionStay(Collision Collision)   //オブジェクトと当たっている時だけ(Stay)
    {
        if (Collision.gameObject.tag == "floor")   //floorというタグを持つオブジェクトと接触していたら
        {
            Jump = true;   //ジャンプ有効
        }
    }
    private void OnCollisionExit(Collision Collision)   //オブジェクトと当たっていない時だけ(Exit)
    {
        if (Collision.gameObject.tag == "floor")   //floorというタグを持つオブジェクトと接触していたら
        {
            Jump = false;   //ジャンプ無効
        }
    }

    void SetCnt()
    {
        CntTex.text = "Count: " + ColorCnt.ToString();
    }
}
