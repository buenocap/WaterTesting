/*MapBorderGenerator.cs 
Pedro Bueno
This script will generate map borders at the start of the game, the size and positioning is dependent on the size of the map which
is determined on the difficulty selected. The purpose of this script is to prevent the user from falling off of the map.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorderGenerator : MonoBehaviour
{
    public Material borderMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if(DifficultyMenu.levelDifficulty == 0) 
        {
            Debug.Log("Border for easy mode activated!");
            GameObject leftPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject rightPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject topPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject bottomPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            //Left plane
            leftPlane.transform.position = new Vector3(0f,2.5f,25f);
            leftPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            leftPlane.transform.localScale = new Vector3(0.5f,1f,5f);

            //Right plane
            rightPlane.transform.position = new Vector3(50f,2.5f,25f);
            rightPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            rightPlane.transform.localScale = new Vector3(0.5f,-1f,-5f);

            //Top plane
            topPlane.transform.position = new Vector3(25f,2.5f,50f);
            topPlane.transform.eulerAngles = new Vector3(0f,90f,-90f);
            topPlane.transform.localScale = new Vector3(0.5f,1f,5f);

            //Bottom plane
            bottomPlane.transform.position = new Vector3(25f,2.5f,0f);
            bottomPlane.transform.eulerAngles = new Vector3(0f,-90f,-90f);
            bottomPlane.transform.localScale = new Vector3(0.5f,1f,5f);

            //Changing the current seeable material of the plane to a transparent border
            leftPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            rightPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            topPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            bottomPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
        }
        else if(DifficultyMenu.levelDifficulty == 1) 
        {
            Debug.Log("Border for normal mode activated!");
            GameObject leftPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject rightPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject topPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject bottomPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            //Left plane
            leftPlane.transform.position = new Vector3(0f,2.5f,75f);
            leftPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            leftPlane.transform.localScale = new Vector3(0.5f,1f,15f);

            //Right plane
            rightPlane.transform.position = new Vector3(150f,2.5f,75f);
            rightPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            rightPlane.transform.localScale = new Vector3(0.5f,-1f,-15f);

            //Top plane
            topPlane.transform.position = new Vector3(75f,2.5f,150f);
            topPlane.transform.eulerAngles = new Vector3(0f,90f,-90f);
            topPlane.transform.localScale = new Vector3(0.5f,1f,15f);
            
            //Bottom plane
            bottomPlane.transform.position = new Vector3(75f,2.5f,0f);
            bottomPlane.transform.eulerAngles = new Vector3(0f,-90f,-90f);
            bottomPlane.transform.localScale = new Vector3(0.5f,1f,15f);

            //Changing the current seeable material of the plane to a transparent border
            leftPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            rightPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            topPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            bottomPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
        }
        else if(DifficultyMenu.levelDifficulty == 2) 
        {
            Debug.Log("Border for hard mode activated!");
            GameObject leftPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject rightPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject topPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject bottomPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            //leftPlane positioning
            leftPlane.transform.position = new Vector3(0f,2.5f,150f);
            leftPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            leftPlane.transform.localScale = new Vector3(0.5f,1f,30f);

            //rightPlane positioning
            rightPlane.transform.position = new Vector3(300f,2.5f,150f);
            rightPlane.transform.eulerAngles = new Vector3(0f,0f,-90f);
            rightPlane.transform.localScale = new Vector3(0.5f,-1f,-30f);

            //topPlane positioning
            topPlane.transform.position = new Vector3(150f,2.5f,300f);
            topPlane.transform.eulerAngles = new Vector3(0f,90f,-90f);
            topPlane.transform.localScale = new Vector3(0.5f,1f,30f);

            //bottomPlane positioning
            bottomPlane.transform.position = new Vector3(150f,2.5f,0f);
            bottomPlane.transform.eulerAngles = new Vector3(0f,-90f,-90f);
            bottomPlane.transform.localScale = new Vector3(0.5f,1f,30f);

            //Changing the current seeable material of the plane to a transparent border
            leftPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            rightPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            topPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
            bottomPlane.GetComponent<MeshRenderer> ().material = borderMaterial;
        }
        else {
            Debug.Log("Error: Level Difficulty could not be detected!");
        }
    }
}
