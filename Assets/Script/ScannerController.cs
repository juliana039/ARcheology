using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    [SerializeField] private SpotController spot;
    [SerializeField] private float scanDuration = 3f;
    [SerializeField] GameObject scamUI;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            TryToPutOnSpot(collision.gameObject);
        }
    }

    private void TryToPutOnSpot(GameObject obj)
    {
        if(!spot.IsOccupied())
        {
            obj.transform.SetParent(spot.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            if(obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            if (obj.TryGetComponent(out ObjectInteractor interactor))
            {
                StartCoroutine(StartScanning(interactor));
            }

            
        }
    }

    private IEnumerator StartScanning(ObjectInteractor interactor)
    {
        Debug.Log("Starting Scan...");

        animator.SetBool("isScanning", true);

        scamUI.SetActive(false);

        interactor.SetLocked(true);
        interactor.SetScanned(false);

        yield return new WaitForSeconds(scanDuration);    
        Debug.Log("Scan Complete!");    

        animator.SetBool("isScanning", false);

        scamUI.SetActive(true);
        interactor.SetLocked(false);
        interactor.SetScanned(true);
    }

}
