using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public interface ISnake
    {
        void Update();
        void SetSpeed(float speed);
        void Turn(Vector3 direction);
        void AddBodySegment(SnakeBodySegment body);
        void AddTailSegment(SnakeBodySegment tail);
        void SetController(IInputController controller);
    }
}