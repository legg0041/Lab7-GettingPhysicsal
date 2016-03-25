using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteSugar : MonoBehaviour {

    private float _deleteTime = 2.0f;
    public Material skullMat;
    private Text mistakesText;
    public CapsuleCollider thisCollider;

    void Start()
    {
        //thisCollider.enabled = true;
        mistakesText = GameObject.Find("Mistakes Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        Destroy(gameObject, _deleteTime);
	}

    public void OnCollisionEnter(Collision collision)
    {
        //thisCollider.enabled = false;

        if (collision.gameObject.CompareTag("Bone"))
        {
            var numLeft = int.Parse(mistakesText.text);
            --numLeft;
            if(numLeft == 0)
            {
                SceneManager.LoadScene("Dentist");
            }
            else
            {
                skullMat.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                mistakesText.text = numLeft.ToString("0");
            }
        }
        Destroy(gameObject);
    }
}
