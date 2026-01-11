using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] private float requiredArea; //área requerida 
    [SerializeField] private ARPlaneManager planeManager; //controlador de planos
    [SerializeField] private GameObject startExperienceUI; //UI para iniciar a experiência
    [SerializeField] private StartExperience startExperience; //script para iniciar a experiência

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesUpdated;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesUpdated;
    }

    public void OnClickStartExperience()
    {
        Debug.Log("Iniciando a experiência AR");
        startExperienceUI.SetActive(false); //desativa pop up
        //desativa todos os planos
        planeManager.enabled = false; //nao escaneia mais o cenario
        
        // foreach (var plane in planeManager.trackables) //desativa todos os planos encontrados
        // {
        //     plane.gameObject.SetActive(false);
        // }

        startExperience.OnStartExperience(GetBiggestPlane()); 
    }

    private void OnPlanesUpdated(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.updated)
        {
            if (plane.extents.x * plane.extents.y >= requiredArea) //se a area for maior ou igual a area requerida
            {
                //encontrou plano
                startExperienceUI.SetActive(true);
            }
        }
    }
    private ARPlane GetBiggestPlane()
    {
        ARPlane biggestPlane = null;
        float biggestArea = 0f;

        foreach (var plane in planeManager.trackables)
        {
            float area = plane.extents.x * plane.extents.y;
            if (area > biggestArea)
            {
                biggestArea = area;
                biggestPlane = plane;
            }
        }
        return biggestPlane;
    }

}

