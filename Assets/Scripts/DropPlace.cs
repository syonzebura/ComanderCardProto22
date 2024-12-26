using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// フィールドにアタッチするクラス
public class DropPlace : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        if (card != null) // もしカードがあれば、
        {
            if (card.model.canUse == true&&
                GetComponent<OnCardCheck>().oncheck==false)//使用可能なカードなら、かつカードが存在しなければ
            {
                card.movement.cardParent = this.transform;// カードの親要素を自分（アタッチされてるオブジェクト）にする
                card.DropField();//カードをフィールドにおいた時の処理をする

            }
            
        }
    }

    
}
