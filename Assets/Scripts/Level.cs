using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "MemoryGame/Level", order = 1)]
public class Level : ScriptableObject 
{
    [SerializeField] private Sprite[] cards;

    public Sprite GetSpriteByIndex(int index) 
    {
        return cards[index];
    }

    public Card[] GetShuffledCards (ICardShuffler shuffler) 
    {
        return shuffler.GenerateCardDeck(cards);
    }
}
