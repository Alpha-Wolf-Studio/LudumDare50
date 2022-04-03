using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorControl : MonoBehaviour
{

    [Header("Cursors")]
    [SerializeField] private Texture2D aimCursor;
    [SerializeField] private Texture2D hookCursor;
    [SerializeField] private Texture2D repairCursor;
    [SerializeField] private LayerMask playerMask = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.SetCursor(hookCursor, new Vector2(hookCursor.width / 10, hookCursor.height / 10), CursorMode.Auto);
        }
        else
        {
            if (hit && Utils.CheckLayerInMask(playerMask, hit.collider.gameObject.layer))
            {
                if (hit.collider.GetComponent<ShipPlayer>().Sinking)
                {
                    Cursor.SetCursor(repairCursor, new Vector2(repairCursor.width / 2, repairCursor.height / 2), CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(aimCursor, new Vector2(aimCursor.width / 2, aimCursor.height / 2), CursorMode.Auto);
                }
            }
            else
            {
                Cursor.SetCursor(aimCursor, new Vector2(aimCursor.width / 2, aimCursor.height / 2), CursorMode.Auto);
            }
        }
    }
}
