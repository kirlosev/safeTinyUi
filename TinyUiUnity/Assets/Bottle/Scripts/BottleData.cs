using UnityEngine;
using UnityEngine.UI;

namespace Bottle.Scripts
{
    public abstract class BottleData : ScriptableObject
    {
        public string Id;
        public bool IsAvailable;

        [Space(10)]
        public string Title;
        public string BaseDescription;
        public Sprite Icon;
        public Image BottleViewPrefab;

        public abstract BottleType BottleType { get; }
        public abstract string GetDescription();
    }
}
