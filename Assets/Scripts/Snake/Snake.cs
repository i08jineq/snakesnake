using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class Snake : MonoBehaviour, ISnake
    {
        private Vector3 moveDirection = Vector3.up;
        private float speed = 1;

        private Vector3 previousPosition;
        private Vector3 previousDirection;

        private IInputController inputController;
        private List<SnakeBodySegment> bodySegmentList = new List<SnakeBodySegment>();
        private SnakeBodySegment tail;

        public System.Action<Collider2D> onSnakeCollide;

        #region public methods

        public void Update()
        {
            UpdateInput();

            CachePreviousTransform();
            Move();

            UpdateBodySegment();
        }

        public void Turn(Vector3 direction)
        {
            if (direction == -moveDirection)
            {
                return;
            }
            moveDirection = direction;
            transform.up = direction;
        }

        public void AddBodySegment(SnakeBodySegment body)
        {
            Vector3 position = GetLastBodyPosition();
            Vector3 direction = GetLasBodyDirection();
            body.UpdateTransform(position, direction);
            bodySegmentList.Add(body);
        }

        public void AddTailSegment(SnakeBodySegment _tail)
        {
            Vector3 position = GetLastBodyPosition();
            Vector3 direction = GetLasBodyDirection();
            _tail.UpdateTransform(position, direction);
            tail = _tail;
        }

        public void SetSpeed(float _speed)
        {
            speed = _speed;
        }

        public void SetController(IInputController input)
        {
            inputController = input;
            inputController.RegisterSnake(this);
        }

        public void ClamPosition(float xRange, float yRange)
        {
            Vector3 position = transform.position;
            if (position.x != Mathf.Clamp(position.x, -xRange, xRange))
            {
                position.x = -position.x;
            }

            if (position.y != Mathf.Clamp(position.y, -yRange, yRange))
            {
                position.y = -position.y;
            }

            transform.position = position;
        }

        #endregion

        #region private methods

        private void UpdateInput()
        {
            inputController.UpdateInput();
        }

        private void CachePreviousTransform()
        {
            previousPosition = transform.position;
            previousDirection = transform.up;
        }

        private void Move()
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        private void UpdateBodySegment()
        {
            Vector3 targetPosition = previousPosition;
            Vector3 targetDirection = previousDirection;
            Vector3 segmentPreviousPosition = previousPosition;
            Vector3 segmentPreviousDirection = previousDirection;
            foreach (var segment in bodySegmentList)
            {
                segmentPreviousPosition = segment.GetPosition();
                segmentPreviousDirection = segment.GetDirection();
                segment.UpdateTransform(targetPosition, targetDirection);
                targetPosition = segmentPreviousPosition;
                targetDirection = segmentPreviousDirection;
            }

            tail.UpdateTransform(targetPosition, targetDirection);
        }

        private Vector3 GetLastBodyPosition()
        {
            if (bodySegmentList.Count == 0)
            {
                return previousPosition;
            }
            int lastIndex = bodySegmentList.Count - 1;
            Vector3 position = bodySegmentList[lastIndex].GetPosition();
            return position;
        }

        private Vector3 GetLasBodyDirection()
        {
            if (bodySegmentList.Count == 0)
            {
                return previousDirection;
            }
            int lastIndex = bodySegmentList.Count - 1;
            Vector3 direction = bodySegmentList[lastIndex].GetDirection();
            return direction;
        }

        #endregion

        #region events

        public void OnTriggerEnter2D(Collider2D col)
        {
            onSnakeCollide.Invoke(col);
        }

        #endregion
    }
}