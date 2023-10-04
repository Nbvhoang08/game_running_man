using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameOverScreen gameOverScreen;
    public player player;
    int maxflatform = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void GameOver()
    {
        gameOverScreen.Setup(maxflatform);

    }
}
