using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CabinetController : MonoBehaviour
{
    [SerializeField] private List<SpotController> spots;
    [SerializeField] private CabinetType cabinetType;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            TryToPutOnCabinet(collision.gameObject);
        }
    }

    private void TryToPutOnCabinet(GameObject obj)
{
    if (GetAvailableSpot() is SpotController availableSpot)
    {
        obj.transform.SetParent(availableSpot.transform);
        
        if (obj.TryGetComponent<PreferredRotation>(out PreferredRotation prefRot))
        {
            Vector3 rot = prefRot.GetCabinetRotation();
            Vector3 pos = prefRot.GetCabinetOffset();
            
            Debug.Log($"[CABINET] {obj.name} - Rotation: {rot}, Position: {pos}");
            
            obj.transform.localPosition = pos;
            obj.transform.localRotation = Quaternion.Euler(rot);
            
            Debug.Log($"[CABINET] {obj.name} - Applied localRotation: {obj.transform.localRotation.eulerAngles}");
        }
        else
        {
            Debug.Log($"[CABINET] {obj.name} - SEM PreferredRotation, usando padrão");
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }

        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        if (obj.TryGetComponent<ObjectInteractor>(out ObjectInteractor interactor))
        {
            CheckIfCorrect(interactor);
        }
    }
}

    private void CheckIfCorrect(ObjectInteractor interactor)
{
    SOObjectInfo info = interactor.GetObjectInfo();
    
    Debug.Log($"[DEBUG] Objeto: {info.objectName}");
    Debug.Log($"[DEBUG] Correct Cabinet no SO: {info.correctCabinet}");
    Debug.Log($"[DEBUG] Cabinet Type atual: {cabinetType}");
    
    if (info != null && info.correctCabinet == cabinetType)
    {
        Debug.Log($"✓ CORRETO! {info.objectName} no cabinet certo!");
        interactor.SetCorrectPlacement(true);
    }
    else
    {
        Debug.Log($"✗ ERRADO! {info.objectName} no cabinet errado!");
        interactor.SetCorrectPlacement(false);
    }

    if (GameManager.Instance != null)
    {
        GameManager.Instance.CheckVictory();
    }
}

    private SpotController GetAvailableSpot()
    {
        foreach (SpotController spot in spots)
        {
            if (!spot.IsOccupied())
            {
                return spot;
            }
        }
        return null;
    }
}