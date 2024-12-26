using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardCheck : MonoBehaviour
{
    public bool oncheck = false;//カードが置かれているかどうか

    private void Update()
    {
        if (gameObject.transform.childCount >= 1)//カードが1枚以上置かれていたら
        {
            oncheck = true;
        }
        else
        {
            oncheck = false;
        }
        
    }
}
