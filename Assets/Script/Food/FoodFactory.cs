using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake {
    public class FoodFactory {
        #region main

        private GameObject foodPrefab;

        #endregion

        #region initial

        public void PreloadPrefab(string prefabs) {
            foodPrefab = Resources.Load<GameObject>(prefabs);
        }

        #endregion

        #region create method

        public GameObject CreateFood(Vector3 position) {
            GameObject food = GameObject.Instantiate<GameObject>(foodPrefab);
            food.name = food.name.Replace("(Clone)", "");

            //setup physic2D
            food.AddComponent<BoxCollider2D>();
            food.layer = GameDefinition.PhysicLayer.FoodLayer;
            food.transform.position = position;

            return food;
        }

        #endregion
    }
}