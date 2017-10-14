using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public interface IInputController
    {
        void UpdateInput();
        void RegisterSnake(ISnake _snake);
        void UnRegisterSnake(ISnake _snake);
    }
}