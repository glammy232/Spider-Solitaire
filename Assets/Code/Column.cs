using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [System.Serializable]
    public class Column
    {
        public void RefreshRenderOrder()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].SetZOrder(i);
            }
        }
        public bool CanBeDroped(Card card)
        {
            if (Cards.Count == 0)
                return true;

            return Cards[Cards.Count - 1].Value == card.Value + 1;
        }

        public List<Card> Cards;
        public float XOrigin;

        public float YCardOffset;

        public Column()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            card.transform.position = new Vector3(XOrigin, Cards.Count * (YCardOffset));
            card.ParentColumn = this;
            Cards.Add(card);
            card.SetZOrder(Cards.Count);
        }
        public void AddCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                AddCard(card);
            }
        }
        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
            if (Cards.Count > 0)
                if (!Cards[Cards.Count - 1].Fliped)
                {
                    Cards[Cards.Count - 1].Fliped = true;
                    Cards[Cards.Count - 1].Pickable = true;
                }
        }

        public void RemoveCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                RemoveCard(card);
            }
        }

        public List<Card> GetChildrenCards(Card card)
        {
            int cardIndex = Cards.IndexOf(card);

            if (cardIndex == Cards.Count - 1)
                return (null);

            cardIndex++;
            return Cards.GetRange(cardIndex, Cards.Count - cardIndex);
        }

        public void RefreshPickable()
        {
            if (Cards.Count == 0)
                return;

            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Pickable = false;

            Cards[Cards.Count - 1].Pickable = true;
            for (int i = Cards.Count - 2; i >= 0; i--)
            {
                if (Cards[i].Value == Cards[i + 1].Value + 1 && Cards[i].Fliped &&
                    Cards[i].CardColor == Cards[i + 1].CardColor)
                    Cards[i].Pickable = true;
                else
                    break;
            }
        }

        public void CheckFinishedSequence()
        {
            int value = 0;
            CardColors cardColor = Cards[Cards.Count - 1].CardColor;

            for (int i = Cards.Count - 1; i > 0; i--)
            {
                if (Cards[i].Value != value || Cards[i].CardColor != cardColor)
                    break;

                if (Cards.Count - 1 >= 12 && Cards[i].Value == 0 && Cards[i - 1].Value == 1 && Cards[i - 2].Value == 2 && Cards[i - 3].Value == 3 && Cards[i - 4].Value == 4 && Cards[i - 5].Value == 5 && Cards[i - 6].Value == 6 && Cards[i - 7].Value == 7 && Cards[i - 8].Value == 8 && Cards[i - 9].Value == 9 && Cards[i - 10].Value == 10 && Cards[i - 11].Value == 11 && Cards[i - 12].Value == 12)
                {
                    MainManager.MM.ALLC.Clear();
                    MainManager.MM.ALLF.Clear();
                    MainManager.MM.ALLP.Clear();
                    MainManager.MM.NumOfMoves = 0;
                    //var doneCards = Cards.GetRange(i, 13/*(Cards.Count) - i*/);

                    var doneCards = new List<Card> { Cards[Cards.Count - 1], Cards[Cards.Count - 2], Cards[Cards.Count - 3], Cards[Cards.Count - 4], Cards[Cards.Count - 5], Cards[Cards.Count - 6], Cards[Cards.Count - 7], Cards[Cards.Count - 8], Cards[Cards.Count - 9], Cards[Cards.Count - 10], Cards[Cards.Count - 11], Cards[Cards.Count - 12], Cards[Cards.Count - 13] };

                    RemoveCards(doneCards);
                    RefreshPickable();
                    //Move cards from table.
                    for (int u = 0; u < doneCards.Count; u++)
                    {
                        doneCards[u].Pickable = false;
                        doneCards[u].spriteRenderer.sprite = doneCards[12].spriteRenderer.sprite;
                        doneCards[u].transform.position = GameManager.Instance.TargetForFinishedSeq.transform.position;
                        doneCards[u].spriteRenderer.color = Color.white;
                    }
                    GameManager.Instance.TargetForFinishedSeq.transform.position = new Vector3(GameManager.Instance.TargetForFinishedSeq.transform.position.x + 0.3f, GameManager.Instance.TargetForFinishedSeq.transform.position.y, GameManager.Instance.TargetForFinishedSeq.transform.position.z);
                }
            }
            value++;
        }
    }
}
