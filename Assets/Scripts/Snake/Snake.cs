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

        private IInputController inputController;
        private List<SnakeBodySegment> bodySegmentList = new List<SnakeBodySegment>();
        private SnakeBodySegment tail;

        public System.Action<Collider2D> onSnakeCollide;

        #region public methods

        public void Update()
        {
            UpdateInput();

            CachePreviousPosition();
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
            body.UpdateTransform(position);
            bodySegmentList.Add(body);
        }

        public void AddTailSegment(SnakeBodySegment _tail)
        {
            Vector3 position = GetLastBodyPosition();
            _tail.UpdateTransform(position);
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

        #endregion

        #region private methods

        private void UpdateInput()
        {
            inputController.UpdateInput();
        }

        private void CachePreviousPosition()
        {
            previousPosition = transform.position;
        }

        private void Move()
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        private void UpdateBodySegment()
        {
            Vector3 targetPosition = previousPosition;
            Vector3 segmentPreviousPosition = previousPosition;
            foreach (var segment in bodySegmentList)
            {
                segmentPreviousPosition = segment.GetPosition();
                segment.UpdateTransform(targetPosition);
                targetPosition = segmentPreviousPosition;
            }

            tail.UpdateTransform(targetPosition);
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

        #endregion

        #region events

        public void OnTriggerEnter2D(Collider2D col)
        {
            onSnakeCollide.Invoke(col);
        }

        #endregion
    }
}