using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    [SerializeField] private SpotController spot;
    [SerializeField] private float scanDuration = 3f;
    [SerializeField] GameObject scamUI;
    [SerializeField] private ParticleSystem scanEffect;
    [SerializeField] private AudioSource scanAudio;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            TryToPutOnSpot(collision.gameObject);
        }
    }

    public void HideUI()
    {
        scamUI.SetActive(false);
    }

    private void TryToPutOnSpot(GameObject obj)
    {
        if (!spot.IsOccupied())
        {
            obj.transform.SetParent(spot.transform);

            // Verifica se tem PreferredRotation (igual no CabinetController)
            if (obj.TryGetComponent<PreferredRotation>(out PreferredRotation prefRot))
            {
                obj.transform.localPosition = prefRot.GetScannerOffset();
                obj.transform.localRotation = Quaternion.Euler(prefRot.GetScannerRotation()); // ← mudança
            }
            else
            {
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            if (obj.TryGetComponent(out ObjectInteractor interactor))
            {
                StartCoroutine(StartScanning(interactor));
            }
        }
    }

    // ScannerController.cs
    private IEnumerator StartScanning(ObjectInteractor interactor)
    {
        Debug.Log($"Starting Scan de: {interactor.gameObject.name}");
        animator.SetBool("isScanning", true);
        scanEffect.Play();
        scanAudio.Play();
        scamUI.SetActive(false);
        interactor.SetLocked(true);
        interactor.SetScanned(false);

        yield return new WaitForSeconds(scanDuration);

        Debug.Log($"Scan Complete de: {interactor.gameObject.name}");
        animator.SetBool("isScanning", false);
        scanEffect.Stop();
        scanAudio.Stop();
        scamUI.SetActive(true);
        interactor.SetLocked(false);
        interactor.SetScanned(true);
        Debug.Log($"SetScanned(true) chamado para: {interactor.gameObject.name}");
    }

}
