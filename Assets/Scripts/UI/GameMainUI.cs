using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    using UnityEngine.UI;

    public class GameMainUI : MonoBehaviour
    {
        
        [SerializeField]private Text scoreText;

        public void SetScore(int score)
        {
            scoreText.text = "Score : " + score.ToString();
        }
    }
}