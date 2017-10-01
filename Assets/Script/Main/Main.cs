using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class Main : MonoBehaviour {
        #region factories

        private SnakeFactory snakeFactory;

        #endregion

        #region main

        private Snake snake;
        private ASnakeController snakeController;

        #endregion

        #region definition

        private const float snakeStartSpeed = 5;

        #endregion

        #region initial

        void Awake() {
            CreateSnakeHeadFactory();
        }

        void Start() {
            CreateSnake();
            CreateSnakeController();
        }

        #endregion

        #region create factories

        private void CreateSnakeHeadFactory() {
            snakeFactory = new SnakeFactory();
            snakeFactory.PreloadPrefab("Snake/Head", "Snake/Body", "Snake/Tail");
        }
        #endregion

        #region create item

        private void CreateSnake() {
            Vector3 headPosition = new Vector3(-1, -1, 0);
            snake = snakeFactory.CreateSnake(headPosition, 1, 0);
            snake.movementSpeed = snakeStartSpeed;
        }

        private void CreateSnakeController() {
            GameObject snackControllerObject = new GameObject("snake controller");
            snakeController = snackControllerObject.AddComponent<KeyboardController>();
            snakeController.Register(snake);
        }

        #endregion

        #region setup item

        #endregion
    }
}