using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform cardParent;
    bool canDrag = true; // カードを動かせるかどうかのフラグ


    public void OnBeginDrag(PointerEventData eventData) // ドラッグを始めるときに行う処理
    {
        CardController card = GetComponent<CardController>();
        canDrag = true;

        if (card.model.canUse == false) // マナコストより少ないカードは動かせない。ついでに場に出した後のカードもCanUseがFalseで動けない
        {
            canDrag = false;
        }

        if (canDrag == false)
        {
            return;
        }

        cardParent = transform.parent;
        if (transform.parent.parent.name == "PlayerFieldArea")//最終的には場においたら動かせなくするつもり。手札に戻す使用は技術的要件から無くなりそう
        {
            transform.SetParent(cardParent.parent.parent, false);
        }
        else
        {
            transform.SetParent(cardParent.parent, false);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycastsをオフにする
    }

    public void OnDrag(PointerEventData eventData) // ドラッグした時に起こす処理
    {
        if (canDrag == false)
        {
            return;
        }
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // カードを離したときに行う処理
    {
        if (canDrag == false)
        {
            return;
        }

        transform.SetParent(cardParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
    }
}

