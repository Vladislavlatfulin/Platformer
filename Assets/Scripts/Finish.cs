using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelCanvas;
    [SerializeField] private GameObject message;
    private bool _isActived = false;

    public void FinishLevel()
    {
        if (_isActived)
        {
            //gameObject.SetActive(false);
            nextLevelCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            message.SetActive(true);
        }
    }

    public void Activate()
    {
        _isActived = true;
        message.SetActive(false);
    }
}
