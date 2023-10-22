using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    public class BottleService : MonoBehaviour
    {
        [SerializeField] private BottleDatabase _bottleDatabase;

        public List<BottleData> Bottles => _bottleDatabase.Bottles;

        public BottleData GetPrevBottleData(BottleData from)
        {
            return GetBottleDataFrom(from, -1);
        }

        public BottleData GetNextBottleData(BottleData from)
        {
            return GetBottleDataFrom(from, 1);
        }

        private BottleData GetBottleDataFrom(BottleData from, int direction)
        {
            var fromIndex = Bottles.IndexOf(from);
            var nextIndex = (fromIndex + direction).Clamp(Bottles.Count);
            return Bottles[nextIndex];
        }
    }
}