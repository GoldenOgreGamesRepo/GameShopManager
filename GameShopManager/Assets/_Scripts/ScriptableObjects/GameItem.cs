using UnityEngine;

[CreateAssetMenu(fileName = "NewGameItem", menuName = "Store/Game Item")]
public class GameItem : ScriptableObject
{
    public string itemName;
    public Sprite coverArt;
    public int purchaseCost;
    public int salePrice;
}
