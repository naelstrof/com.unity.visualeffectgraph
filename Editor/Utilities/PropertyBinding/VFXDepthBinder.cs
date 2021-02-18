using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.VFX.Utility
{
    [AddComponentMenu("VFX/Property Binders/Depth Binder")]
    [VFXBinder("Depth/Depth")]
    class VFXDepthBinder : VFXBinderBase {
        public string Property { get { return (string)m_Property; } set { m_Property = value; UpdateSubProperties(); } }
        [VFXPropertyBinding("UnityEditor.VFX.Depth"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
        protected ExposedProperty m_Property = "DepthTexture";
        private ExposedProperty DepthBuffer;
        private Texture depthTexture;
        protected override void OnEnable() {
            base.OnEnable();
            UpdateSubProperties();
        }

        void OnValidate() {
            UpdateSubProperties();
        }

        void UpdateSubProperties() {
            DepthBuffer = m_Property;
        }

        public override bool IsValid(VisualEffect component) {
            return component.HasTexture((int)DepthBuffer);
        }

        public override void UpdateBinding(VisualEffect component) {
            if (depthTexture == null) {
                depthTexture = Shader.GetGlobalTexture("_CameraDepthTexture");
            }
            component.SetTexture((int)DepthBuffer, depthTexture);
        }

        public override string ToString() {
            return string.Format("Depth : '{0}' -> {1}", m_Property, depthTexture == null ? "(null)" : depthTexture.ToString());
        }
    }
}
