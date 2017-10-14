using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class FoodFactory
    {
        private GameObject foodPrefab;

        #region initial

        public FoodFactory(string foodPrefabPath)
        {
            foodPrefab = Resources.Load<GameObject>(foodPrefabPath);
        }

        #endregion

        #region public method

        public IFood CreateFood<T>(float randomXRange, float randomYRange) where T: MonoBehaviour, IFood
        {
            float x = Random.Range(-randomXRange, randomXRange);
            float y = Random.Range(-randomYRange, randomYRange);
            Vector3 position = new Vector3(x, y, 0);
            T food = (T)CreateFood<T>(position);
            return food;
        }

        public IFood CreateFood<T>(Vector3 position) where T: MonoBehaviour, IFood
        {
            T food = (T)CreateFood<T>();
            food.transform.position = position;
            return food;
        }

        public IFood CreateFood<T>() where T: MonoBehaviour, IFood
        {
            GameObject foodGameObject = GameObject.Instantiate<GameObject>(foodPrefab);
            T food = foodGameObject.AddComponent<T>();
            return food;
        }

        #endregion

    }
}