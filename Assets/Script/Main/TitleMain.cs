using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SnakeSnake {
    public class TitleMain : MonoBehaviour {

        #region main

        [SerializeField] private Button startButton;
        private bool isChangingScene;

        #endregion

        #region definition

        private const string GameScene = "GameScene";

        #endregion

        #region initial

        // Use this for initialization
        void Start() {
            SetupStartButtonEvent();
        }

        #endregion

        #region setupEvent

        private void SetupStartButtonEvent() {
            startButton.onClick.AddListener(ChangeToGameScene);
        }

        #endregion

        #region update

        // Update is called once per frame
        void Update() {
            UpdateInput();
        }

        private void UpdateInput() {
            if (Input.GetKeyDown(KeyCode.Return)) {
                ChangeToGameScene();
            }
        }

        #endregion

        #region event

        private void ChangeToGameScene() {
            if (isChangingScene) {
                return;
            }
            isChangingScene = true;
            SceneManager.LoadScene(GameScene);
        }

        #endregion
    }
}