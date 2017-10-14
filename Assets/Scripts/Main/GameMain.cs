using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class GameMain : MonoBehaviour
    {
        private SnakeFactory snakeFactory;
        private Snake snake;

        #region initial

        void Awake()
        {
            CreateSnakefactory();
            CreateSnake();
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
            snake = (Snake)snakeFactory.CreateSnake<KeyboardController>(position);
            snake.enabled = false;
        }

        #endregion
    }
}