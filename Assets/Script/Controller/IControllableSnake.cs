using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public interface IControllableSnake {
        void Turn(Vector3 direction);
    }
}