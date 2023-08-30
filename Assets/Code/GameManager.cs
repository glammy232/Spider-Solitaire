using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.Net;
using System;
using UnityEngine.UIElements;

namespace Assets.Code
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public DealSettings DealSettings;

        public Sprite Cardback;
        public Card CardPrefab;

        public List<Card> Cards;

        public GameObject CardHolder;

        public Column[] Columns;
        public AudioClip AudioForCard;
        public AudioSource AudioSource;

        public bool StopAd;
      
        public List<CardSet> CardSets;

        public int DealtCardsIndex = 0;
        public int StockDealt = 0;

        public bool StartBool;
        public GameObject AllCardsAnimation;

        public bool Y = false;

        public Transform TargetForFinishedSeq;

        public void Update()
        {
            if (MainManager.MM.FirstOrNo == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CardMusic.cardMusic.StartBoolForMusic = false;
                    if (StartBool == true)
                    {
                        AllCardsAnimation.SetActive(false);
                        StartY(StartBool);
                        StartBool = false;
                    }
                }
            }
            else
            {
                CardMusic.cardMusic.StartBoolForMusic = false;
                if (StartBool == true)
                {
                    AllCardsAnimation.SetActive(false);
                    StartY(StartBool);
                    StartBool = false;
                }
            }
        }

        void StartY(bool CanI)
        {
            if (CanI == true)
            {
                Instance = this;

                Columns = new Column[10];

                if (MainManager.MM.FirstOrNo == true)
                {
                    GenerateCards(DealSettings);
                }

                for (int i = 0; i < 10; i++)
                {
                    Columns[i] = new Column();
                    Columns[i].YCardOffset = -0.2f;
                    Columns[i].XOrigin = i * 1.1f;
                }
                if (MainManager.MM.FirstOrNo == true)
                {
                    Shuffle(Cards);
                }
                DealCards();

                //TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.ForCancelMove();
            }
        }
        private void Start()
        {
            StartBool = true;
            StartCoroutine(Wait(4.835f));
        }
        public IEnumerator Wait(float sec)
        {
            if (MainManager.MM.FirstOrNo == true)
            {
                yield return new WaitForSeconds(sec);
                AllCardsAnimation.SetActive(false);
                StartY(StartBool);
                StartBool = false;
            }
        }
        private void GenerateCards(DealSettings dealSettings)
        {
            if (dealSettings == DealSettings.OneSuit)
            {
                for (int i = 0; i < 8; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Club));
                }
            }
            else if(dealSettings == DealSettings.TwoSuit)
            {
                for (int i = 0; i < 4; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Club));
                }

                for (int i = 0; i < 4; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Hearth));
                }
            }
            else if(dealSettings == DealSettings.FourSuit)
            {
                for (int i = 0; i < 2; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Club));
                }

                for (int i = 0; i < 2; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Hearth));
                }
                for (int i = 0; i < 2; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Diamond));
                }

                for (int i = 0; i < 2; i++)
                {
                    Cards.AddRange(GenerateCards(CardColors.Spade));
                }
            }
        }

        public void DealFromStock()
        {
            if (StockDealt == 5)
                return;

            TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.ForHelpVoidData.Clear();
            for (int i = 0; i < 10; i++)
            {
                TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.ForHelpVoidData.Add(Cards[DealtCardsIndex]);
                Columns[i].AddCard(Cards[DealtCardsIndex]);

                Cards[DealtCardsIndex].Fliped = true;
                Cards[DealtCardsIndex].Pickable = true;

                Columns[i].RefreshPickable();
                DealtCardsIndex++;
            }
            MainManager.MM.ALLC.Clear();
            MainManager.MM.ALLF.Clear();
            MainManager.MM.ALLP.Clear();
            MainManager.MM.NumOfMoves = 0;
            StockDealt++;
        }

        public void DealCards()
        {
            for (int i = 0; i < 10; i++)
            {
                int unFlipedCards = 5;
                if (i > 3)
                {
                    unFlipedCards = 4;
                }

                Columns[i].AddCards(Cards.GetRange(DealtCardsIndex, unFlipedCards));
                DealtCardsIndex += unFlipedCards;

                Cards[DealtCardsIndex].Fliped = true;
                Cards[DealtCardsIndex].Pickable = true;
                Columns[i].AddCard(Cards[DealtCardsIndex]);
                DealtCardsIndex++;
            }
            TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.HelpWithCardData();
        }
        Card[] GenerateCards(CardColors color)
        {
            Card[] cards = new Card[13];

            Sprite[] sprites = CardSets[(int)color].Sprites;

            for (int i = 0; i < 13; i++)
            {   
                Card newCard = GameObject.Instantiate<Card>(CardPrefab, 
                    CardHolder.transform);

                cards[i] = newCard;
                cards[i].Value = i;
                cards[i].CardColor = color;
                cards[i].CarbackSprite = Cardback;
                cards[i].Sprite = sprites[i];

                cards[i].Fliped = false;
            }
            return (cards);
        }
        void Shuffle(List<Card> cards)
        {
            System.Random random = new System.Random();
            int n = cards.Count - 1;
            while (n > 1)
            {
                int k = random.Next(n);
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
                n--;
            }
            MainManager.MM.All.Add(MainManager.MM.FirstOfAll);
            GameObject FOA = Instantiate(MainManager.MM.FirstOfAll);
            MainManager.MM.All.Add(FOA);
            MainManager.MM.All[1].SetActive(false);
        }

        public void SortDropedCard(Card card)
        {
            var colX = card.transform.position.x / 1.1f;

            int colNum = Mathf.RoundToInt(colX);
            if (colNum > 9 || colNum < 0)
            {
                card.ReturnToOriginalPosition();
                return;
            }
            try
            {
                if (Columns[colNum].CanBeDroped(card))
                {
                    MainManager.MM.ForCancelMove();

                    if (card.Children != null)
                        card.ParentColumn.RemoveCards(card.Children);

                    card.ParentColumn.RemoveCard(card);
                    card.ParentColumn.RefreshPickable();

                    Columns[colNum].AddCard(card);
                    if (card.Children != null)
                        Columns[colNum].AddCards(card.Children);

                    Columns[colNum].RefreshPickable();
                    Columns[colNum].CheckFinishedSequence();
                    TopPunnel_InGame_Buttons.topPunnel_InGame_Buttons.HelpWithCardData();

                    GameManager.Instance.AudioSource.PlayOneShot(GameManager.Instance.AudioForCard);
                }
                else
                {
                    card.ReturnToOriginalPosition();
                }
            }
            catch (Exception e) when (e is IndexOutOfRangeException)
            {
                Debug.Log(e);
                card.ReturnToOriginalPosition();
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException)
            {
                Debug.Log(e);
            }
        }
    }
}
