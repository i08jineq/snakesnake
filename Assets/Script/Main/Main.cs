using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeSnake {
    public class Main : MonoBehaviour {

        #region factories

        private SnakeFactory snakeFactory;
        private FoodFactory foodFactory;
        private GameOverWindowFactory gameOverWindowFactory;
        private GameScoreUIFactory gameScoreUIFactory;

        #endregion

        #region observers

        private ICollideObserver snakeCollideObserver;

        #endregion

        #region main

        // game relate
        private Snake snake;
        private ASnakeController snakeController;
        private GameObject currentFoodObject;
        private int score = 0;

        // ui relate
        private Canvas mainCanvas;
        private GameOverWindow gameOverWindow;
        private GameScoreUI gameScoreUI;

        #endregion

        #region definition

        private const string GameScene = "GameScene";
        private const float snakeStartSpeed = 10;

        #endregion

        #region initial

        void Awake() {
            CreateSnakeHeadFactory();
            CreateFoodFactory();
            CreateGameOverWindowFactory();
            CreateGameScoreUIFactory();

            CreateSnakeCollideObserver();
        }

        void Start() {
            SearchMainCanvas();

            CreateSnake();
            CreateSnakeController();
            CreateFood();

            CreateGameOverWindow();
            CreateGameScoreUI();

            SetupGameOverWindowEvent();
            SetupSnakeEvent();
        }

        #endregion

        #region search item

        private void SearchMainCanvas() {
            mainCanvas = GameObject.FindObjectOfType<Canvas>();
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

        private void CreateGameOverWindowFactory() {
            gameOverWindowFactory = new GameOverWindowFactory();
            gameOverWindowFactory.PreloadPrefabs("UI/GameOverWindow");
        }

        private void CreateGameScoreUIFactory() {
            gameScoreUIFactory = new GameScoreUIFactory();
            gameScoreUIFactory.PreloadScoreUI("UI/ScoreUI");
        }

        #endregion

        #region create obeerver

        private void CreateSnakeCollideObserver() {
            var me = this;
            snakeCollideObserver = new MainSnakeCollideObserver(ref me);
        }

        #endregion

        #region create item

        private void CreateSnake() {
            Vector3 headPosition = new Vector3(-1, -1, 0);
            snake = snakeFactory.CreateSnake(headPosition, 1f, 1);
            snake.SetSpeed(snakeStartSpeed);
        }

        private void CreateSnakeController() {
            GameObject snackControllerObject = new GameObject("snake controller");
            snakeController = snackControllerObject.AddComponent<KeyboardController>();
            snakeController.Register(snake);
        }

        private void CreateGameOverWindow() {
            gameOverWindow = gameOverWindowFactory.CreateGameOverWindow(mainCanvas.transform);
        }

        private void CreateGameScoreUI() {
            gameScoreUI = gameScoreUIFactory.CreateGameScoreUI(mainCanvas.transform);
        }

        private void CreateFood() {
            Vector3 position = GetRandomPosition(9, 5);
            currentFoodObject = foodFactory.CreateFood(position);
        }


        #endregion

        #region setup event

        private void SetupGameOverWindowEvent() {
            gameOverWindow.OnRetry = OnRetry;
        }

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
                    OpenGameOverWindow();
                    DisableSnakeController();
                    break;
            }
        }

        private void IncreaseScore(int scoreUp = 1) {
            score += scoreUp;
            gameScoreUI.SetScore(score);
        }

        private void AddSnakeBody(int length = 2) {
            snakeFactory.AddSnakeBodies(snake, length);
        }

        private void DestroyCurrentFood() {
            Destroy(currentFoodObject);
        }

        private void DisableSnake() {
            snake.enabled = false;
        }

        private void DisableSnakeController() {
            snakeController.enabled = false;
        }

        private void OpenGameOverWindow() {
            gameOverWindow.Open(score);
        }

        private void OnRetry() {
            SceneManager.LoadScene(GameScene);
        }

        #endregion
    }
}