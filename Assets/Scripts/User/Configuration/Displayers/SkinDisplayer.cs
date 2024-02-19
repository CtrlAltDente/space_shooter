using SpaceShooter.Skins;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class SkinDisplayer : ConfigDisplayer<SkinChooser>
    {
        [SerializeField]
        private Skin _currentSkin;

        [SerializeField]
        private TextMeshProUGUI _informationText;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            InitializeSkin(_configChooser.Container.Items[configDataIndex]);
        }

        private void InitializeSkin(Skin skin)
        {
            InitializeSkinPart(skin.Head, _currentSkin.Head);
            InitializeSkinPart(skin.LeftHand, _currentSkin.LeftHand);
            InitializeSkinPart(skin.RightHand, _currentSkin.RightHand);
            ShowInformation(skin);
        }

        private void InitializeSkinPart(SkinPart selectedSkinPart, SkinPart displayerSkinPart)
        {
            displayerSkinPart.MeshFilter.sharedMesh = selectedSkinPart.MeshFilter.sharedMesh;
            displayerSkinPart.MeshRenderer.sharedMaterials = selectedSkinPart.MeshRenderer.sharedMaterials;
        }

        private void ShowInformation(Skin skin)
        {
            _informationText.text = skin.Description;
        }
    }
}