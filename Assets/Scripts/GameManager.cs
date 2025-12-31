using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public PlayableDirector[] actions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCue(int index) {
        Debug.Log("Cue "+index.ToString());
    }
    public void PlayAction(int index) {
        Debug.Log("Action "+index.ToString());
        actions[index].Play();
    }
}
