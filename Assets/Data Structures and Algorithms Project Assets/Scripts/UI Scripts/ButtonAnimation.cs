using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler      
{
    [SerializeField] private GameObject doorOpenSprite;
    [SerializeField] private GameObject doorClosedSprite;
    [SerializeField] private GameObject playButton;

    private void Awake()
    {
        doorOpenSprite.SetActive(false) ;
        doorClosedSprite.SetActive(true);
        playButton.SetActive(false);   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        doorOpenSprite.SetActive(true);
        doorClosedSprite.SetActive(false);
        playButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        doorClosedSprite.SetActive(true);
        playButton.SetActive(false);
        doorOpenSprite.SetActive(false);

    }
}
