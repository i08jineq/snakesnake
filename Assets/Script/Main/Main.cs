using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class Main : MonoBehaviour {
        #region factories

        private SnakeFactory snakeFactory;
        private FoodFactory foodFactory;

        #endregion

        #region observer

        private ICollideObserver snakeCollideObserver;

        #endregion

        #region main

        private Snake snake;
        private ASnakeController snakeController;
        private GameObject currentFoodObject;
        private int score = 0;

        #endregion

        #region definition

        private const float snakeStartSpeed = 10;

        #endregion

        #region initial

        void Awake() {
            CreateSnakeHeadFactory();
            CreateFoodFactory();

            CreateSnakeCollideObserver();
        }

        void Start() {
            CreateSnake();
            CreateSnakeController();

            CreateFood();
            SetupSnakeEvent();
        }

        #endregion

        #region create factories

        private void CreateSnakeHeadFactory() {
            snakeFactory = new SnakeFactory();
            snakeFactory.PreloadPrefab("Snake/Head", "Snake/Body", "Snake/Tail");
        }

        private void CreateFoodFactory() {
            foodFactory = new FoodFactory();
            foodFactory.PreloadPrefab("Foods/Food");
        }

        #endregion

        #region create obeerver

        private void CreateSnakeCollideObserver() {
            snakeCollideObserver = new MainSnakeCollideObserver(this);
        }

        #endregion

        #region create item

        private void CreateSnake() {
            Vector3 headPosition = new Vector3(-1, -1, 0);
            snake = snakeFactory.CreateSnake(headPosition, 0.2f, 1);
            snake.SetSpeed(snakeStartSpeed);
        }

        private void CreateSnakeController() {
            GameObject snackControllerObject = new GameObject("snake controller");
            snakeController = snackControllerObject.AddComponent<KeyboardController>();
            snakeController.Register(snake);
        }

        private void CreateFood() {
            Vector3 position = GetRandomPosition(9, 5);
            currentFoodObject = foodFactory.CreateFood(position);
        }

        #endregion

        #region setup event

        private void SetupSnakeEvent() {
            snake.RegisterCollideObserver(snakeCollideObserver);
        }

        #endregion

        #region private method

        private Vector3 GetRandomPosition(float xRange, float yRange) {
            float x = Random.Range(-xRange, xRange);
            float y = Random.Range(-yRange, yRange);
            Vector3 position = new Vector3(x, y, 0);
            return position;
        }

        #endregion

        #region events

        public void OnSnakeCollideObject(Collider2D collider) {
            int objectLayer = collider.gameObject.layer;
            switch(objectLayer) {
                case GameDefinition.PhysicLayer.FoodLayer:
                    //score up
                    IncreaseScore(1);
                    DestroyCurrentFood();
                    CreateFood();
                    AddSnakeBody();
                    break;
                case GameDefinition.PhysicLayer.SnakeBodyLayer:
                    //game over
                    DisableSnake();
                    DestroyCurrentFood();
                    break;
            }
        }

        private void IncreaseScore(int scoreUp = 1) {
            score += scoreUp;
        }

        private void AddSnakeBody() {
            snakeFactory.AddSnakeBodies(snake, 2);
        }

        private void DestroyCurrentFood() {
            Destroy(currentFoodObject);
        }

        private void DisableSnake() {
            snake.enabled = false;
        }

        #endregion
    }
}