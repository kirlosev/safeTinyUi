using System;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleData/Projectile Speed", fileName = "New Projectile Speed Bottle", order = 0)]
    public class ProjectileSpeedBottleData : BottleData
    {
        [Space(10)]
        public float ChangePercentage;
        public Color ChangePercentageColor = Color.white;

        [Space(10)]
        public int Levels;
        public Color LevelsColor = Color.white;

        [Space(10)]
        public float MaxPercentage;
        public Color MaxPercentageColor = Color.white;

        private const string ChangePercentagePlaceholder = "{cp}";
        private const string LevelsPlaceholder = "{l}";
        private const string MaxPercentagePlaceholder = "{mp}";

        public override BottleType BottleType => BottleType.ProjectileSpeed;

        public override string GetDescription()
        {
            var description = BaseDescription;
            if (description.Contains(ChangePercentagePlaceholder))
            {
                description = description.Replace(
                    ChangePercentagePlaceholder, $"{ChangePercentage}%".WithColor(ChangePercentageColor.ToHex()), StringComparison.Ordinal);
            }

            if (description.Contains(LevelsPlaceholder))
            {
                description = description.Replace(
                    LevelsPlaceholder, $"{Levels}".WithColor(LevelsColor.ToHex()), StringComparison.Ordinal);
            }

            if (description.Contains(MaxPercentagePlaceholder))
            {
                description = description.Replace(
                    MaxPercentagePlaceholder, $"+{MaxPercentage}%".WithColor(MaxPercentageColor.ToHex()), StringComparison.Ordinal);
            }

            return description;
        }
    }
}