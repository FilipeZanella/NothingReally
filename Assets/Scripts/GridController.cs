using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]

public class GridController : MonoBehaviour
{
    [SerializeField] private CardView prefab;

    private GridLayoutGroup grid;

    private void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
    }

    public void Initialize (Level level, Card[] cards, IGridResponsivenessHandler handler)
    {
        foreach (var card in cards) 
        {
            var cardInstance = Instantiate(prefab, transform);
            cardInstance.Initialize(card, level.GetSpriteByIndex(card.imageIndex));

            if (card.paired)
            {
                cardInstance.Display();
            }
        }
        
        handler.UpdateGrid(grid);   
    }
}
