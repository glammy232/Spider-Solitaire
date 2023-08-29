using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code;
using System.Xml.Serialization;

public class MainManager : MonoBehaviour
{
    public static MainManager MM;
    public bool On = true;
    public int OnMusic
    {
        get { return PlayerPrefs.GetInt("OnMusic", 0); }
        set { PlayerPrefs.SetInt("OnMusic", value); }
    }
    public bool FirstOrNo;
    public int NumOfAll = 0;
    public GameObject FirstOfAll;
    public List<GameObject> All;
    public void Start()
    {
        MM = this;
        ALLC = new List<List<List<Card>>>(50);
        ALLF = new List<List<List<bool>>>(50);
        ALLP = new List<List<List<bool>>>(50);
        NumOfMoves = 0;
        FirstOrNo = true;
    }
    public void StartWithThisParameters()
    {
        GameObject FOA = Instantiate(All[NumOfAll + 1]);
        All.Add(FOA);
        All[NumOfAll + 1].SetActive(true);
        MainManager.MM.FirstOrNo = false;
        All[NumOfAll].SetActive(false);
        GameManager.Instance.StartBool = true;
        GameManager.Instance.Columns[0].Cards.Clear();
        GameManager.Instance.Columns[1].Cards.Clear();
        GameManager.Instance.Columns[2].Cards.Clear();
        GameManager.Instance.Columns[3].Cards.Clear();
        GameManager.Instance.Columns[4].Cards.Clear();
        GameManager.Instance.Columns[5].Cards.Clear();
        GameManager.Instance.Columns[6].Cards.Clear();
        GameManager.Instance.Columns[7].Cards.Clear();
        GameManager.Instance.Columns[8].Cards.Clear();
        GameManager.Instance.Columns[9].Cards.Clear();
        GameManager.Instance.DealtCardsIndex = 0;
        GameManager.Instance.DealCards();
        if (OnMusic == 1)
        {
            TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.UnMuteAllSound();
        }
        else
        {
            TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.MuteAllSound();
        }
        ALLC.Clear();
        ALLF.Clear();
        ALLP.Clear();
        NumOfMoves = 0;
        NumOfAll++;
    }

    public List<List<Card>> AllCards;
    public List<List<bool>> AllFlip;
    public List<List<bool>> AllPick;

    public List<List<List<Card>>> ALLC = new List<List<List<Card>>>(50);
    public List<List<List<bool>>> ALLF = new List<List<List<bool>>>(50);
    public List<List<List<bool>>> ALLP = new List<List<List<bool>>>(50);

