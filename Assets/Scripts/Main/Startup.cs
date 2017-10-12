using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SnakeSnake
{
    public class Startup : MonoBehaviour
    {
        
        [SerializeField]private Canvas mainCanvas;
        private StartUpUI startUpUI;

        #region Init

        void Awake()
        {
            CreateStartUpUI();
        }

        // Use this for initialization
        void Start()
        {
            SetupUIEvent();
        }

        #endregion

        #region private method

        private void CreateStartUpUI()
        {
            StartUpUI uiPrefab = Resources.Load<StartUpUI>(PrefabPath.StartUpUIPrefab);
            startUpUI = GameObject.Instantiate<StartUpUI>(uiPrefab, mainCanvas.transform);
        }

        private void SetupUIEvent()
        {
            startUpUI.AddStartButtonListener(OnClickStartButton);
        }

        private void OnClickStartButton()
        {
            RemoveAllUIListener();
            LoadGameScene();
        }

        private void RemoveAllUIListener()
        {
            startUpUI.RemoveAllUIAction();
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene(SceneName.GameScene);
        }

        #endregion
    }
}