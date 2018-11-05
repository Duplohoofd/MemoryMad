using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCard : MonoBehaviour {

 
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;

    public void OnMouseDown()
    {
        if (Card_Back.activeSelf && controller.canReveal)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }


///Kaarten met elkaar vergelijken en kijken of er een match is.
    
    private int _id;
    public int id
    {
        get { return _id; }

        
    }
///Zorgt ervoor dat de omgedraaide kaart Random is.
    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    ///Elke keer dat deze methode wordt aangeroepen laat die weer de achterkant van de kaart zien.
    public void Unreveal()
    {
        Card_Back.SetActive(true);
    }




}