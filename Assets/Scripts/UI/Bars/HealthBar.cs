using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class HealthBar : ValueBar
    {
        [SerializeField]
        private Image _valueImage;

        [SerializeField]
        private TextMeshProUGUI _valueText;

        public override void SetBarValue(float currentValue, float maxValue)
        {
            _valueImage.fillAmount = currentValue / maxValue;
            _valueText.text = $"{Mathf.RoundToInt(currentValue)}/{Mathf.RoundToInt(maxValue)}";
        }
    }
}