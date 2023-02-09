using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PipeHolder;
    public GameObject[] Pipes;
    public GameObject Chest;
    public GameObject BonusItem;

    [SerializeField]
    int totalPipes = 0;

    private int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipeHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < totalPipes; i++) 
        {
            Pipes[i] = PipeHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        Debug.Log("correct move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");

            StartCoroutine(chestEvent());

        }
    }

    // triggers chest animation then spawns item upon completion of animation
    public IEnumerator chestEvent()
    {
        // open chest
        Chest.GetComponent<Animator>().Play("Open");
        yield return new WaitForSeconds(2.75f);

        // spawn item above chest
        Instantiate(BonusItem, new Vector3(Chest.transform.position.x, Chest.transform.position.y + .5f, Chest.transform.position.z), Chest.transform.rotation);
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }
}
