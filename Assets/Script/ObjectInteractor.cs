using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private bool isLocked = false;
    private bool isScanned = false;
    private bool isCorrectlyPlaced = false; 
    
    [SerializeField] private SOObjectInfo objectInfo;
    [SerializeField] private float infoDisplayHeight = 1.0f;
    [SerializeField] private float infoDisplayX = 0f;
    [SerializeField] private float infoDisplayZ = 0f;

    public void OnInteract()
    {
        Debug.Log($"OnInteract - isScanned: {isScanned}, objectInfo null: {objectInfo == null}");
        
        if (isLocked) return;
        
        if (HoldingManager.Instance.TryPickUp(gameObject))
        {
            Debug.Log("Cubo pego!");
            isHeld = true;
            
            var scanner = FindObjectOfType<ScannerController>();
            if (scanner != null)
            {
                scanner.HideUI();
            }
            
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
        if (objectInfo == null || isScanned == false) return;
        
        var infoController = FindObjectOfType<ObjectInfoController>();
        
        if (infoController != null)
        {
            infoController.SetObjectInfo(objectInfo);
            infoController.SetVisible(true);
            
            Transform panelTransform = infoController.transform;
            panelTransform.SetParent(null);
            panelTransform.localScale = Vector3.one * 0.15f;
            
            StartCoroutine(FollowObject(panelTransform));
        }
    }

    private IEnumerator FollowObject(Transform panel)
    {
        while (panel != null && isHeld)
        {
            Vector3 worldOffset = new Vector3(infoDisplayX, infoDisplayHeight, infoDisplayZ);
            panel.position = transform.position + worldOffset;
            yield return null;
        }
    }

    private void HideObjectInfo()
    {
        Debug.Log("Escondendo info do objeto");
        var infoController = FindObjectOfType<ObjectInfoController>();
        if (infoController != null)
        {
            infoController.SetVisible(false);
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
    
    // NOVOS MÉTODOS
    public SOObjectInfo GetObjectInfo()
    {
        return objectInfo;
    }
    
    public void SetCorrectPlacement(bool correct)
    {
        isCorrectlyPlaced = correct;
    }
    
    public bool IsCorrectlyPlaced()
    {
        return isCorrectlyPlaced;
    }
}