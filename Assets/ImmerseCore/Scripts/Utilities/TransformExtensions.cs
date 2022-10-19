using UnityEngine;

namespace Immerse.Core
{
    public static class TransformExtensions
    {
        public static void ResizeToTexture(this RectTransform rectTransform, Texture texture)
        {
            float x = rectTransform.sizeDelta.x;
            rectTransform.sizeDelta = new Vector2(x, x * texture.height / texture.width);
        }
    }
}