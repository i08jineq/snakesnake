using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class PrefabPath
    {
        public const string StartUpUIPrefab = "Prefabs/UI/StartUpUI";

        #region snake
        public const string SnakeHeadPath = "Prefabs/Snake/SnakeHead";
        public const string SnakeBodyPath = "Prefabs/Snake/SnakeBody";
        public const string SnakeTailPath = "Prefabs/Snake/SnakeTail";
        #endregion

        #region food
        public const string FoodPath = "Prefabs/Food/Food";
        #endregion
    }
}