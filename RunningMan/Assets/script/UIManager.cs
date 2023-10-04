using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Text textScore;
    public void setScore(int score)
    {
        textScore.text = score.ToString();
    }
}
