using UnityEngine;
using UnityEngine.PostProcessing;

namespace UnityEditor.PostProcessing {
    // Specify explicitly which MinAttribute you want to use
    [CustomPropertyDrawer(typeof(UnityEngine.PostProcessing.MinAttribute))]  // Use this if it's PostProcessing
    // [CustomPropertyDrawer(typeof(UnityEngine.MinAttribute))]  // Uncomment this if it's the UnityEngine one
    sealed class MinDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Specify which MinAttribute to use below as well
            UnityEngine.PostProcessing.MinAttribute attribute = (UnityEngine.PostProcessing.MinAttribute)base.attribute;  // Use this if it's PostProcessing
            // UnityEngine.MinAttribute attribute = (UnityEngine.MinAttribute)base.attribute;  // Uncomment if it's the UnityEngine one

            if (property.propertyType == SerializedPropertyType.Integer) {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            } else if (property.propertyType == SerializedPropertyType.Float) {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            } else {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}