using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class FlexibleGridLayout : LayoutGroup
{
    public enum Type
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns,
        FixedChildSize
    }

    public Type type;
    public int rows;
    public int columns;
    public bool fixedRows;
    public bool fixedColumns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (type == Type.Uniform || type == Type.Height || type == Type.Width)
        {
            fixedRows = true;
            fixedColumns = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        if (type == Type.Width || type == Type.FixedColumns)
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        else if (type == Type.Height || type == Type.FixedRows)
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        else
        {
            rows = Mathf.CeilToInt(parentHeight / (cellSize.x + (spacing.x * 2)));
            columns = Mathf.CeilToInt(parentWidth / (cellSize.y + (spacing.y * 2)));
        }

        if(type != Type.FixedChildSize)
        {
            float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
            float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            cellSize.x = fixedRows ? cellWidth : cellSize.x;
            cellSize.y = fixedColumns ? cellHeight : cellSize.y;
        }

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
