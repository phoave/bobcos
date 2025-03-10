using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewClickSystem : MonoBehaviour
{
    private bool isHolding = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UICLICK.IsBusy)
        {
            isHolding = true;
            StartCoroutine(StartHolding());
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
        }
    }

    private IEnumerator StartHolding()
    {
        while (isHolding)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out TileScript tile))
                {
                    ClientSend.SendBlock((short)tile.siraid);
                }
            }

            yield return new WaitForSeconds(0.2f); // Adjust the hold duration as needed
        }
    }
}