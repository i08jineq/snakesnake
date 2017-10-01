using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class SnakeBody : MonoBehaviour {

        #region public method

        public void UpdateTransform(Vector3 lookAtPosition, float speed = 0) {
            Vector3 distanceVector = lookAtPosition - transform.position;
            transform.up = distanceVector;
            transform.position = Vector3.MoveTowards(transform.position, lookAtPosition, speed);
        }

        #endregion
    }
}