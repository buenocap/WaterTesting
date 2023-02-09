using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = {0, 90, 180, 270};

    public float[] correctRotations;
    [SerializeField]
    bool isPlaced = false;

    int possibleRotations = 1;

    public GameObject manager;
    private PuzzleManager puzzleManager;

    private void Awake()
    {
        puzzleManager = manager.GetComponent<PuzzleManager>();
        
        
    }

    private void Start()
    {
        possibleRotations = correctRotations.Length;

        int rand = Random.Range(0, rotations.Length);
        transform.localEulerAngles = new Vector3(0, 0, rotations[rand]);

        if (possibleRotations > 1) 
        {
            if (transform.localEulerAngles.z == correctRotations[0] || transform.localEulerAngles.z == correctRotations[1])
            {
                isPlaced = true;
                puzzleManager.GetComponent<PuzzleManager>().correctMove();
            }
        }
        else
        {
            if (transform.localEulerAngles.z == correctRotations[0])
            {
                isPlaced = true;
                puzzleManager.GetComponent<PuzzleManager>().correctMove();
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        // round tranformed angle to nearest integer to account for floating point inaccuracies
        transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x), Mathf.Round(transform.localEulerAngles.y), Mathf.Round(transform.localEulerAngles.z));
        
        Debug.Log(transform.localEulerAngles.z);

        if (possibleRotations > 1)
        {
            if ((transform.localEulerAngles.z == correctRotations[0] || transform.localEulerAngles.z == correctRotations[1]) && isPlaced == false)
            {
                Debug.Log(transform.localEulerAngles.z);
                isPlaced = true;
                puzzleManager.GetComponent<PuzzleManager>().correctMove();
            }
            else if (isPlaced == true)
            {
                Debug.Log(transform.localEulerAngles.z);
                isPlaced = false;
                puzzleManager.GetComponent<PuzzleManager>().wrongMove();
            }
        }
        else
        {
            if (transform.localEulerAngles.z == correctRotations[0])
            {
                Debug.Log(transform.localEulerAngles.z);
                isPlaced = true;
                puzzleManager.GetComponent<PuzzleManager>().correctMove();
            }
            else if (isPlaced == true)
            {
                Debug.Log(transform.localEulerAngles.z);
                isPlaced = false;
                puzzleManager.GetComponent<PuzzleManager>().wrongMove();
            }
        }
        
    }

}
