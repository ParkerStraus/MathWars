using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour
{
    public Texture2D cursorIdle;
    public Texture2D cursorHover;
    public bool InGame;
    

    private void Start()
    {

        Cursor.SetCursor(cursorIdle,Vector2.zero,CursorMode.Auto);
    }

    private void Update()
    {
        if (InGame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                OnMouseEnter();
            }
            else
            {
                OnMouseExit();
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            OnMouseEnter();
        }

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHover, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.Auto);
    }


}
