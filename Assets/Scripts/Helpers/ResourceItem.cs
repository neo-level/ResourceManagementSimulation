using UnityEngine;

namespace Helpers
{
    [CreateAssetMenu(fileName = "ResourceItem", menuName = "Tutorial/Resource Item")]
    public class ResourceItem : ScriptableObject
    {
        public new string name;
        public string id;
        public Sprite coneImage;
    }
}