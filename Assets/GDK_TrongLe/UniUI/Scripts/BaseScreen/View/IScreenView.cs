namespace GDK_TrongLe.UniUI.Scripts.BaseScreen.View
{
    using System;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniUI.Scripts.BaseUI;
    using UnityEngine;

    public interface IScreenView : IUIView
    {
        public RectTransform RectTransform { get; }
        public bool          IsReadyToUse  { get; }
        public UniTask       Open();
        public UniTask       Close();
        public void          Hide();
        public void          Show();

        public void DestroySelf();

        public event Action ViewDidClose;
        public event Action ViewDidOpen;
        public event Action ViewDidDestroy;
    }
}