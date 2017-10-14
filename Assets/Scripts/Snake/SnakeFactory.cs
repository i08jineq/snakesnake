using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class SnakeFactory
    {
        private GameObject headPrefab;
        private GameObject bodyPrefab;
        private GameObject tailPrefab;

        #region initial

        public SnakeFactory(string headPath, string bodyPath, string tailPath)
        {
            headPrefab = Resources.Load<GameObject>(headPath);
            bodyPrefab = Resources.Load<GameObject>(bodyPath);
            tailPrefab = Resources.Load<GameObject>(tailPath);
        }

        #endregion

        #region public methods

        public Snake CreateSnake<T>(Vector3 position, bool usePhysic = true, int startBodyNumber = 1) where T: IInputController, new()
        {
            Snake snake = CreateSnake(position, usePhysic, startBodyNumber);
            T input = new T();
            snake.SetController(input);
            return snake;
        }

        public List<SnakeBodySegment> createBodies(ISnake snake, int number, bool usePhysic)
        {
            List<SnakeBodySegment> segmentList = new List<SnakeBodySegment>();
            for (int i = 0; i < number; i++)
            {
                SnakeBodySegment segment = createBody(snake, usePhysic);
                segmentList.Add(segment);
            }

            return segmentList;
        }

        public SnakeBodySegment createBody(ISnake snake, bool usePhysic)
        {
            SnakeBodySegment segment = createBodySegment(bodyPrefab, usePhysic);
            snake.AddBodySegment(segment);
            return segment;
        }

        public SnakeBodySegment createTail(ISnake snake, bool usePhysic)
        {
            SnakeBodySegment segment = createBodySegment(tailPrefab, usePhysic);
            snake.AddTailSegment(segment);
            return segment;
        }

        #endregion

        #region private methods

        private Snake CreateSnake(Vector3 position, bool usePhysic = true, int startBodyNumber = 1)
        {
            Snake snake = (Snake)CreateSnake();
            snake.transform.position = position;
            if (usePhysic)
            {
                SetupSnakePhysic(snake);
            }

            for (int i = 0; i < startBodyNumber; i++)
            {
                createBody(snake, usePhysic);
            }

            createTail(snake, usePhysic);

            return snake;
        }

        private Snake CreateSnake()
        {
            GameObject snakeGameObject = GameObject.Instantiate<GameObject>(headPrefab);
            snakeGameObject.name = snakeGameObject.name.Replace("(Clone)", "");
            snakeGameObject.layer = PhysicLayer.SnakeLayer;
            Snake snake = snakeGameObject.AddComponent<Snake>();
            return snake;
        }

        private SnakeBodySegment createBodySegment(GameObject prefab, bool usePhysic)
        {
            GameObject bodyGameObject = GameObject.Instantiate<GameObject>(prefab);
            bodyGameObject.name = bodyGameObject.name.Replace("(Clone)", "");
            bodyGameObject.layer = PhysicLayer.SnakeBodySegmentLayer;

            if (usePhysic)
            {
                BoxCollider2D collider = bodyGameObject.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;
                Rigidbody2D rigid = bodyGameObject.AddComponent<Rigidbody2D>();
                rigid.gravityScale = 0;
                rigid.isKinematic = true;
            }

            SnakeBodySegment bodySegment = bodyGameObject.AddComponent<SnakeBodySegment>();
            return bodySegment;
        }

        private void SetupSnakePhysic(Snake snake)
        {
            BoxCollider2D collider = snake.gameObject.AddComponent<BoxCollider2D>();
            collider.offset = new Vector2(0, 0.5f);
            collider.size = new Vector2(0.25f, 0.25f);
            collider.isTrigger = true;
            Rigidbody2D rigid = snake.gameObject.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            rigid.isKinematic = true;
        }

        #endregion
    }
}