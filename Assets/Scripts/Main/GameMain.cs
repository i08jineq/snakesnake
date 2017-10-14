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

        [SerializeField]private float snakeStartSpeed = 5;
        [SerializeField]private int snakeStartLength = 3;

        #region initial

        void Awake()
        {
            CreateSnakeFactory();
            CreateFoodFactory();
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
                    OnSnakeCollidedFood();
                    break;
            }
        }

        private void OnSnakeCollidedFood()
        {
            
        }

        private void OnSnakeCollidedBodySegment()
        {
        }

        #endregion
    }
}