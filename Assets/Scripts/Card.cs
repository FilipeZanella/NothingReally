
[System.Serializable]
public class Card
{
    public int index;
    public int imageIndex;
    public bool paired;

    public Card(int _index, int image) 
    {
        index = _index;
        imageIndex = image;
    }
}
