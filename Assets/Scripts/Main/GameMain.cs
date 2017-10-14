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
            snake = snakeFactory.CreateSnake<KeyboardController>(position, true, snakeStartLength);
            snake.enabled = false;
            snake.onSnakeCollide = OnSnakeCollision;
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