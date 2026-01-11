using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteractor : MonoBehaviour, IInteractable
{
    private bool isHeld = false;

    [SerializeField] private SOObjectInfo objectInfo;
    public void OnInteract()
    {
        Debug.Log("Interagindo com o cubo!");

        if(HoldingManager.Instance.TryPickUp(gameObject))
        {
             Debug.Log("Cubo pego!");
            isHeld = true;
            ShowObjectInfo();
        }
        else if(isHeld)
        {
            Debug.Log("Cubo solto!");
            HoldingManager.Instance.Drop();
            isHeld = false;
            HideObjectInfo();
        }
    }

    public void StopInteract()
    {
        Debug.Log("Parando interação com o cubo!");
    }

    void Update()
    {
        if( InputHandler.TryRayCastHit( out RaycastHit hitObject ) )
        {
            if (hitObject.transform == transform)
            {
                OnInteract();
            }
        }
    }

    private void ShowObjectInfo()
    {
        Debug.Log("Mostrando info do objeto");

        if(objectInfo == null) return;

        var infoController = FindObjectOfType<ObjectInfoController>();

        if(infoController != null)
        {
            infoController.SetObjectInfo(objectInfo);
            infoController.SetVisible(true);

            infoController.transform.SetParent(transform);
            infoController.transform.localPosition = new Vector3(-2 , 1.2f, 0);
        }
        
    }

    private void HideObjectInfo()
    {
        Debug.Log("Escondendo info do objeto");
        var infoController = FindObjectOfType<ObjectInfoController>();

        if(infoController != null)
        {
            infoController.SetVisible(false);
            infoController.transform.SetParent(null);
        }
        
    }

}
