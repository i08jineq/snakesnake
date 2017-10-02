using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeSnake {
    public class GameScoreUI : MonoBehaviour {
        #region main

        [SerializeField]private Text scoreText;

        #endregion

        #region public method

        public void SetScore(int score) {
            scoreText.text = score.ToString();
        }

        #endregion
    }
}