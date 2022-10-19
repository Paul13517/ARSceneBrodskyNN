using System;
using Immerse.Core.UI;
using UnityEngine;

namespace Immerse.Brodsky.UI
{
    [RequireComponent(typeof(SliderScreen))]
    public abstract class BrodskyScreen : MonoBehaviour
    {
        public Action Exited;

        private SliderScreen _sliderScreen;
        private SliderScreen SliderScreen => _sliderScreen ??= GetComponentInChildren<SliderScreen>(true);
        

        public abstract void Init();

        public void HideToBase()
        {
            SliderScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Right, ScreenPosition.Left);
        }

        public void Open()
        {
            SliderScreen.Open(OnOpened);
        }

        protected virtual void OnOpened()
        {
        }

        private void Close()
        {
            SliderScreen.Close();
        }

        protected void Exit()
        {
            Close();
            Exited?.Invoke();
        }
    }
}