using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class GameOverWindowFactory {
        #region prefab

        private GameOverWindow gameOverWindow;

        #endregion


        #region preload

        public void PreloadPrefabs(string gameoverWindowPath) {
            gameOverWindow = Resources.Load<GameOverWindow>(gameoverWindowPath);
        }

        #endregion

        #region create method

        public GameOverWindow CreateGameOverWindow(Transform canvasTransform) {
            GameOverWindow window = GameObject.Instantiate<GameOverWindow>(gameOverWindow, canvasTransform);
            window.Init();
            window.gameObject.SetActive(false); //hide when created
            return window;
        }

        #endregion
    }
}