using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public interface ICollideObserver {
        void OnCollideObject(Collider2D collider);
    }
}