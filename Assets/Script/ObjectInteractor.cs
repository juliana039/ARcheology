using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectInteractor : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private bool isLocked = false;
    private bool isScanned = false;

    [SerializeField] private SOObjectInfo objectInfo;
    [SerializeField] private float infoDisplayHeight = 1.0f;
    [SerializeField] private float infoDisplayX = 0f;
    [SerializeField] private float infoDisplayZ = 0f;


    public void OnInteract()
    {
        Debug.Log("Interagindo com o cubo!");
        if (isLocked) return;

        if (HoldingManager.Instance.TryPickUp(gameObject))
        {
            Debug.Log("Cubo pego!");
            isHeld = true;
            ShowObjectInfo();
        }
        else if (isHeld)
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
        if (InputHandler.TryRayCastHit(out RaycastHit hitObject))
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

        if (objectInfo == null || isScanned == false) return;

        var infoController = FindObjectOfType<ObjectInfoController>();

        if (infoController != null)
        {
            infoController.SetObjectInfo(objectInfo);
            infoController.SetVisible(true);

            // Move só o PANEL (onde está o ObjectInfoController)
            // Assumindo que ObjectInfoController está no Panel
            Transform panelTransform = infoController.transform;
            panelTransform.SetParent(transform);
            panelTransform.localRotation = Quaternion.identity;
            panelTransform.localScale = Vector3.one;
            panelTransform.localPosition = new Vector3(infoDisplayX, infoDisplayHeight, infoDisplayZ);
        }
    }

    private void HideObjectInfo()
    {
        Debug.Log("Escondendo info do objeto");
        var infoController = FindObjectOfType<ObjectInfoController>();

        if (infoController != null)
        {
            infoController.SetVisible(false);
            // Volta o Panel para o Canvas original
            infoController.transform.SetParent(GameObject.Find("Canvas").transform);
        }

    }
    public void SetLocked(bool locked)
    {
        isLocked = locked;

    }

    public void SetScanned(bool scanned)
    {
        isScanned = scanned;
    }
}
