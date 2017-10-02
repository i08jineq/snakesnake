using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class MainSnakeCollideObserver : ICollideObserver {
        #region main

        private Main main;

        #endregion

        #region initial

        public MainSnakeCollideObserver(ref Main m) {
            main = m;
        }

        #endregion

        public void OnCollideObject(Collider2D collider) {
            main.OnSnakeCollideObject(collider);
        }
    }
}