using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardModel
{
    public int cardId;
    //public string name;
    public int cost;
    public int HP;
    public Sprite icon;
    public int Attack;//一律1のつもりだが念の為。カードに表記はしない
    public int AttackType;

    public bool FieldCard = false;//カードが場にあるかどうか
    public bool canUse = false;//カードを使用可能か

    public CardModel(int cardID)
    {
        CardEntity cardEntity = Resources.Load<CardEntity>("CardEntityList/Card" + cardID);

        cardId = cardEntity.cardId;
        //name = cardEntity.name;
        cost = cardEntity.cost;
        HP = cardEntity.HP;
        icon = cardEntity.icon;
        Attack = cardEntity.Attack;
        AttackType = cardEntity.AttackType;
    }
}
