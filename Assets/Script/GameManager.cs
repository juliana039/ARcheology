using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private List<ObjectInteractor> allObjects; // Arraste os 6 objetos aqui
    [SerializeField] private GameObject victoryCanvas; // Canvas de vitória
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CheckVictory()
{
    ObjectInteractor[] interactors = FindObjectsOfType<ObjectInteractor>();
    if (interactors == null || interactors.Length == 0) return;
    
    foreach (ObjectInteractor interactor in interactors)
    {
        if (interactor == null || interactor.GetObjectInfo() == null) continue;
        
        if (!interactor.IsCorrectlyPlaced())
        {
            Debug.Log($"{interactor.gameObject.name} ainda não está no lugar correto");
            return;
        }
    }
    
    Debug.Log("🎉 VITÓRIA! Todos os artefatos Chikenistas foram organizados corretamente!");
}
    
    private void Victory()
    {
        Debug.Log("🎉 VITÓRIA! Todos os objetos estão nos lugares corretos!");
        
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
        }
    }
}
