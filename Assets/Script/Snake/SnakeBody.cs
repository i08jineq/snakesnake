using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class SnakeBody : MonoBehaviour {
        #region main

        private Vector3 currentBasePosition;
        private Vector3 targetPosition;

        #endregion

        #region public method

        public void UpdateTargetPosition (Vector3 position) {
            currentBasePosition = targetPosition;
            targetPosition = position;
        }

        public void UpdateTransform(Vector3 lookAtPosition, float weight = 0) {
            Vector3 distanceVector = lookAtPosition - transform.position;
            transform.up = distanceVector;
            transform.position = Vector3.Lerp(currentBasePosition, targetPosition, weight);
        }


        #endregion
    }
}