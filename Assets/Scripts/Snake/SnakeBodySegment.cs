using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class SnakeBodySegment : MonoBehaviour
    {
        public void UpdateTransform(Vector3 targetPosition)
        {
            if (targetPosition != transform.position)
            {
                transform.up = targetPosition - transform.position;
            }
            transform.position = targetPosition;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}