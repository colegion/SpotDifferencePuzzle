using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public Sprite ItemSprite;
        public Vector2 ItemPosition;
        public Vector2 ItemSize;
        public Vector3 ItemRotation;
    }
}
