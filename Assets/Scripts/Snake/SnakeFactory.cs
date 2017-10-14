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

        #region public method

        private ISnake CreateSnake(Vector3 position, int startBodyNumber = 1)
        {
            GameObject snakeGameObject = GameObject.Instantiate<GameObject>(headPrefab);
            snakeGameObject.transform.position = position;
            Snake snake = snakeGameObject.AddComponent<Snake>();

            for(int i = 0; i < startBodyNumber; i++)
            {
                createBody(snake);
            }

            createTail(snake);

            return snake;
        }

        public ISnake CreateSnake<T>(Vector3 position, int startBodyNumber = 1) where T: IInputController, new()
        {
            ISnake snake = CreateSnake(position, startBodyNumber);
            T input = new T();
            snake.SetController(input);
            return snake;
        }

        public ISnakeBodySegment createBody(ISnake snake)
        {
            ISnakeBodySegment segment = createBodySegment(bodyPrefab);
            snake.AddBodySegment(segment);
            return segment;
        }

        public ISnakeBodySegment createTail(ISnake snake)
        {
            ISnakeBodySegment segment = createBodySegment(tailPrefab);
            snake.AddTailSegment(segment);
            return segment;
        }

        private ISnakeBodySegment createBodySegment(GameObject prefab)
        {
            GameObject body = GameObject.Instantiate<GameObject>(prefab);
            SnakeBodySegment bodySegment = body.AddComponent<SnakeBodySegment>();
            return bodySegment;
        }

        #endregion
    }
}