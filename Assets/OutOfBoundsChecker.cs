using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OutOfBoundsChecker : MonoBehaviour
{
    [SerializeField] private GameObject OutOfBoundsWarning;
    [SerializeField] private TMP_Text TxtOutOfBoundsCountDownTimer;
    [SerializeField] private float OutOfBoundsTime = 5.0f;

    private float OutOfBoundsCountDownTimer = 0.0f;

    private bool PlayerOutOfBounds = false;

    private Queue<Vector3> PlayerPositions = new Queue<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        OutOfBoundsCountDownTimer = OutOfBoundsTime;
    }

    // Update is called once per frame
    void Update()
    {       
        if (PlayerOutOfBounds)
        {
            TxtOutOfBoundsCountDownTimer.SetText(OutOfBoundsCountDownTimer.ToString("F1"));
            OutOfBoundsCountDownTimer -= Time.deltaTime;

            if (OutOfBoundsCountDownTimer <= 0)
                SceneManager.LoadScene(2);
        }        
    }

    private void DisableWarning()
    {
        OutOfBoundsCountDownTimer = OutOfBoundsTime;
        OutOfBoundsWarning.SetActive(false);
    }

    private void EnableWarning()
    {
        PlayerOutOfBounds = true;
        OutOfBoundsWarning.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CancelInvoke("DisableWarning");
            Invoke("EnableWarning", 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerOutOfBounds = false;
            CancelInvoke("EnableWarning");
            Invoke("DisableWarning", 1.5f);
        }
    }
}
