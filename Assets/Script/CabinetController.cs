using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    [SerializeField] private List<SpotController> spots; //lista de spots dentro do gabinete
    void OnCollisionEnter(Collision collision) //detecta quando um objeto colide com o gabinete
    {
        if(collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable)) //verifica se o objeto que colidiu tem a tag "Player"
        {
            //poder guardar no armário
            TryToPutOnCabinet(collision.gameObject);
        }
    }

    private void TryToPutOnCabinet(GameObject obj)
    {
        //verficar se o objeto pode ser guardado no armário
        if(GetAvailableSpot() is SpotController availableSpot)
        {
            obj.transform.SetParent(availableSpot.transform); //coloca o objeto como filho do spot disponível
            obj.transform.localPosition = Vector3.zero; //posiciona o objeto na posição local do spot
            obj.transform.localRotation = Quaternion.identity; //reseta a rotação do objeto

            if(obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true; //desativa a física do objeto para estar numa posicao melhor
            }  
        }

    }

    private SpotController GetAvailableSpot()
    {
        foreach(SpotController spot in spots)
        {
            if(!spot.IsOccupied()) //se o spot não estiver ocupado
            {
                return spot;
            }
        }
        return null; //nenhum spot disponível
    }
}
