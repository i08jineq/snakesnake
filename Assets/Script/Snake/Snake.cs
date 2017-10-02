using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class Snake : MonoBehaviour, IControllableSnake {
        #region main

        [System.NonSerialized]public float bodySize = 1;

        private float movementSpeed = 1;
        private Vector3 movementDirection = Vector3.up;
        private List<SnakeBody> bodyList = new List<SnakeBody>();
        private SnakeBody tail;
        private ICollideObserver collideOberver;
        private float movementRangeX = 10;
        private float movementRangeY = 5;


        private Vector3 previousPosition;
        private const float UpdateTargetPositionPeriod = 0.03f;
        #endregion

        #region update

        void Update() {
            UpdateMovement();
            UpdateBody();
        }

        private void UpdateMovement() {
            float distance = movementSpeed * Time.deltaTime;
            previousPosition = transform.position;
            Vector3 position = transform.position;
            position += movementDirection * distance;

            float x = position.x;
            float y = position.y;
            float clampX = Mathf.Clamp(x, -movementRangeX, movementRangeX);
            float clampY = Mathf.Clamp(y, -movementRangeY, movementRangeY);
            if(x != clampX) {
                x = clampX;
                x = -x;
            }
            if(y != clampY) {
                y = clampY;
                y = -y;
            }
            position.x = x;
            position.y = y;
            transform.position = position;
        }


        private void UpdateBody() {
            Vector3 targetPosition = previousPosition;
            foreach(var body in bodyList) {
                previousPosition = body.transform.position;
                body.UpdateTransform(targetPosition);
                targetPosition = previousPosition;
            }
            tail.UpdateTransform(targetPosition);
        }

        #endregion


        #region public method

        public void RegisterCollideObserver(ICollideObserver observer) {
            collideOberver = observer;
        }

        public void UnRegisterCollideObserver(ICollideObserver observer) {
            collideOberver = null;
        }

        public void SetSpeed(float speed) {
            movementSpeed = speed;
        }

        public void SetMovementRange(float rangeX, float rangeY) { 
            movementRangeX = rangeX;
            movementRangeY = rangeY;
        }

        public void Turn(Vector3 direction) {
            if (direction == -movementDirection) { // not allowed to suddenly moveback
                return;
            }
            movementDirection = direction;
            transform.up = direction;

            UpdateMovement();
            UpdateBody();
        }

        #endregion

        #region AddBody Part

        public void AddBody(SnakeBody body) {
            SetupLastBodyTransform(body);
            bodyList.Add(body);
        }

        public void AddTail(SnakeBody snakeTail) {
            SetupLastBodyTransform(snakeTail);
            tail = snakeTail;
        }

        public void SetupLastBodyTransform(SnakeBody body) {
            Vector3 position = GetLastBodyPosition();
            Vector3 direction = GetLastBodyDirection();
            body.transform.up = direction;
            body.transform.position = position;
        }

        #endregion

        #region getter method

        public Vector3 GetLastBodyPosition() {
            if (bodyList.Count <= 0) {
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

        #region event

        void OnTriggerEnter2D(Collider2D collider) {
            collideOberver.OnCollideObject(collider);
        }

        #endregion
    }
}