using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfectedTeeth : MonoBehaviour {

    public List<GameObject> teeth;
    public Color infectionColor;
    public GameObject singleTooth;
    public Text mistakesText;
    public Material skullMat;

    private List<TransformParent> _teethPositions = new List<TransformParent>();

    public float initialInfectionDelay = 1.0f;
    public float nextInfectionDelay = 4.0f;
    //not used
    //private float _elapsedTime = 0.0f;
    //private bool _allTeethInfected = false;

    private int _totalHealth = 100;
    private int _totalMistakes = 3;

    public Slider healthBar;
    public Text healthText;

    void Start()
    {
        skullMat.color = Color.green;

        //go and store all of the initial positions/transforms of the teeth so that we can spawn new teeth at this location later
        foreach (GameObject go in teeth)
        {
            TransformParent temp;
            temp.transform = go.transform;
            temp.parentGO = go.transform.parent.gameObject;

            _teethPositions.Add(temp);
        }

        healthText.text = _totalHealth.ToString("000");
        healthBar.maxValue = _totalHealth;
        healthBar.value = _totalHealth;

        InvokeRepeating("InfectTooth", initialInfectionDelay, nextInfectionDelay);

        //wanted to respawn teeth but couldnt get it to work correctly
        //InvokeRepeating("SpawnTooth", 3.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
/*      replace with invokerepeating
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= nextInfectionDelay)
        {
            _elapsedTime = 0.0f;
            InfectTooth();
        }
*/
    }

    public void InfectTooth()
    {
        if (teeth.Count > 0)
        {
            for (int attemptedTooth = 0; attemptedTooth < teeth.Count; ++attemptedTooth)
            {
                int index = (int)(Random.Range(0, teeth.Count));
                GameObject selectedTooth = teeth[index];
                if (selectedTooth.GetComponent<Infection>() == null)
                {
                    Infection newInfection = selectedTooth.AddComponent<Infection>();
                    newInfection.infectedColor = infectionColor;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void RemoveTooth(GameObject toothToRemove)
    {
        if (teeth.Contains(toothToRemove))
        {
            int toothIndex = teeth.IndexOf(toothToRemove);
            teeth[toothIndex] = null;
        }
    }

    public void SpawnTooth()
    {
        for(int toothIndex = 0; toothIndex < teeth.Count; ++toothIndex)
        {
            if(teeth[toothIndex] == null)
            {
                //spawn a tooth here
                var toothToFill = _teethPositions[toothIndex];
                var newTooth = Instantiate(singleTooth, toothToFill.transform.position, Quaternion.identity) as GameObject;
                newTooth.transform.parent = toothToFill.parentGO.gameObject.transform;
            }
        }
    }

    public void TakeDamage()
    {
        //subtract one from the total health
        --_totalHealth;
        if(_totalHealth == 0)
        {
            SceneManager.LoadScene("Dentist");
        }

        healthBar.value = _totalHealth;
        healthText.text = _totalHealth.ToString("000");
        ChangeColor();
    }

    public void MakeMistake()
    {
        --_totalMistakes;

        if (_totalMistakes == 0)
        {
            SceneManager.LoadScene("Dentist");
        }
        skullMat.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        mistakesText.text = _totalMistakes.ToString("0");
        
    }

    void ChangeColor()
    {
        if(_totalHealth > 50 && _totalHealth < 70)
        {
            skullMat.color = Color.cyan;
        }
        else
        {
            if(_totalHealth > 0 && _totalHealth < 30)
            {
                skullMat.color = Color.yellow;
            }
        }
    }
}

struct TransformParent
{
    public Transform transform;
    public GameObject parentGO;
}
