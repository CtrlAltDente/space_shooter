using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class PercentageBar : MonoBehaviour
    {
        [SerializeField]
        private Image _percentageImage;

        public float Percentage
        {
            set
            {
                _percentageImage.fillAmount = value;
            }
        }
    }
}