using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMinigame : MonoBehaviour, ITriggerable
{
    public string[] questions;
    public string[] answers;
    private int _currentQuestion = 0;
    public string input;
    public TMP_Text Prompt;
    public GameObject button;
    public GameObject inputField;
    public Image background;
    public Image skull;
    
    public event Action Triggered;

    private void Start()
    {
        button.SetActive(true);
        inputField.SetActive(true);
        Prompt.text = questions[_currentQuestion];
    }

    public void CheckAnswer()
    {
        StopAllCoroutines();
        
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        if (input == answers[_currentQuestion])
        {
            _currentQuestion++;
            
            if (_currentQuestion == questions.Length)
            {
                StartCoroutine(OnGameComplete());
                yield break;
            }
                
            Prompt.text = "Gut gemacht! Jetzt kommt eine weitere Frage";
            yield return new WaitForSeconds(2f);
            Prompt.text = questions[_currentQuestion];
            yield break;
        }
        
        Prompt.text = "Falsch! Versuch noch mal";
        yield return new WaitForSeconds(2f);
        Prompt.text = questions[_currentQuestion];
    }

    private IEnumerator OnGameComplete()
    {
        button.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        skull.gameObject.SetActive(false);
        background.color = new Color(83 / 255f, 210 / 255f, 255 / 255f);
        Prompt.text = "Virus gelöscht!";
        WorldVariables.SetRepairStatus(50);
        GameObject.Find("SchoolRepairedNumber").GetComponent<TextMeshProUGUI>().text = WorldVariables.GetSchoolRepairStatus() + " %";
        OnTriggered();
        yield return new WaitForSeconds(2f);
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void UpdateInput(string s)
    {
        input = s;
    }

    public void OnTriggered()
    {
        Triggered?.Invoke();
    }
}
