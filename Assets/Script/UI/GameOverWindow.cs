using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeSnake {
    public class GameOverWindow : MonoBehaviour {
        #region main

        [SerializeField]private Button retryButton;
        [SerializeField]Text scoreText;

        #endregion

        #region event callback

        public System.Action OnRetry;

        #endregion

        #region initial

        public void Init() {
            SetupRetryButton();
        }

        private void SetupRetryButton() {
            retryButton.onClick.AddListener(OnPressedRetryButton);
        }

        #endregion

        #region public method

        public void Open(int score) {
            gameObject.SetActive(true);
            scoreText.text = score.ToString();
        }

        #endregion


        #region event

        private void OnPressedRetryButton() {
            if (OnRetry != null) {
                OnRetry();
            }
        }

        #endregion
    }
}