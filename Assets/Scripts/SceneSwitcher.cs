using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject body;
    public List<GameObject> fans;

    private Material bodyMaterial;
    private Material fanMaterial;

    public void SwitchScene(int index)
    {
        bodyMaterial = body.GetComponent<MeshRenderer>().material;
        fanMaterial = fans[0].GetComponent<MeshRenderer>().material;

        SceneManager.LoadScene(index);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var sceneObject = new GameObject("SceneObject");
        sceneObject.AddComponent<SceneObject>();
        sceneObject.GetComponent<SceneObject>().body = bodyMaterial;
        sceneObject.GetComponent<SceneObject>().fans = fanMaterial;
    }
}
