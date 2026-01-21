using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject victoryPanel;

    void Awake()
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

    public void SetVictoryPanel(GameObject panel)
    {
        victoryPanel = panel;
    }

    public void CheckVictory()
    {
        ObjectInteractor[] interactors = FindObjectsOfType<ObjectInteractor>();

        if (interactors == null || interactors.Length == 0)
            return;

        foreach (ObjectInteractor interactor in interactors)
        {
            if (interactor == null || interactor.GetObjectInfo() == null)
                continue;

            if (!interactor.IsCorrectlyPlaced())
            {
                Debug.Log($"{interactor.gameObject.name} ainda não está no lugar correto");
                return;
            }
        }

        Debug.Log("🎉 VITÓRIA! Todos os artefatos Chikenistas foram organizados corretamente!");
        ShowVictory();
    }

    private void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Victory Panel NÃO foi atribuído ao GameManager!");
        }
    }

}
