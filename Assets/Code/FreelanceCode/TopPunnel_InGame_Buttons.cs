using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Code;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Linq;

public class TopPunnel_InGame_Buttons : MonoBehaviour
{
    public static TopPunnel_InGame_Buttons topPunnel_InGame_Buttons;
    public Button MuteBtn;
    public Button UnMuteBtn;

    public bool MayIGoOut;
    public bool MayIClick;

    public List<Column> Columns;
    public List<List<Column>> AllColumnsCards;
    public int NumOfMove;

    public List<Card> ForHelpVoidData;
    public List<Card> FinalCards;
    public List<Vector3> StartedMovePos;
    public List<Vector3> EndingMovePos;
    public int NumOfHelp;

    public AudioSource MainAudioSource;

    public void Start()
    {
        NumOfMove = 0;
        NumOfHelp = 0;
        topPunnel_InGame_Buttons = this;
        MayIClick = true;

        try
        {
            if (MainManager.MM.OnMusic == 1)
            {
                AudioListener.volume = 1;
                MainAudioSource.mute = false;
                UnMuteBtn.gameObject.SetActive(false);
                MuteBtn.gameObject.SetActive(true);
                MainManager.MM.On = true;
            }
            else if(MainManager.MM.OnMusic == 0)
            {
                AudioListener.volume = 0;
                MainAudioSource.mute = true;
                MuteBtn.gameObject.SetActive(false);
                UnMuteBtn.gameObject.SetActive(true);
                MainManager.MM.On = false;
            }
        }
        catch
        {
            
        }
    }
    public void HelpWithCardData()
    {
        ForHelpVoidData.Clear();
        FinalCards.Clear();
        for (int i = 0; i < GameManager.Instance.Cards.Count; i++)
        {
            if (GameManager.Instance.Cards[i].fliped == true && GameManager.Instance.Cards[i].pickable == true && GameManager.Instance.Cards[i].transform.position == GameManager.Instance.Cards[i].ParentColumn.Cards[GameManager.Instance.Cards[i].ParentColumn.Cards.Count - 1].transform.position)
            {
                ForHelpVoidData.Add(GameManager.Instance.Cards[i]);
            }
        }
    }
    public void OnClickHelp()
    {
        if (MayIClick == true)
        {
            Sort();
            MayIGoOut = true;
        }
    }
    public void Sort()
    {
        StartedMovePos.Clear();
        EndingMovePos.Clear();
        FinalCards.Clear();
        for (int i = 0; i < ForHelpVoidData.Count; i++)
        {
            for (int j = 0; j < ForHelpVoidData.Count; j++)
            {
                if (ForHelpVoidData[i].Value - ForHelpVoidData[j].Value == 1 && ForHelpVoidData[j].pickable == true && ForHelpVoidData[i].pickable == true &&
                    ForHelpVoidData[j] == ForHelpVoidData[j].ParentColumn.Cards[ForHelpVoidData[j].ParentColumn.Cards.Count - 1]/* && ForHelpVoidData[i].ParentColumn != ForHelpVoidData[j].ParentColumn*/)
                {
                    FinalCards.Add(ForHelpVoidData[j]);
                    StartedMovePos.Add(ForHelpVoidData[j].transform.position);
                    EndingMovePos.Add(ForHelpVoidData[i].transform.position);
                }
            }
        }
    }
    public void Update()
    {
        MoveCardBecauseOfHelp();
    }
    void MoveCardBecauseOfHelp()
    {
        if (MayIGoOut == true)
        {
            if (EndingMovePos.Count != 0)
            {
                if (NumOfHelp < EndingMovePos.Count)
                {
                    MayIClick = false;
                    Vector3 Target = new Vector3(EndingMovePos[NumOfHelp].x, EndingMovePos[NumOfHelp].y - 0.27f, EndingMovePos[NumOfHelp].z - 104);
                    if (FinalCards[NumOfHelp].transform.position != Target)
                    {
                        FinalCards[NumOfHelp].transform.position = Vector3.MoveTowards(FinalCards[NumOfHelp].transform.position, Target, 250f * Time.deltaTime);
                    }
                    else if (FinalCards[NumOfHelp].transform.position == Target)
                    {
                        MayIClick = false;
                        StartCoroutine(Wait(1f));
                        MayIGoOut = false;
                    }
                }
                else
                {
                    NumOfHelp = 0;
                }
            }
        }
    }
    void ForNumOfHelp()
    {
        if (NumOfHelp < FinalCards.Count - 1)
        {
            NumOfHelp++;
        }
        else
        {
            NumOfHelp = 0;
        }
    }
    public IEnumerator Wait(float sec)
    {
        if (MayIGoOut == true)
        {
            yield return new WaitForSeconds(sec);
            FinalCards[NumOfHelp].transform.position = StartedMovePos[NumOfHelp];
            ForNumOfHelp();
            MayIGoOut = false;
            MayIClick = true;
        }
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void NewGameOneSuit()
    {
        SceneManager.LoadScene(1);
    }
    public void NewGameTwoSuit()
    {
        SceneManager.LoadScene(2);
    }
    public void NewGameFourSuit()
    {
        SceneManager.LoadScene(3);
    }
    public void ResartGameSuit()
    {
        MainManager.MM.StartWithThisParameters();
    }
    public void MuteAllSound()
    {
        MainManager.MM.OnMusic = 0;
        AudioListener.volume = 0;
        MainAudioSource.mute = true;
        MuteBtn.gameObject.SetActive(false);
        UnMuteBtn.gameObject.SetActive(true);
        MainManager.MM.On = false;
    }

    public void UnMuteAllSound()
    {
        MainManager.MM.OnMusic = 1;
        AudioListener.volume = 1;
        MainAudioSource.mute = false;
        UnMuteBtn.gameObject.SetActive(false);
        MuteBtn.gameObject.SetActive(true);
        MainManager.MM.On = true;
    }
}