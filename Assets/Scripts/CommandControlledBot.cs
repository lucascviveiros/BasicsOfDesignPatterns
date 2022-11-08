using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CommandControlledBot : MonoBehaviour
{
    private NavMeshAgent _agent; 
    private Queue<Command> _commands = new Queue<Command>(); //list of collections in order
    private Command _currentCommand;

    private void Start() => _agent = GetComponent<NavMeshAgent>();

    private void Update() 
    {
        ListenForCommands();
        ProcessCommands();    
    }

    private void ProcessCommands()
    {  
        if(_commands.Any() == false)
        {
            return;
        }

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }

    private void ListenForCommands()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hitInfo))
            {
                _commands.Enqueue(new MoveCommand(hitInfo.point, _agent));
                //var moveCommand = new MoveCommand(hitInfo.point, _agent);
                //_commands.Enqueue(moveCommand);
            }
        }
    }
}

internal class MoveCommand : Command
{
    private readonly Vector3 _destination;
    private readonly NavMeshAgent _agent;

    public MoveCommand(Vector3 destination, NavMeshAgent _agent)
    {
        _destination = destination;
        this._agent = _agent;
    }

    public override void Execute()
    {
        _agent.SetDestination(_destination);
    }

    public override bool IsFinished => _agent.remainingDistance <= 0.1f;
}
