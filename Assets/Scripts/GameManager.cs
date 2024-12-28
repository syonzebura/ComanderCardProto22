using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] Transform playerHand,playerFieldArea,enemyFieldArea;

    //以下、マナポイント実装
    [SerializeField] Text playerManaPointText;
    [SerializeField] Text playerDefaultManaPointText;
    public int playerManaPoint;//使用すると減るマナポイント
    public int playerDefaultManaPoint;//毎ターン増えていくベースのマナポイント
    [SerializeField] GameObject ManaGage;//マナゲージ
    private bool managanarateBool = true;//マナ生成のbool

    //敵生成のbool
    private bool enemyGanarateBool=true;
    //敵の位置取得用のスクリプト取得
    //private OnCardCheck check;
    [SerializeField] Transform EF1, EF2, EF3, EF4, EF5;

    //以下、リーダー関連
    [SerializeField] Text playerLeaderHPText;//HPのテキスト
    [SerializeField] Text enemyLeaderHPText;
    private int playerLeaderHP=50;//HPの変数
    private int enemyLeaderHP=50;

    //以下、GameManagerのstatic化
    public static GameManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        
        /*
        //以下、フィールドに直接生成する時の処理
        //自軍の2番目と4番目にカード生成
        List<Transform> PFch = new List<Transform>();
        foreach(Transform child in playerFieldArea.GetComponentsInChildren<Transform>())//GetComponentsInChildrenは自分自身も取得するためロジックで弾く
        {
            if (child.gameObject.name=="PlayerFieldArea") continue;
            
            PFch.Add(child); 
            
        }
        Instantiate(cardPrefab, PFch[1]);
        Instantiate(cardPrefab, PFch[3]);

        //敵軍の2番目と4番目にカード生成
        List<Transform> EFch = new List<Transform>();
        foreach (Transform child in enemyFieldArea.GetComponentsInChildren<Transform>())//GetComponentsInChildrenは自分自身も取得するためロジックで弾く
        {
            if (child.gameObject.name == "EnemyFieldArea") continue;

            EFch.Add(child);

        }
        Instantiate(cardPrefab, EFch[1]);
        Instantiate(cardPrefab, EFch[3]);
        */

        StartGame();



    }

    private void Update()
    {

        //敵の生成
        if (enemyGanarateBool == true)
        {
           
            StartCoroutine("EnemyGanarater");
            enemyGanarateBool = false;
     
            
        }
        if (managanarateBool == true)
        {
            StartCoroutine("GanarateMana");
            managanarateBool = false;
        }


    }

    

    void StartGame()
    {
        //各プレイヤーのHP表示
        playerLeaderHPText.text = playerLeaderHP.ToString();
        enemyLeaderHPText.text = enemyLeaderHP.ToString();

        //マナの初期値設定
        playerManaPoint = 5;
        playerDefaultManaPoint = 5;
        ShowManaPoint();

        /*
        //敵陣にカード生成
        List<Transform> EFch = new List<Transform>();
        foreach (Transform child in enemyFieldArea.GetComponentsInChildren<Transform>())//GetComponentsInChildrenは自分自身も取得するためロジックで弾く
        {
            if (child.gameObject.name == "EnemyFieldArea") continue;
            EFch.Add(child);
        }
        CreateCard(1,180,EFch[1]);
        */
        SetStartHand();//初期手札をドローする
        
    }

    //
    //以下、マナ系
    void ShowManaPoint() // マナポイントを表示するメソッド
    {
        playerManaPointText.text = playerManaPoint.ToString();
        playerDefaultManaPointText.text = playerDefaultManaPoint.ToString();
    }

    public void ReduceManaPoint(int cost) // コストの分、マナポイントを減らす
    {
        playerManaPoint -= cost;
        if (playerManaPoint == 0)
        {
            playerManaPointText.color = Color.red;
        }
        else
        {
            playerManaPointText.color = Color.white;
        }
        ShowManaPoint();

        SetCanUsePanelHand();
        
    }
    //以下、マナ生成AND管理
    IEnumerator GanarateMana()
    {
        yield return new WaitForSeconds(0.01f);
        this.ManaGage.GetComponent<Image>().fillAmount += 0.002f;//ココで速度変えるのがベストか
        if (this.ManaGage.GetComponent<Image>().fillAmount == 1)//もしゲージが満タンになったら
        {
            this.ManaGage.GetComponent<Image>().fillAmount = 0;//ゲージリセット
            if (playerManaPoint != playerDefaultManaPoint)//マナが満タンでないなら
            {
                playerManaPoint += 1;
                ShowManaPoint();
                playerManaPointText.color = Color.white;
                SetCanUsePanelHand();
            }
            
        }
        managanarateBool = true;
    }





    //
    //以下、手札系
    void SetCanUsePanelHand() // 手札のカードを取得して、使用可能なカードにCanUseパネルを付ける。ドローの際にも確認
    {
        CardController[] playerHandCardList = playerHand.GetComponentsInChildren<CardController>();
        foreach (CardController card in playerHandCardList)
        {
            if (card.model.cost <= playerManaPoint)
            {
                card.model.canUse = true;
                card.view.SetCanUsePanel(card.model.canUse);
            }
            else
            {
                card.model.canUse = false;
                card.view.SetCanUsePanel(card.model.canUse);
            }
        }
        

    }

    void CreateCard(int cardID,float carddc,Transform place)//カード生成の処理
    {
        CardController card = Instantiate(cardPrefab,place.transform.position,Quaternion.Euler(0,0,carddc),place);
        card.Init(cardID);
    }

    public void DrowCard(Transform hand)//カードをドローする時の処理
    {
        int cardNumber = Random.Range(1, 6);//ココで引くカードの種類調整
        CreateCard(cardNumber, 0, hand);
        SetCanUsePanelHand();
    }
    void SetStartHand()//初期手札をセット
    {
        for(int i=0; i < 5; i++)
        {
            DrowCard(playerHand);
        }
    }
    public void DrowCardForHand()//外部スクリプトから手札を引く処理をする際に使用
    {
        DrowCard(playerHand);
    }






    
    //
    //以下、敵生成コルーチン
    IEnumerator EnemyGanarater()
    {
        yield return new WaitForSeconds(4.0f);
        int rn = Random.Range(1, 6);//どのますに生成するか
        switch (rn)
        {
            
            case 1:
                if (EF1.transform.childCount == 0)
                {
                    CreateCard(Random.Range(1,6), 180, EF1);//改造時はココ変更
                }
                break;
            case 2:
                if (EF2.transform.childCount == 0)
                {
                    CreateCard(Random.Range(1, 6), 180, EF2);//改造時はココ変更
                }
                break;
            case 3:
                if (EF3.transform.childCount == 0)
                {
                    CreateCard(Random.Range(1, 6), 180, EF3);//改造時はココ変更
                }
                break;
            case 4:
                if (EF4.transform.childCount == 0)
                {
                    CreateCard(Random.Range(1, 6), 180, EF4);//改造時はココ変更
                }
                break;
            case 5:
                if (EF5.transform.childCount == 0)
                {
                    CreateCard(Random.Range(1, 6), 180, EF5);//改造時はココ変更
                }
                break;
            default:
                break;
        }
        enemyGanarateBool = true;
    }



    //
    //以下、リーダーHP関連
    public void LeaderHP(bool whichLeader)//boolはtureなら自分リーダー、falseなら敵リーダー
    {
        if (whichLeader == true)//もしplayerリーダーなら
        {
            playerLeaderHP -= 1;//攻撃力が変わればここも引数で受け取るようにする
            playerLeaderHPText.text = playerLeaderHP.ToString();
        }
        else
        {
            enemyLeaderHP -= 1;
            enemyLeaderHPText.text = enemyLeaderHP.ToString();
        }
    }



}
