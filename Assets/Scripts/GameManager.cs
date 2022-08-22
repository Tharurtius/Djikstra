using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GamePlayStates
    {
        Game,
        Pause,
        End
    }

    public static GamePlayStates state;

    public static int lives = 30;
    public Text livesCounter;

    public int money = 200;
    public Text moneyCounter;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update lives text
        livesCounter.text = $"Lives: {lives}";
        //update money text
        moneyCounter.text = $"${money}";
    }
}
