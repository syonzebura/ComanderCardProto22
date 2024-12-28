using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class balletController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 50f*Time.deltaTime, 0);
    }

    public void SetBalletRotation(float rotation)//balletの角度指定する
    {
        transform.Rotate(0, 0, rotation);
    }

    private void OnTriggerStay2D(Collider2D collision)//Enterだとカードゾーンにある敵の攻撃を踏んでくれないため採用
    {
        if (collision.gameObject == transform.parent.parent.gameObject)//攻撃元のカードにぶつかった時は何もしない
        {
            return;
        }
        else if (collision.gameObject.tag=="Card"&&
            collision.gameObject.GetComponent<CardController>().model.FieldCard==true)//カードに当たったかつフィールドカードであれば（出すか悩んでる際に球が当たらないようにする）
        {
            collision.gameObject.GetComponent<CardController>().Damage();//当たったカードにダメージ
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag== "EnemyLeader")//敵リーダーにぶつかった際の処理
        {
            GameManager.instance.LeaderHP(false);//敵リーダーにぶつかった際の処理
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerLeader")//自分リーダーにぶつかった際の処理
        {
            GameManager.instance.LeaderHP(true);//味方リーダーにぶつかった際の処理
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        
        
        //Destroy(this);
    }

    
    private void OnTriggerExit2D(Collider2D collision)//画面外にでたら消す
    {
        if (collision.gameObject.tag == "BalletArea")
        {
            Destroy(gameObject);
        }
    }
    




}
