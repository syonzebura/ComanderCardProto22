using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balletGanarater : MonoBehaviour
{
    //ballet生成用
    bool ganaratebool = true;//update関数内で制御するためのbool
    [SerializeField] balletController bc;//球
    [SerializeField] GameObject costIcom;//球発射位置取得用
    //[SerializeField] Transform canvas;
    private CardModel model;

    private void Start()
    {
        this.model = GetComponent<CardController>().model;//cardmodelを取得
    }

    void Update()
    {
        //ballet生成用
        if (ganaratebool == true&&this.model.FieldCard==true)//ganarateboolとFieldCard（場に存在するかどうか）がtrueなら
        {
            StartCoroutine("GanarateBallet");
            ganaratebool = false;
        }
        //balet用ココまで
    }

    //ballet生成用
    IEnumerator GanarateBallet()
    {
        switch (model.AttackType)
        {
            case 1:
                balletController Ibc1 = Instantiate(bc, this.costIcom.transform);
                Ibc1.GetComponent<balletController>().SetBalletRotation(0);//角度を引数に指定
                break;
            case 2:
                balletController Ibc2 = Instantiate(bc, this.costIcom.transform);
                Ibc2.GetComponent<balletController>().SetBalletRotation(45);//角度を引数に指定
                break;
            case 3:
                balletController Ibc3 = Instantiate(bc, this.costIcom.transform);
                Ibc3.GetComponent<balletController>().SetBalletRotation(-45);//角度を引数に指定
                break;
            case 4:
                balletController Ibc4_1 = Instantiate(bc, this.costIcom.transform);
                Ibc4_1.GetComponent<balletController>().SetBalletRotation(45);//角度を引数に指定
                balletController Ibc4_2 = Instantiate(bc, this.costIcom.transform);
                Ibc4_2.GetComponent<balletController>().SetBalletRotation(-45);//角度を引数に指定
                break;
            case 5:
                balletController Ibc5_1 = Instantiate(bc, this.costIcom.transform);
                Ibc5_1.GetComponent<balletController>().SetBalletRotation(0);//角度を引数に指定
                balletController Ibc5_2 = Instantiate(bc, this.costIcom.transform);
                Ibc5_2.GetComponent<balletController>().SetBalletRotation(45);//角度を引数に指定
                balletController Ibc5_3 = Instantiate(bc, this.costIcom.transform);
                Ibc5_3.GetComponent<balletController>().SetBalletRotation(-45);//角度を引数に指定
                break;
        }
        
        /*
        balletController Ibc1 = Instantiate(bc, this.costIcom.transform);
        Ibc1.GetComponent<balletController>().SetBalletRotation(45);//角度を引数に指定
        */

        yield return new WaitForSeconds(0.5f);
        ganaratebool = true;
    }
    //balet用ココまで
}
