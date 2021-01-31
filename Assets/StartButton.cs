using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel1;
    public GameObject panel2;
    public void OnPointerClick(PointerEventData eventData)
    {
        panel2.SetActive(true);
        panel1.SetActive(false);
    }
}