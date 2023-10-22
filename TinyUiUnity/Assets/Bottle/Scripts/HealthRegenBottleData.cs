using System;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleData/Health Regen", fileName = "New Health Regen Bottle", order = 0)]
    public class HealthRegenBottleData : BottleData
    {
        [Space(10)]
        public float RegenPercentage;
        public Color RegenPercentageColor = Color.white;

        [Space(10)]
        public int MinutesToRegen;
        public Color MinutesToRegenColor = Color.white;

        private const string RegenPercentagePlaceholder = "{rp}";
        private const string MinutesToRegenPlaceholder = "{m}";

        public override BottleType BottleType => BottleType.HealthRegen;

        public override string GetDescription()
        {
            var description = BaseDescription;
            if (description.Contains(RegenPercentagePlaceholder))
            {
                description = description.Replace(
                    RegenPercentagePlaceholder, $"{RegenPercentage}%".WithColor(RegenPercentageColor.ToHex()), StringComparison.Ordinal);
            }

            if (description.Contains(MinutesToRegenPlaceholder))
            {
                description = description.Replace(
                    MinutesToRegenPlaceholder, $"{MinutesToRegen}".WithColor(MinutesToRegenColor.ToHex()), StringComparison.Ordinal);
            }

            return description;
        }
    }
}
