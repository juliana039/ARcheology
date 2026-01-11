using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    public static HoldingManager Instance { get; private set; } //nao posso modificar só pegar
    private GameObject heldObject;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float holdDistance = 0.5f;
    [SerializeField] private float speed = 10f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (heldObject != null)
        {
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * holdDistance; 
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * speed);
        }  
    }

    //pick up

    public void PickUp(GameObject obj)
    {
        if (heldObject == null)
        {
            heldObject = obj;
            obj.transform.SetParent(null);

            var body = heldObject.GetComponent<Rigidbody>();
        
            if (body != null)
            {
                body.isKinematic = true; //desativa fisica
            }
        }
    }

    //drop
    public void Drop()
    {
        if (heldObject != null)
        {
            var body = heldObject.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.isKinematic = false; //ativa fisica
            }
            heldObject = null;
        }
    }

    public bool TryPickUp(GameObject obj)
    {
        if (heldObject == null)
        {
            PickUp(obj);
            return true;
        }
        return false;
    }
}
