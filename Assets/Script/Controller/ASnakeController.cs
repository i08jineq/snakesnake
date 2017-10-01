using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public abstract class ASnakeController : MonoBehaviour {
        public abstract void Register(IControllableSnake head);
        public abstract void UnRegister(IControllableSnake head);
    }
}