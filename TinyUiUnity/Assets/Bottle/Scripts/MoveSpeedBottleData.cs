using System;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleData/Move Speed", fileName = "New Move Speed Bottle", order = 0)]
    public class MoveSpeedBottleData : BottleData
    {
        [Space(10)]
        public float SpeedIncrease;
        public Color SpeedIncreaseColor = Color.white;

        private const string SpeedIncreasePlaceholder = "{si}";

        public override BottleType BottleType => BottleType.MoveSpeed;

        public override string GetDescription()
        {
            var description = BaseDescription;
            if (description.Contains(SpeedIncreasePlaceholder))
            {
                description = description.Replace(
                    SpeedIncreasePlaceholder, $"{SpeedIncrease}%".WithColor(SpeedIncreaseColor.ToHex()), StringComparison.Ordinal);
            }

            return description;
        }
    }
}