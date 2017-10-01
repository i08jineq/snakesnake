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

        private float deltaTIme = 0;

        private const float UpdateTargetPositionPeriod = 0.03f;
        #endregion

        #region update

        void Update() {
            
            UpdateMovement();
            UpdateBody();
        }

        private void UpdateMovement() {
            float distance = movementSpeed * Time.deltaTime;
            transform.position += movementDirection * distance;
        }


        private void UpdateBody() {
            Vector3 targetPosition = transform.position - transform.up * bodySize;
            float deltaSpeed = movementSpeed * Time.deltaTime;
            foreach(var body in bodyList) {
                body.UpdateTransform(targetPosition, deltaSpeed);
                targetPosition = body.transform.position - body.transform.up * bodySize;
            }
            tail.UpdateTransform(targetPosition, deltaSpeed);
        }

        private void UpdateDeltaTIme() {
            deltaTIme += Time.deltaTime;
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
            body.transform.position = position - direction * bodySize;
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