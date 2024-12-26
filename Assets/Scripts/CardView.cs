using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text  hpText, costText;
    [SerializeField] Image iconImage;
    [SerializeField] GameObject canAttackPanel,canUsePanel;

    public void Show(CardModel cardModel) // cardModelのデータ取得と反映
    {
        //nameText.text = cardModel.name;
        hpText.text = cardModel.HP.ToString();
        costText.text = cardModel.cost.ToString();
        iconImage.sprite = cardModel.icon;
        //Attackは表記しない
    }
    public void SetCanAttackPanel(bool flag)
    {
        canAttackPanel.SetActive(flag);
    }
    public void SetCanUsePanel(bool flag) // フラグに合わせてCanUsePanelを付けるor消す
    {
        canUsePanel.SetActive(flag);
    }
}
