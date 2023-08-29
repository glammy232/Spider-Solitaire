using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code
{
    public class Card : MonoBehaviour
    {
        //
        public Vector3 PosInMouseDown;
        //
        public CardColors CardColor;
        public int Value;
        public Sprite Sprite;
        public Sprite CarbackSprite;

        //Questionable
        public Column ParentColumn;

        private Vector3 dragOffset;
        public Vector3 originalPosition;
        public Vector2 StartPosition;

        public List<Card> Children;

        public bool Pickable
        {
            get
            {
                return pickable;
            }
            set
            {
                pickable = value;
                if (!pickable && fliped == true)
                    SpriteRenderer.color = Color.gray;
                else
                    SpriteRenderer.color = Color.white;
            }
        }

        public bool Fliped
        {
            get
            {
                return fliped;
            }
            set
            {
                fliped = value;

                if (value)
                    SpriteRenderer.sprite = Sprite;
                else
                    SpriteRenderer.sprite = CarbackSprite;
            }
        }

        public bool fliped;
        public bool pickable = false;
        private bool picked;

        public SpriteRenderer spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (spriteRenderer == null)
                    spriteRenderer = GetComponent<SpriteRenderer>();

                return spriteRenderer;
            }
            set
            {
                spriteRenderer = value;
            }
        }
        public void OnMouseDown()
        {
            if (fliped == true && pickable == true)
            {
                GameManager.Instance.AudioSource.PlayOneShot(GameManager.Instance.AudioForCard);
            }
            PosInMouseDown = this.transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragOffset = this.transform.position - mousePos;

            picked = false;
            originalPosition = transform.position;

            Children = ParentColumn.GetChildrenCards(this);
        }

        public void OnMouseDrag()
        {
            Column ColumnInOnMouseDrag = new Column();
            if (!Pickable)
                return;

            picked = true;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 newPosition = mousePos + dragOffset;

            this.transform.position = new Vector2(newPosition.x, newPosition.y);

            MoveChildren(newPosition);

            //When card is Draged
            SetZOrder(110);

            if (Children != null)
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].SetZOrder(111 + i);
                }
        }

        public void SetZOrder(int orderInLayer)
        {
            this.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                orderInLayer * -1);
        }

        private void OnMouseUp()
        {
            if (fliped == true && pickable == true)
            {
                GameManager.Instance.AudioSource.PlayOneShot(GameManager.Instance.AudioForCard);
            }
            if (!picked)
                return;

            GameManager.Instance.SortDropedCard(this);
        }
        public void ReturnToOriginalPosition()
        {
            this.transform.position = originalPosition;

            MoveChildren(originalPosition);

            ParentColumn.RefreshRenderOrder();
        }

        public void MoveChildren(Vector2 newPos)
        {
            if (Children != null)
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].transform.position = new Vector2(newPos.x, newPos.y + (i + 1) * ParentColumn.YCardOffset);
                }
        }
    }
}
