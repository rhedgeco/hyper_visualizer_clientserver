//using HyperEngine;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace DefaultVisualizer.data
//{
//    public class TextController : MonoBehaviour
//    {
//        private Text text;
//        [SerializeField] private GameObject textProperty;
//
//        private void Start()
//        {
//            text = GetComponent<Text>();
//            InputField field = PropertyManager.AddInspectorProperty(textProperty.GetComponent<RectTransform>())
//                .GetComponentInChildren<InputField>();
//            field.onEndEdit.AddListener(ChangeText);
//        }
//
//        private void ChangeText(string title)
//        {
//            text.text = title;
//        }
//    }
//}