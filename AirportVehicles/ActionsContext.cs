using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    abstract class ActionsContext
    {
        protected LinkedList<VehicleAction> actions;
        protected LinkedListNode<VehicleAction> currentActionNode;
        protected VehicleAction CurrentAction => currentActionNode.Value;

        protected Vehicle Vehicle { get; }

        public bool IsRunning { get; private set; }

        public event EventHandler Completed;

        public ActionsContext(Vehicle vehicle, IEnumerable<VehicleAction> actions)
        {
            Vehicle = vehicle;
            this.actions = new LinkedList<VehicleAction>(actions);
            foreach (var action in this.actions)
            {
                action.Vehicle = Vehicle;
                action.Done += MoveToNextAction;
            }
            Completed += (s, e) => { IsRunning = false; };
        }

        public abstract void Run();

        protected void OnCompleted(object? sender, EventArgs e) =>
            Completed?.Invoke(sender, e);

        protected void MoveToNextAction(object? sender, EventArgs e)
        {
            currentActionNode = currentActionNode.Next;
            CurrentAction.Initiate();
        }
    }
}
