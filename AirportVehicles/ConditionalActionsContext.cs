using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    class ConditionalActionsContext : ActionsContext
    {
        public bool Condition { get; set; }

        private LinkedList<VehicleAction> _onConditionActions;
        private LinkedList<VehicleAction> _finalActions;

        public ConditionalActionsContext(
            Vehicle vehicle, 
            IEnumerable<VehicleAction> initialActions, 
            IEnumerable<VehicleAction> onConditionActions,
            IEnumerable<VehicleAction> finalActions
            ): base(vehicle, initialActions)
        {
            _onConditionActions = new LinkedList<VehicleAction>(onConditionActions);
            _finalActions = new LinkedList<VehicleAction>(finalActions);
            foreach (var action in _onConditionActions)
                action.Done += MoveToNextAction;
            foreach (var action in _finalActions)
                action.Done += MoveToNextAction;
            actions.Last.Value.Done += (s, e) => ExpandActionsListConditionally();
            _onConditionActions.Last.Value.Done += (s, e) => ExpandActionsListConditionally();
            _finalActions.Last.Value.Done += (s, e) => OnCompleted(this, EventArgs.Empty);
        }

        public override void Run()
        {
            currentActionNode = actions.First;
            //actions.Last.Value.Done += (s, e) =>
            ExpandActionsListConditionally();
            CurrentAction.Initiate();
        }

        private void ExpandActionsListConditionally() => 
            actions.AddLast(Condition ? _onConditionActions.First : _finalActions.First);
    }
}
