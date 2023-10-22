using System;
using Extensions;
using UnityEngine;

namespace Bottle.Scripts
{
    [CreateAssetMenu(menuName = "tinyUi/BottleData/Vampirism", fileName = "New Vampirism Bottle", order = 0)]
    public class VampirismBottleData : BottleData
    {
        [Space(10)]
        public float RegenChance;
        public Color RegenChanceColor = Color.white;

        [Space(10)]
        public int HpHeal;
        public Color HpHealColor = Color.white;

        private const string RegenChancePlaceholder = "{rc}";
        private const string HpHealPlaceholder = "{hp}";

        public override BottleType BottleType => BottleType.Vampirism;

        public override string GetDescription()
        {
            var description = BaseDescription;
            if (description.Contains(RegenChancePlaceholder))
            {
                description = description.Replace(
                    RegenChancePlaceholder, $"{RegenChance}%".WithColor(RegenChanceColor.ToHex()), StringComparison.Ordinal);
            }

            if (description.Contains(HpHealPlaceholder))
            {
                description = description.Replace(
                    HpHealPlaceholder, $"{HpHeal}".WithColor(HpHealColor.ToHex()), StringComparison.Ordinal);
            }

            return description;
        }
    }
}