    public int NumOfMoves = 0;
    public void ForCancelMove()
    {
        List<Card> lCards0 = new List<Card>(30);
        List<Card> lCards1 = new List<Card>(30);
        List<Card> lCards2 = new List<Card>(30);
        List<Card> lCards3 = new List<Card>(30);
        List<Card> lCards4 = new List<Card>(30);
        List<Card> lCards5 = new List<Card>(30);
        List<Card> lCards6 = new List<Card>(30);
        List<Card> lCards7 = new List<Card>(30);
        List<Card> lCards8 = new List<Card>(30);
        List<Card> lCards9 = new List<Card>(30);

        List<bool> lFlip0 = new List<bool>(30);
        List<bool> lFlip1 = new List<bool>(30);
        List<bool> lFlip2 = new List<bool>(30);
        List<bool> lFlip3 = new List<bool>(30);
        List<bool> lFlip4 = new List<bool>(30);
        List<bool> lFlip5 = new List<bool>(30);
        List<bool> lFlip6 = new List<bool>(30);
        List<bool> lFlip7 = new List<bool>(30);
        List<bool> lFlip8 = new List<bool>(30);
        List<bool> lFlip9 = new List<bool>(30);

        List<bool> lPick0 = new List<bool>(30);
        List<bool> lPick1 = new List<bool>(30);
        List<bool> lPick2 = new List<bool>(30);
        List<bool> lPick3 = new List<bool>(30);
        List<bool> lPick4 = new List<bool>(30);
        List<bool> lPick5 = new List<bool>(30);
        List<bool> lPick6 = new List<bool>(30);
        List<bool> lPick7 = new List<bool>(30);
        List<bool> lPick8 = new List<bool>(30);
        List<bool> lPick9 = new List<bool>(30);

        for (int i = 0; i < GameManager.Instance.Columns[0].Cards.Count; i++)
        {
            lCards0.Add(GameManager.Instance.Columns[0].Cards[i]);
            lFlip0.Add(GameManager.Instance.Columns[0].Cards[i].fliped);
            lPick0.Add(GameManager.Instance.Columns[0].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[1].Cards.Count; i++)
        {
            lCards1.Add(GameManager.Instance.Columns[1].Cards[i]);
            lFlip1.Add(GameManager.Instance.Columns[1].Cards[i].fliped);
            lPick1.Add(GameManager.Instance.Columns[1].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[2].Cards.Count; i++)
        {
            lCards2.Add(GameManager.Instance.Columns[2].Cards[i]);
            lFlip2.Add(GameManager.Instance.Columns[2].Cards[i].fliped);
            lPick2.Add(GameManager.Instance.Columns[2].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[3].Cards.Count; i++)
        {
            lCards3.Add(GameManager.Instance.Columns[3].Cards[i]);
            lFlip3.Add(GameManager.Instance.Columns[3].Cards[i].fliped);
            lPick3.Add(GameManager.Instance.Columns[3].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[4].Cards.Count; i++)
        {
            lCards4.Add(GameManager.Instance.Columns[4].Cards[i]);
            lFlip4.Add(GameManager.Instance.Columns[4].Cards[i].fliped);
            lPick4.Add(GameManager.Instance.Columns[4].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[5].Cards.Count; i++)
        {
            lCards5.Add(GameManager.Instance.Columns[5].Cards[i]);
            lFlip5.Add(GameManager.Instance.Columns[5].Cards[i].fliped);
            lPick5.Add(GameManager.Instance.Columns[5].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[6].Cards.Count; i++)
        {
            lCards6.Add(GameManager.Instance.Columns[6].Cards[i]);
            lFlip6.Add(GameManager.Instance.Columns[6].Cards[i].fliped);
            lPick6.Add(GameManager.Instance.Columns[6].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[7].Cards.Count; i++)
        {
            lCards7.Add(GameManager.Instance.Columns[7].Cards[i]);
            lFlip7.Add(GameManager.Instance.Columns[7].Cards[i].fliped);
            lPick7.Add(GameManager.Instance.Columns[7].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[8].Cards.Count; i++)
        {
            lCards8.Add(GameManager.Instance.Columns[8].Cards[i]);
            lFlip8.Add(GameManager.Instance.Columns[8].Cards[i].fliped);
            lPick8.Add(GameManager.Instance.Columns[8].Cards[i].pickable);
        }
        for (int i = 0; i < GameManager.Instance.Columns[9].Cards.Count; i++)
        {
            lCards9.Add(GameManager.Instance.Columns[9].Cards[i]);
            lFlip9.Add(GameManager.Instance.Columns[9].Cards[i].fliped);
            lPick9.Add(GameManager.Instance.Columns[9].Cards[i].pickable);
        }

        AllCards = new List<List<Card>>(10);

        AllCards.Add(lCards0);
        AllCards.Add(lCards1);
        AllCards.Add(lCards2);
        AllCards.Add(lCards3);
        AllCards.Add(lCards4);
        AllCards.Add(lCards5);
        AllCards.Add(lCards6);
        AllCards.Add(lCards7);
        AllCards.Add(lCards8);
        AllCards.Add(lCards9);

        ALLC.Add(AllCards);

        AllFlip = new List<List<bool>>(10);

        AllFlip.Add(lFlip0);
        AllFlip.Add(lFlip1);
        AllFlip.Add(lFlip2);
        AllFlip.Add(lFlip3);
        AllFlip.Add(lFlip4);
        AllFlip.Add(lFlip5);
        AllFlip.Add(lFlip6);
        AllFlip.Add(lFlip7);
        AllFlip.Add(lFlip8);
        AllFlip.Add(lFlip9);

        ALLF.Add(AllFlip);

        AllPick = new List<List<bool>>(10);

        AllPick.Add(lPick0);
        AllPick.Add(lPick1);
        AllPick.Add(lPick2);
        AllPick.Add(lPick3);
        AllPick.Add(lPick4);
        AllPick.Add(lPick5);
        AllPick.Add(lPick6);
        AllPick.Add(lPick7);
        AllPick.Add(lPick8);
        AllPick.Add(lPick9);

        ALLP.Add(AllPick);

        NumOfMoves++;
    }
    public void CancelMove()
    {
        if (ALLC.Count != 0)
        {
            for (int i = 0; i < 10; i++)
            {
                GameManager.Instance.Columns[i].Cards.Clear();
            }

            for (int i = 0; i < 10; i++)
            {
                GameManager.Instance.Columns[i].AddCards(ALLC[NumOfMoves - 1][i]); // i + i * NumOfMoves
                for (int j = 0; j < GameManager.Instance.Columns[i].Cards.Count; j++)
                {
                    GameManager.Instance.Columns[i].Cards[j].fliped = ALLF[NumOfMoves - 1][i][j];
                    GameManager.Instance.Columns[i].Cards[j].pickable = ALLP[NumOfMoves - 1][i][j];
                    GameManager.Instance.Columns[i].Cards[j].Pickable = ALLP[NumOfMoves - 1][i][j];

                    if (GameManager.Instance.Columns[i].Cards[j].fliped == false)
                    {
                        GameManager.Instance.Columns[i].Cards[j].SpriteRenderer.sprite = GameManager.Instance.Columns[i].Cards[j].CarbackSprite;
                    }
                }
            }
            ALLC.Remove(ALLC[NumOfMoves - 1]);
            ALLF.Remove(ALLF[NumOfMoves - 1]);
            ALLP.Remove(ALLP[NumOfMoves - 1]);
            NumOfMoves--;
        }
    }
}