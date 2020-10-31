using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    public float secondsToReprioritise;
    public float secondsSinceLastReprioritise;

    [SerializeField]
    private Stimulus target = null;
    public GameObject displayedIntention;
    [SerializeField]
    private List<Stimulus> stimuli = new List<Stimulus>();

    // Stop controller running, Knight is doing some uninteruptable action
    private bool interacting;

    // Stop controller running, use to test Knight pathfinding
    public bool pathfinding_mode;

    // Start is called before the first frame update
    void Start()
    {
        GlobalReferences.Knight = gameObject;
        interacting = false;
        pathfinding_mode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void FixedUpdate() 
    {
        secondsSinceLastReprioritise += Time.fixedDeltaTime;
        if(!pathfinding_mode){
            if(!interacting)
            {
                if (secondsSinceLastReprioritise >= secondsToReprioritise)
                {
                    target = Reprioritise();
                    
                    if(target.position != null)
                    {
                        MoveToPosition(target.position);
                        Debug.Log("Knight - Prioritising " + target.type);
                        if(!target.type.Equals("idle")){
                            secondsSinceLastReprioritise = 0;
                        }
                    }                        
                }
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
