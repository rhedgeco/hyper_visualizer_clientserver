using UnityEngine;

namespace HyperEngine
{
    public class PropertyManager
    {
        private static Transform content;

        internal static void SetUpPropertyManager(Transform contentArea)
        {
            content = contentArea;
        }

        // Method is to be overridden by child
        public static GameObject AddInspectorProperty(RectTransform property)
        {
            return Object.Instantiate(property.gameObject, content);
        }
    }
}