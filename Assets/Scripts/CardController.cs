using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardView view; // カードの見た目の処理
    public CardModel model; // カードのデータを処理

    public CardMovement movement;//移動(movement)に関することを操作

    private void Awake()
    {
        view = GetComponent<CardView>();
        movement = GetComponent<CardMovement>();
        
    }

    private void Start()
    {
        if (this.gameObject.transform.parent.parent.name == "EnemyFieldArea")//敵フィールドのフラグを立てる
        {
            model.FieldCard = true;
        }
    }

    public void Init(int cardID) // カードを生成した時に呼ばれる関数
    {
        model = new CardModel(cardID); // カードデータを生成
        view.Show(model); // 表示
    }

    public void DropField()
    {
        model.FieldCard = true;//フィールドカードのフラグを立てる
        GameManager.instance.ReduceManaPoint(model.cost);//マナを減らす
        model.canUse = false;
        view.SetCanUsePanel(model.canUse);//出した時にCanUsePanelを消す
        GameManager.instance.DrowCardForHand();//カードを引く
        
    }



    public void Damage()
    {
        model.HP -= 1;
        view.Show(model);
        if (model.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

   
    
    

}


