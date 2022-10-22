using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;

        if (Input.GetMouseButton(0)) {
            interactMouse();
        }
    }

    public static Vector3 mousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position - new Vector3(0,0,100);
    }

    private void interactMouse() {
        RaycastHit2D telePos = Physics2D.Raycast(mousePosition(), Vector3.forward);
        MouseInteractable mouseInteractable = null;
        if (telePos.collider != null) mouseInteractable = telePos.collider.gameObject.GetComponent(typeof(MouseInteractable)) as MouseInteractable;

        if (mouseInteractable != null) mouseInteractable.interact();
        Debug.Log("hello");
    }
}

public interface MouseInteractable
{
    void interact();
}