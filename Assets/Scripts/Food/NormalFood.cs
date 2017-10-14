using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeSnake
{
    public class NormalFood : MonoBehaviour, IFood
    {
        public int GetScore()
        {
            return 1;
        }
    }
}