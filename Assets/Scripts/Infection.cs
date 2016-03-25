using UnityEngine;
using System.Collections;


public class Infection : MonoBehaviour {

    public Color infectedColor;
    private Color _oldColor;

    public InfectedTeeth skull;
    public bool isHurting = false;

    //public float infectionStatus = 0.0f;

    public float _infectionStatus = 0.0f;

    private float _elapsedTime = 0.0f;
    private float _nextDamagePoint = 1.0f;

    private float _duration = 5.0f;
    private Material _rendererMaterial;

	// Use this for initialization
	void Start ()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        if(renderer != null)
        {
            _rendererMaterial = renderer.material;

            _oldColor = _rendererMaterial.color;
        }

        skull = GameObject.FindGameObjectWithTag("Skull").GetComponent<InfectedTeeth>();

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(_infectionStatus < 1.0f)
        {
            _infectionStatus += (Time.deltaTime / _duration);

            _rendererMaterial.color = Color.Lerp(_oldColor, infectedColor, _infectionStatus);
        }
        else
        {
            if (_infectionStatus > 1.0f)
            {
                isHurting = true;
                CauseDamage();
            }
        }
	}

    public void CauseDamage()
    {
        if(isHurting == true)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _nextDamagePoint)
            {
                _elapsedTime = 0.0f;
                skull.TakeDamage();
            }
        }
    }
}
