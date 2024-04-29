/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int indexOfWaypoints;
    public float waitTimer;
    public override void Enter()
    {

    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        //patrol logic
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if (indexOfWaypoints < enemy.pathing.waypoints.Count - 1)
                {
                    indexOfWaypoints++;
                }
                else
                    indexOfWaypoints = 0;
                enemy.Agent.SetDestination(enemy.pathing.waypoints[indexOfWaypoints].position);
                waitTimer = 0;
            }
        }
    }
}
*/