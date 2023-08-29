using UnityEngine;
using Assets.Code;

public class CardMusic : MonoBehaviour
{
    public static CardMusic cardMusic;
    public int ForinvokeRepeating;
    public AudioSource AudioSource1;
    public AudioClip AudioForCard1;
    public bool StartBoolForMusic;
    public void Start()
    {
        cardMusic = this;
        ForinvokeRepeating = 0;
        StartBoolForMusic = true;
        InvokeRepeating("CiklMusic", 0f, 0.1f);
    }
    void CiklMusic()
    {
        if (ForinvokeRepeating < 54 && StartBoolForMusic == true && MainManager.MM.FirstOrNo == true)
        {
            AudioSource1.PlayOneShot(AudioForCard1);
            ForinvokeRepeating++;
            if (ForinvokeRepeating > 49)
            {
                ForinvokeRepeating = 0;
                StartBoolForMusic = false;
            }
        }
    }
}
