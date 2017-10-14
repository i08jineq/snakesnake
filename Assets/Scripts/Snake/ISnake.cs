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
        void AddBodySegment(ISnakeBodySegment body);
        void AddTailSegment(ISnakeBodySegment tail);
        void SetController(IInputController controller);
    }
}