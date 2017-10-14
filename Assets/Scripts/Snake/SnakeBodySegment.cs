using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class SnakeBodySegment : MonoBehaviour
    {
        public void UpdateTransform(Vector3 targetPosition, Vector3 direction)
        {
            transform.up = direction;
            transform.position = targetPosition;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Vector3 GetDirection()
        {
            return transform.up;
        }
    }
}