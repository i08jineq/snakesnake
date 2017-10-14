using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public interface ISnakeBodySegment
    {
        void UpdateTransform(Vector3 targetPosition);
        Vector3 GetPosition();
    }
}