using System.Collections.Generic;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleDatabase", fileName = "New Bottle Database", order = 0)]
    public class BottleDatabase : ScriptableObject
    {
        public List<BottleData> Bottles = new();
    }
}