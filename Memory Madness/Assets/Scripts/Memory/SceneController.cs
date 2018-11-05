using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    ///Hoe worden de kaarten gegenereerd op het scherm. Deze code beschrijft 2 rijen en 4 kollomen (kaarten).
    ///De offset is de ruimte tussen elke kaart.
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private void Start()
    {

            
        Vector3 startPos = originalCard.transform.position; ///Bekijkt de positie van de eerste kaart en plaats de overige kaarten op basis van deze positie.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        ///int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7 };
        numbers = ShuffleArray(numbers); /// functie die we zo gaan maken!

        for(int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if( i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            
            }
        }
    }
    ///Schud de array (lijst met kaarten) en 'return' in een random volgorde
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
            
        }
        return newArray;
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------
    //Elke kaart wordt gecheckt wanneer deze onthuld wordt. Ook kunnen er maar 2 kaarten tegelijkertijd onthuld zijn.
    //Checkmatch is een Coroutine die gebruikt wordt om te bekijken of de geselecteerde kaarten hetzelfde zijn.

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
            

        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            ///met unreveal worden de geselecteerde kaarten weer omgedraait.
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //kaarten worden terug gezet naar null zodat ze weer gebruikt kunnen worden.
        _firstRevealed = null;
        _secondRevealed = null;

        if(_score == 4)
        {
            SceneManager.LoadScene("LevelGehaald");
        }


    }






    public void Restart()
    {
        SceneManager.LoadScene("Scene_001");
    }

}
