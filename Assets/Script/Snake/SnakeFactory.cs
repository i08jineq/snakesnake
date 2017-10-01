using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class SnakeFactory {
        #region main

        private GameObject snakeHeadPrefab;
        private GameObject snakeBodyPrefab;
        private GameObject snakeTailPrefab;

        #endregion

        #region inital

        public void PreloadPrefab(string headPath, string bodyPath, string tailPath) {
            snakeHeadPrefab = Resources.Load<GameObject>(headPath);
            snakeBodyPrefab = Resources.Load<GameObject>(bodyPath);
            snakeTailPrefab = Resources.Load<GameObject>(tailPath);
        }

        #endregion

        #region create head method

        public Snake CreateSnake() {
            GameObject headGameObject = GameObject.Instantiate<GameObject>(snakeHeadPrefab);
            headGameObject.name = headGameObject.name.Replace("(Clone)", "");
            SetupPhysics(headGameObject);

            Snake snake = headGameObject.AddComponent<Snake>();

            return snake;
        }

        public Snake CreateSnake(Vector3 position, float bodySize, int bodyNumber) {
            Snake snake = CreateSnake();
            snake.transform.position = position;
            snake.bodySize = bodySize;

            AddSnakeBodies(snake, bodyNumber);
            AddSnakeTail(snake);
            return snake;
        }

        #endregion

        #region add body and tail method

        private SnakeBody CreateSnakeBodyObject(GameObject prefab) {
            GameObject snakeBodyGameObject = GameObject.Instantiate<GameObject>(prefab);
            snakeBodyGameObject.name = snakeBodyGameObject.name.Replace("(Clone)", "");

            SetupPhysics(snakeBodyGameObject);

            SnakeBody snakeBody = snakeBodyGameObject.AddComponent<SnakeBody>();
            return snakeBody;
        }

        private SnakeBody AddSnakeTail(Snake snake) {
            SnakeBody snakeTail = CreateSnakeBodyObject(snakeTailPrefab);
            snake.AddTail(snakeTail);
            return snakeTail;
        }


        public SnakeBody AddSnakeBody(Snake snake) {
            SnakeBody snakeBody = CreateSnakeBodyObject(snakeBodyPrefab);
            snake.AddBody(snakeBody);
            return snakeBody;
        }

        public List<SnakeBody> AddSnakeBodies(Snake snake, int numbers = 1) {
            List<SnakeBody> bodies = new List<SnakeBody>();
            for(int i = 0; i < numbers; i++) {
                SnakeBody body = AddSnakeBody(snake);
                bodies.Add(body);
            }
            return bodies;
        }

        #endregion

        #region setup

        private void SetupPhysics(GameObject target) {
            target.AddComponent<BoxCollider2D>();
            Rigidbody2D rigid = target.AddComponent<Rigidbody2D>();
            rigid.isKinematic = true;
            rigid.gravityScale = 0;
        }

        #endregion
    }
}