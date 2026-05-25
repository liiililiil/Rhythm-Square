using AudioManagement;
using Tables.MusicTable;
using UnityEngine;

public class MusicSelectGeneric : MusicSelectableObjectBase
{
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public override void PositionUpdate(Vector2 position)
    {
        rectTransform.anchoredPosition = new Vector2(position.x, position.y);
    }

}
