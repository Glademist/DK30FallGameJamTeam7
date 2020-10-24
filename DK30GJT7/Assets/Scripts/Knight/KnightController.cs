using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stimulus
{
    public GameObject gameobject;
    public Vector2 position;
    public string type;
    public float timeSeen;

    public Stimulus(GameObject stimulus_go, Vector2 stimulus_pos, string stimulus_type){
        gameobject = stimulus_go;
        position = stimulus_pos;
        type =  stimulus_type;
        timeSeen = Time.time;
    }
}

public class KnightController : MonoBehaviour
{
    public float secondsToReprioritise;
    public float secondsSinceLastReprioritise;
    public Stimulus target;
    public List<Stimulus> stimuli;
    public bool interacting;

    // Start is called before the first frame update
    void Start()
    {
        GlobalReferences.Knight = gameObject;
        stimuli = new List<Stimulus>();
        interacting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate() 
    {
        secondsSinceLastReprioritise += Time.fixedDeltaTime;
        if(interacting == false){
            if (secondsSinceLastReprioritise >= secondsToReprioritise){
                Debug.Log("Knight Reprioritising");
                target = Reprioritise();
                if(target.position != null){
                    MoveToPosition(target.position);
                }
                secondsSinceLastReprioritise = 0;
            }
        }
    }

    private Stimulus Reprioritise()
    {
        if(stimuli.Count != 0){
            Stimulus stim = stimuli[0];
            stimuli.RemoveAt(0);
            return stim;
        }
        else{
            Stimulus idle = new Stimulus(null, transform.position, "idle");
            return idle;
        }
    }

    public void AddKnightStimulus(GameObject gameobject, Vector2 position, string type)
    {
        Debug.Log("Adding knight stimulus " + type);
        Stimulus newStimulus = new Stimulus(gameobject, position, type);
        stimuli.Add(newStimulus);
    }


    private void MoveToPosition(Vector2 targetPosition)
    {
        Vector2 knightPos = gameObject.transform.position;
        if(Vector2.Distance(targetPosition, knightPos) >= 1){
            KnightMove pathfinder = gameObject.GetComponent<KnightMove>();
            pathfinder.CommandKnight(targetPosition);
        }
    }
}
