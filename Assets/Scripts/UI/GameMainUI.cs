using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    using UnityEngine.UI;
    using UnityEngine.Events;
    public class GameMainUI : MonoBehaviour
    {
        [SerializeField]private Text scoreText;

        [SerializeField]private GameObject retryWindowGameObject;
        [SerializeField]private Text resultScoreText;
        [SerializeField]private Button retryButton;

        public void Init(UnityAction onRetryButtonPressed)
        {
            retryButton.onClick.AddListener(onRetryButtonPressed);
        }

        public void SetScore(int score)
        {
            scoreText.text = "Score : " + score.ToString();
        }

        public void OpenResultWindow(int score)
        {
            resultScoreText.text = score.ToString();
            retryWindowGameObject.SetActive(true);
        }
    }
}