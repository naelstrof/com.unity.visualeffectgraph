﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

namespace UnityEngine.VFX.Utility {
    /// <summary>
    /// Camera parameter binding helper class.
    /// </summary>
    [VFXBinder("URP/URP Camera")]
    public class URPCameraBinder : VFXBinderBase {
        /// <summary>
        /// Camera HDRP additional data.
        /// </summary>
        public Camera cam;
        public RenderTexture DepthTexture;

        [VFXPropertyBinding("UnityEditor.VFX.CameraType"), SerializeField]
        ExposedProperty CameraProperty = "Camera";

        ExposedProperty m_Position;
        ExposedProperty m_Angles;
        ExposedProperty m_Scale;
        ExposedProperty m_FieldOfView;
        ExposedProperty m_NearPlane;
        ExposedProperty m_FarPlane;
        ExposedProperty m_AspectRatio;
        ExposedProperty m_Dimensions;
        ExposedProperty m_DepthBuffer;
        ExposedProperty m_ColorBuffer;

        /// <summary>
        /// Set a camera property.
        /// </summary>
        /// <param name="name">Property name.</param>
        public void SetCameraProperty(string name) {
            CameraProperty = name;
            UpdateSubProperties();
        }

        void UpdateSubProperties() {
            // Update VFX Sub Properties
            m_Position = CameraProperty + "_transform_position";
            m_Angles = CameraProperty + "_transform_angles";
            m_Scale = CameraProperty + "_transform_scale";
            m_FieldOfView = CameraProperty + "_fieldOfView";
            m_NearPlane = CameraProperty + "_nearPlane";
            m_FarPlane = CameraProperty + "_farPlane";
            m_AspectRatio = CameraProperty + "_aspectRatio";
            m_Dimensions = CameraProperty + "_pixelDimensions";
            m_DepthBuffer = CameraProperty + "_depthBuffer";
            m_ColorBuffer = CameraProperty + "_colorBuffer";
        }

        /// <summary>
        /// OnEnable implementation.
        /// </summary>
        protected override void OnEnable() {
            base.OnEnable();
            UpdateSubProperties();
        }

        /// <summary>
        /// OnDisable implementation.
        /// </summary>
        protected override void OnDisable() {
            base.OnDisable();
        }

        private void OnValidate() {
            UpdateSubProperties();
        }

        /// <summary>
        /// Returns true if the Visual Effect and the configuration of the binder are valid to perform the binding.
        /// </summary>
        /// <param name="component">Component to be tested.</param>
        /// <returns>True if the Visual Effect and the configuration of the binder are valid to perform the binding.</returns>
        public override bool IsValid(VisualEffect component) {
            return DepthTexture != null
                && cam != null
                && component.HasVector3(m_Position)
                && component.HasVector3(m_Angles)
                && component.HasVector3(m_Scale)
                && component.HasFloat(m_FieldOfView)
                && component.HasFloat(m_NearPlane)
                && component.HasFloat(m_FarPlane)
                && component.HasFloat(m_AspectRatio)
                && component.HasVector2(m_Dimensions)
                && component.HasTexture(m_DepthBuffer)
                && component.HasTexture(m_ColorBuffer);
        }

        /// <summary>
        /// Update bindings for a visual effect.
        /// </summary>
        /// <param name="component">Component to update.</param>
        public override void UpdateBinding(VisualEffect component) {
            component.SetVector3(m_Position, cam.transform.position);
            component.SetVector3(m_Angles, cam.transform.eulerAngles);
            component.SetVector3(m_Scale, cam.transform.lossyScale);

            // While field of View is set in degrees for the camera, it is expected in radians in VFX
            component.SetFloat(m_FieldOfView, Mathf.Deg2Rad * cam.fieldOfView);
            component.SetFloat(m_NearPlane, cam.nearClipPlane);
            component.SetFloat(m_FarPlane, cam.farClipPlane);

            component.SetFloat(m_AspectRatio, cam.aspect);
            component.SetVector2(m_Dimensions, new Vector2(cam.pixelWidth, cam.pixelHeight));

            component.SetTexture(m_DepthBuffer, DepthTexture);
        }

        /// <summary>
        /// To string implementation.
        /// </summary>
        /// <returns>String containing the binder information.</returns>
        public override string ToString() {
            return string.Format($"URP Camera : '{(cam == null ? "null" : cam.gameObject.name)}' -> {CameraProperty}");
        }
    }

}