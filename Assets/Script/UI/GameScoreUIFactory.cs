using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    
    public class GameScoreUIFactory {
        #region main

        private GameScoreUI scoreUIPrefab;

        #endregion

        #region preload

        public void PreloadScoreUI(string scoreUIPath) {
            scoreUIPrefab = Resources.Load<GameScoreUI>(scoreUIPath);
        }

        #endregion

        #region create method

        public GameScoreUI CreateGameScoreUI(Transform mainCanvasTransform) {
            GameScoreUI scoreUI = GameObject.Instantiate<GameScoreUI>(scoreUIPrefab, mainCanvasTransform);
            scoreUI.SetScore(0);
            return scoreUI;
        }

        #endregion
    }
}