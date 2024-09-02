using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D texture2D;
    private void Start()
    {
        Cursor.SetCursor(texture2D, new Vector2(0f,0f), CursorMode.Auto);
    }
}
