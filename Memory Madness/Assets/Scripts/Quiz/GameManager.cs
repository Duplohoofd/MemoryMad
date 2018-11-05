using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour {
    //Maak een array met vragen die gemaakt worden in de unity inspector.
    public Question[] vragen;
    private static List<Question> onbeantwoordevragen;

    private Question huidigevraag;

    [SerializeField]
    private Text feitText;

    [SerializeField]
    private Text GoedeAntwoordText;

    [SerializeField]
    private Text FouteAntwoordText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float tijdTussenVragen = 1f;

    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;

    void Start ()
    {
        if (onbeantwoordevragen == null || onbeantwoordevragen.Count == 0)
        {
            onbeantwoordevragen = vragen.ToList<Question>();
        }

        ZetHuidigeVraag();

    }

    void ZetHuidigeVraag ()
    {
        int RandomVraagIndex = Random.Range(0, onbeantwoordevragen.Count);
        huidigevraag = onbeantwoordevragen[RandomVraagIndex];

        feitText.text = huidigevraag.feit;

        if (huidigevraag.isGoed)
        {
            GoedeAntwoordText.text = "CORRECT!";
            FouteAntwoordText.text = "FOUT!";
            _score++;
            scoreLabel.text = "Score: " + _score;
        } else
        {
            GoedeAntwoordText.text = "FOUT!";
            FouteAntwoordText.text = "CORRECT";
        }
    }

    IEnumerator gaNaarVolgendeVraag ()
    {
        onbeantwoordevragen.Remove(huidigevraag);

        yield return new WaitForSeconds(tijdTussenVragen);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }

    public void UserSelectTrue ()
    {
        animator.SetTrigger("Goed");
        if (huidigevraag.isGoed)
        {
            Debug.Log("CORRECT");
        } else
        {
            Debug.Log("FOUT");
        }

        StartCoroutine(gaNaarVolgendeVraag());

    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("Fout");
        if (!huidigevraag.isGoed)
        {
            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("FOUT");
        }

        StartCoroutine(gaNaarVolgendeVraag());
    }
}
