using System;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleData/Summon Companion", fileName = "New Summon Companion Bottle", order = 0)]
    public class SummonCompanionBottleData : BottleData
    {
        [Space(10)]
        public float Levels;
        public Color LevelsColor = Color.white;

        private const string LevelsPlaceholder = "{l}";

        public override BottleType BottleType => BottleType.SummonCompanion;

        public override string GetDescription()
        {
            var description = BaseDescription;
            if (description.Contains(LevelsPlaceholder))
            {
                description = description.Replace(
                    LevelsPlaceholder, $"{Levels}".WithColor(LevelsColor.ToHex()), StringComparison.Ordinal);
            }

            return description;
        }
    }
}