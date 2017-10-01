using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class Snake : MonoBehaviour, IControllableSnake {
        #region main

        [System.NonSerialized]public float movementSpeed = 1;
        [System.NonSerialized]public float bodySize = 1;

        private Vector3 movementDirection = Vector3.up;
        private List<SnakeBody> bodyList = new List<SnakeBody>();
        private SnakeBody tail;

        private float movedDeltaDistance = 0;

        #endregion

        #region update

        void Update() {
            if (bodySize <= movedDeltaDistance)
                UpdateBodyTargetPosition();
            
            UpdateMovement();
            UpdateBody();

        }

        private void UpdateMovement() {
            float distance = movementSpeed * Time.deltaTime;
            transform.position += movementDirection * distance;
            movedDeltaDistance += distance;
        }

        private void UpdateBodyTargetPosition() {
            Vector3 targetPosition = transform.position;
            foreach(var body in bodyList) {
                body.UpdateTargetPosition(targetPosition);
                targetPosition = body.transform.position;
            }
            tail.UpdateTargetPosition(targetPosition);
            //reset move distance
            movedDeltaDistance = 0;
        }

        private void UpdateBody() {
            Vector3 lookatPosition = transform.position;
            float delta = movedDeltaDistance / bodySize;
            foreach(var body in bodyList) {
                body.UpdateTransform(lookatPosition, delta);
                lookatPosition = body.transform.position;
            }
            tail.UpdateTransform(lookatPosition, delta);
        }

        #endregion


        #region public method


        public void Turn(Vector3 direction) {
            if (direction == -movementDirection) { // not allowed to suddenly moveback
                return;
            }
            movementDirection = direction;
            transform.up = direction;
        }

        #endregion

        #region AddBody Part

        public void AddBody(SnakeBody body) {
            bodyList.Add(body);
            SetupLastBodyTransform(body);
        }

        public void AddTail(SnakeBody snakeTail) {
            tail = snakeTail;
            SetupLastBodyTransform(tail);
        }

        public void SetupLastBodyTransform(SnakeBody body) {
            Vector3 position = GetLastBodyPosition();
            Vector3 direction = GetLastBodyDirection();
            body.transform.up = direction;
            body.transform.position = position - direction * bodySize;
        }

        #endregion

        #region getter method

        public Vector3 GetLastBodyPosition() {
            if (bodyList.Count == 0) {
                return transform.position;
            }
            int lastIndex = bodyList.Count - 1;
            SnakeBody body = bodyList[lastIndex];
            return body.transform.position;
        }

        public Vector3 GetLastBodyDirection() {
            if (bodyList.Count == 0) {
                return transform.up;
            }
            int lastIndex = bodyList.Count - 1;
            SnakeBody body = bodyList[lastIndex];
            return body.transform.up;
        }

        #endregion
    }
}