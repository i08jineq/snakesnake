using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SnakeSnake {
    public class KeyboardController : ASnakeController {
        private IControllableSnake snakeHead;

        public override void Register(IControllableSnake head) {
            snakeHead = head;
        }

        public override void UnRegister(IControllableSnake head) {
            snakeHead = null;
        }

        void Update() {
            if (snakeHead == null) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                snakeHead.Turn(Vector3.up);
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                snakeHead.Turn(Vector3.down);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                snakeHead.Turn(Vector3.left);
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                snakeHead.Turn(Vector3.right);
            }
                
        }
    }
}