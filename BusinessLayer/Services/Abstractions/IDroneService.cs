using System.Collections.Generic;
using BusinessLayer.Filters;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDroneService
    {
        ICollection<Drone> GetDetachedDrones();

        ICollection<Drone> GetDronesByPersonId(string personId, DroneListFilter filterViewModel);

        Drone GetDroneByCode(string code);

        bool IsCodeValid(string code);

        void AttachDroneToPerson(string personId, string code);

        void DetachDroneToPerson(string personId, string droneId);

        void GenerateDronesPack();
    }
}
