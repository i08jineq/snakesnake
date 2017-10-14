using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class KeyboardController : IInputController
    {
        private ISnake snake;

        public void UpdateInput()
        {
            if (snake == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                snake.Turn(Vector3.up);
            } else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                snake.Turn(Vector3.down);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                snake.Turn(Vector3.left);
            } else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                snake.Turn(Vector3.right);
            }
        }

        public void RegisterSnake(ISnake _snake)
        {
            snake = _snake;
        }

        public void UnRegisterSnake(ISnake _snake)
        {
            snake = null;
        }
    }
}