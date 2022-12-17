using UnityEngine;

[CreateAssetMenu(fileName ="ItemData", menuName = "SO/NewItemData")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public Sprite sprite;
}

public enum ItemType
{
    BucketWithWater,
    Vegetables,
    FertilizerBag,
    Pitchfork,
    EmptyBucket,
    HayForLivestock
}
