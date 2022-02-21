using System;
using System.Transactions;
using MyAssets.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyAssets.MenuFeature.State {
    public class FurnitureButton : Button {
        private Furniture _furniture;

        public FurnitureButton(Furniture furniture) {
            _furniture = furniture;
            style.backgroundImage = _furniture.image;
            style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            name = _furniture.name;
        }
    }
}