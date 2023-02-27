using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public SpriteRenderer image;

    private void Awake()
    {
        image.sprite = null;
        image.enabled = false;
    }
    public void DrawShape(Sprite spriteToDraw)
    {
        if (!image.enabled)
            image.enabled = true;

        image.sprite = spriteToDraw;
    }
    public void Toggle()
    {
        if (image != null)
        {
            image.enabled = !image.enabled;
        }
    }
}
