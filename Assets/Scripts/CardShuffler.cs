using System.Collections.Generic;
using UnityEngine;

public interface ICardShuffler 
{
    Card[] GenerateCardDeck(Sprite[] _image);
}

public class CardShuffler_FisherYates : ICardShuffler
{
    public Card[] GenerateCardDeck(Sprite[] images)
    {
        List<Card> cardList = new List<Card>();

        for (int i = 0; i < images.Length; i++)
        {
            cardList.Add(new Card(cardList.Count, i)); // First card of the pair
            cardList.Add(new Card(cardList.Count, i)); // Second card of the pair
        }

        Shuffle(cardList);

        return cardList.ToArray();
    }

    private void Shuffle(List<Card> cardList)
    {
        // Fisher-Yates shuffle algorithm
        for (int i = cardList.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Card temp = cardList[i];
            cardList[i] = cardList[j];
            cardList[j] = temp;
        }
    }
}
