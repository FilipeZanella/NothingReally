using UnityEngine;
using UnityEngine.UI;

public class ResponsiveGrid : IGridResponsivenessHandler
{
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform container;
    public float cellAspectRatio = 1.0f; // Width / Height ratio of each cell
    public int itemCount = 12; // Total number of items (cards)

    public ResponsiveGrid (float aspect) 
    {
        cellAspectRatio = aspect;
    }

    public void UpdateGrid(GridLayoutGroup grid)
    {
        gridLayoutGroup = grid;
        container = grid.transform as RectTransform;
        itemCount = container.childCount;

        // Get the container dimensions
        float containerWidth = container.rect.width;
        float containerHeight = container.rect.height;

        // Calculate the best-fit rows and columns based on aspect ratio and item count
        Vector2Int gridDimensions = CalculateBestFitGridDimensions(containerWidth, containerHeight, cellAspectRatio, itemCount);

        // Calculate the optimal cell size based on the container size and grid dimensions
        Vector2 cellSize = CalculateCellSize(containerWidth, containerHeight, gridDimensions.x, gridDimensions.y, cellAspectRatio);

        // Apply the grid configuration
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridDimensions.x;
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = new Vector2(5, 5); // Optional: spacing adjustment
    }

    private Vector2Int CalculateBestFitGridDimensions(float containerWidth, float containerHeight, float aspectRatio, int itemCount)
    {
        int bestColumns = 1;
        int bestRows = itemCount;
        float bestFitError = float.MaxValue;

        // Iterate over possible column counts to find the best fit
        for (int columns = 1; columns <= itemCount; columns++)
        {
            int rows = Mathf.CeilToInt((float)itemCount / columns);
            float cellWidth = containerWidth / columns;
            float cellHeight = containerHeight / rows;

            // Adjust cell height to maintain the aspect ratio
            cellHeight = cellWidth / aspectRatio;

            // Calculate error if cells exceed the container's height
            float fitError = Mathf.Abs(containerHeight - (cellHeight * rows));

            // Check if this configuration fits better
            if (fitError < bestFitError)
            {
                bestFitError = fitError;
                bestColumns = columns;
                bestRows = rows;
            }
        }

        return new Vector2Int(bestColumns, bestRows);
    }

    private Vector2 CalculateCellSize(float containerWidth, float containerHeight, int columns, int rows, float aspectRatio)
    {
        // Calculate cell width based on columns and container width
        float cellWidth = containerWidth / columns;
        // Calculate cell height to maintain aspect ratio
        float cellHeight = cellWidth / aspectRatio;

        // Adjust cell height if it doesn't fit in the container height
        if (cellHeight * rows > containerHeight)
        {
            cellHeight = containerHeight / rows;
            cellWidth = cellHeight * aspectRatio;
        }

        return new Vector2(cellWidth, cellHeight);
    }
}
