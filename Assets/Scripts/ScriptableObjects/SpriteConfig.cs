using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteConfig", menuName = "ScriptableObjects/SpriteConfig", order = 2)]
public class SpriteConfig : ScriptableObject
{
    [SerializeField] SlotModel[] sprites;

    public Sprite GetSprite(SlotsIDs id)
    {
        return sprites.FirstOrDefault((s) => s.slotID == id).icon;
    }
}