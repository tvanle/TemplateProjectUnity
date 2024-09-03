namespace tvan.uni.foundation.UniUI.Scripts.Manager
{
    using UnityEngine;

    public class RootUICanvas : MonoBehaviour
    {
        [SerializeField] private Transform rootUIScreenTransform;
        [SerializeField] private Transform rootUIPopupTransform;
        [SerializeField] private Transform rootUIOverlayTransform;
        [SerializeField] private Transform rootUIClosedTransform;
        [SerializeField] private Camera    uiCamera;

        public Camera    UICamera              => this.uiCamera;
        public Transform RootUIScreenTransform => this.rootUIScreenTransform;
        
        public Transform RootUIClosedTransform  => this.rootUIClosedTransform;
        public Transform RootUIOverlayTransform => this.rootUIOverlayTransform;


        private void Awake()
        {
            this.rootUIScreenTransform ??= this.transform;

            this.rootUIClosedTransform ??= this.transform;

            this.rootUIOverlayTransform ??= this.transform;
        }
    }
}