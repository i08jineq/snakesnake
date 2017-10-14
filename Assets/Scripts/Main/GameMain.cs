using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class GameMain : MonoBehaviour
    {
        private SnakeFactory snakeFactory;
        private FoodFactory foodFactory;
        private Snake snake;

        private int score = 0;
        private bool isGameOver = false;
        [SerializeField]private float snakeStartSpeed = 5;
        [SerializeField]private int snakeStartLength = 3;
        [SerializeField]private int snakeIncreaseLength = 3;
        [SerializeField]private Canvas MainCanvas;

        private GameMainUI gameMainUI;

        #region initial

        void Awake()
        {
            CreateSnakeFactory();
            CreateFoodFactory();
            CreateGameMainUI();
            CreateSnake();
            CreateFood();
        }

        void Start()
        {
            SetupSnake();
        }

        #endregion

        #region Update

        void Update()
        {
            if (isGameOver)
            {
                return;
            }
            snake.Update();
        }

        #endregion

        #region create items

        private void CreateSnakeFactory()
        {
            snakeFactory = new SnakeFactory(PrefabPath.SnakeHeadPath, PrefabPath.SnakeBodyPath, PrefabPath.SnakeTailPath);
        }

        private void CreateFoodFactory()
        {
            foodFactory = new FoodFactory(PrefabPath.FoodPath);
        }

        private void CreateGameMainUI()
        {
            GameMainUI prefab = Resources.Load<GameMainUI>(PrefabPath.MainGameUIPrefab);
            gameMainUI = GameObject.Instantiate<GameMainUI>(prefab);
            gameMainUI.SetScore(0);
        }

        private void CreateSnake()
        {
            Vector3 position = new Vector3(-1, -1, 0);
            snake = snakeFactory.CreateSnake<KeyboardController>(position, true, snakeStartLength);
            snake.enabled = false;
            snake.onSnakeCollide = OnSnakeCollision;
        }

        private void CreateFood()
        {
            foodFactory.CreateFood<NormalFood>(5, 9);
        }

        #endregion

        #region setup items

        private void SetupSnake()
        {
            snake.SetSpeed(snakeStartSpeed);
        }

        #endregion

        #region events

        private void OnSnakeCollision(Collider2D collider)
        {
            int collidedLayer = collider.gameObject.layer;
            switch (collidedLayer)
            {
                case PhysicLayer.SnakeBodySegmentLayer:
                    OnSnakeCollidedBodySegment();
                    break;
                case PhysicLayer.FoodLayer:
                    OnSnakeCollidedFood(collider);
                    break;
            }
        }

        private void OnSnakeCollidedFood(Collider2D collider)
        {            
            IncreaseScore(collider);

            Destroy(collider.gameObject);

            CreateFood();

            IncreaseSnakeBodyLength();
        }

        private void OnSnakeCollidedBodySegment()
        {
            isGameOver = true;
        }

        private void IncreaseScore(Collider2D collider)
        {
            NormalFood food = collider.gameObject.GetComponent<NormalFood>();
            int increaseScore = food.GetScore();
            score += increaseScore;

            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            gameMainUI.SetScore(score);
        }

        private void IncreaseSnakeBodyLength()
        {
            snakeFactory.createBodies(snake, snakeIncreaseLength, true);
        }

        #endregion
    }
}