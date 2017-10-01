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
            GameObject snakeGameObject = GameObject.Instantiate<GameObject>(snakeHeadPrefab);
            snakeGameObject.name = snakeGameObject.name.Replace("(Clone)", "");
            AddRigidBody(snakeGameObject, GameDefinition.PhysicLayer.SnakeLayer);
            BoxCollider2D collider = AddBoxCollider(snakeGameObject);
            collider.offset = new Vector2(0, 0.75f);
            collider.size = new Vector2(0.5f, 0.25f);
            collider.isTrigger = true;

            Snake snake = snakeGameObject.AddComponent<Snake>();

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

            AddRigidBody(snakeBodyGameObject, GameDefinition.PhysicLayer.SnakeBodyLayer);
            AddBoxCollider(snakeBodyGameObject);

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

        private Rigidbody2D AddRigidBody(GameObject target, int physicLayer) {
            Rigidbody2D rigid = target.AddComponent<Rigidbody2D>();
            rigid.isKinematic = true;
            rigid.gravityScale = 0;
            target.layer = physicLayer;
            return rigid;
        }

        private BoxCollider2D AddBoxCollider(GameObject target) {
            BoxCollider2D collider = target.AddComponent<BoxCollider2D>();
            return collider;
        }

        #endregion
    }
}