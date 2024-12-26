using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEntity", menuName = "Create CardEntity")]

public class CardEntity : ScriptableObject
{
    public int cardId;
    //public new string name;
    public int cost;
    public int HP;
    public Sprite icon;
    public int Attack;//一律1のつもりだが念の為。カードに表記はしない
    public int AttackType;
}