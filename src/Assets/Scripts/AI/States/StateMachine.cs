/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    //property for patrol
    public void Initialized()
    {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;

        //fail-safe null
        if(activeState != null)
        {
            //new state
            activeState.stateMachine = this;
            //assign state enemy
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
*/