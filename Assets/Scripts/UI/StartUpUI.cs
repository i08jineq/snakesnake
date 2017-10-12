using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    using UnityEngine.UI;
    using UnityEngine.Events;

    public class StartUpUI : MonoBehaviour
    {
        [SerializeField]private Button startButton;

        #region public method

        public void AddStartButtonListener(UnityAction action)
        {
            startButton.onClick.AddListener(action);
        }

        public void RemoveAllUIAction()
        {
            startButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}