using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartOneSuit()
    {
        SceneManager.LoadScene(1);
    }
    public void StartTwoSuit()
    {
        SceneManager.LoadScene(2);
    }
    public void StartFourSuit()
    {
        SceneManager.LoadScene(3);
    }
}
