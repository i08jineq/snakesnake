using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class SnakeBody : MonoBehaviour {

        #region public method

        public void UpdateTransform(Vector3 targetPosition) {
            Vector3 distanceVector = targetPosition - transform.position;
            transform.up = distanceVector;
            transform.position = targetPosition;
        }

        #endregion
    }
}