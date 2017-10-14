using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class GameMain : MonoBehaviour
    {
        private SnakeFactory snakeFactory;
        private Snake snake;

        [SerializeField]private float snakeStartSpeed = 5;
        [SerializeField]private int snakeStartLength = 3;

        #region initial

        void Awake()
        {
            CreateSnakefactory();
            CreateSnake();
        }

        void Start()
        {
            SetupSnake();
        }

        #endregion

        #region Update

        void Update()
        {
            snake.Update();
        }

        #endregion

        #region create items

        private void CreateSnakefactory()
        {
            snakeFactory = new SnakeFactory(PrefabPath.SnakeHeadPrefab, PrefabPath.SnakeBodyPrefab, PrefabPath.SnakeTailPrefab);
        }

        private void CreateSnake()
        {
            Vector3 position = new Vector3(-1, -1, 0);
            snake = (Snake)snakeFactory.CreateSnake<KeyboardController>(position, snakeStartLength);
            snake.enabled = false;
        }

        #endregion

        #region setup items

        private void SetupSnake()
        {
            snake.SetSpeed(snakeStartSpeed);
        }

        #endregion
    }
}