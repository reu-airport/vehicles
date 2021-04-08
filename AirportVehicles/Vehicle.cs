using AirportVehicles.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirportVehicles
{
    class Vehicle
    {
        [JsonPropertyName("vehicleId")]
        public Guid Id { get; } = Guid.NewGuid();

        [JsonPropertyName("vehicleType")]
        public VehicleType Type { get; set; }

        public Route Route { get; set; }

        public bool IsIdle { get; set; } = true;
    
        public int SiteNum { get; set; }

        public LinkedList<VehicleAction> ActionsSequence { get; }
        
        private LinkedListNode<VehicleAction> CurrentActionNode { get; set; }
        public VehicleAction CurrentAction => CurrentActionNode.Value;

        public Vehicle() { }

        public Vehicle(params VehicleAction[] actions)
        {
            ActionsSequence = new LinkedList<VehicleAction>(actions);
            foreach (var action in ActionsSequence)
            {
                action.Vehicle = this;
                action.Done += (s, e) => { 
                    CurrentActionNode = CurrentActionNode.Next;
                    CurrentAction.Initiate();
                };
            }
        }

        // Количество оставшихся запросов для данной машинки на обслуживание борта
        private int _pendingRequestsForOperatingCount = 0;

        public void AcceptRequestForOperating()
        {
            if (IsIdle)
                StartOperating();
            else
                _pendingRequestsForOperatingCount++;
        }

        private void StartOperating()
        {
            CurrentActionNode = ActionsSequence.First;
            CurrentAction.Initiate();
        }
    }
}
