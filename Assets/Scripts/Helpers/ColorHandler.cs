using UnityEngine;

// Handles setting a color to a given renderer and material slot. Used to simplify coloring our Unit.
// (This can be added on the visual prefab and the Unit code can just query oif that component exists to set color)
namespace Helpers
{
    public class ColorHandler : MonoBehaviour
    {
        public Renderer tintRenderer;
        public int tintMaterialSlot;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        public void SetColor(Color c)
        {
            var prop = new MaterialPropertyBlock();
            prop.SetColor(BaseColor, c);
            tintRenderer.SetPropertyBlock(prop, tintMaterialSlot);
        }
    }
}