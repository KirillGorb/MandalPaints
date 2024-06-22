using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "ConfigResponse", menuName = "Config/ConfigResponse", order = 1)]
    public class ConfigResponse : ScriptableObject
    {
        public float TimeCheck;
        [SerializeField] private Color isStandardPlane;
        [SerializeField] private Color isStandardText;

        [SerializeField] private Color isRightPlane;
        [SerializeField] private Color isFalsePlane;

        [SerializeField] private Color isRightText;
        [SerializeField] private Color isFalseText;

        public Color IsCheckPlane(bool isRightValue) => isRightValue ? isRightPlane : isFalsePlane;
        public Color IsCheckText(bool isRightValue) => isRightValue ? isRightText : isFalseText;
        public (Color, Color) GetCheckPlaneTextColor() => (isStandardPlane, isStandardText);
    }
}