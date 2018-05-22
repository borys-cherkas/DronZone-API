using System;
using System.Collections.Generic;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using Common.Models.Additional;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services
{
    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _droneRepository;

        public DroneService(IDroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }

        public ICollection<Drone> GetDetachedDrones()
        {
            return _droneRepository.GetAll(x => string.IsNullOrEmpty(x.OwnerId));
        }

        public ICollection<Drone> GetDronesByPersonId(string personId)
        {
            return _droneRepository.GetAll(x => x.OwnerId == personId);
        }

        public Drone GetDroneByCode(string code)
        {
            return _droneRepository.GetSingleByPredicate(x => String.Equals(x.Code, code, StringComparison.CurrentCultureIgnoreCase));
        }

        public Drone GetDroneById(string droneId)
        {
            return _droneRepository.GetSingleByPredicate(x => String.Equals(x.Id, droneId, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool IsCodeValid(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            return _droneRepository.Any(x => String.Equals(x.Code, code, StringComparison.CurrentCultureIgnoreCase));
        }

        public void AttachDroneToPerson(string personId, string code)
        {
            if (!IsCodeValid(code))
            {
                return;
            }

            var drone = GetDroneByCode(code);

            drone.AttachedDateTime = DateTime.UtcNow;
            drone.OwnerId = personId;

            _droneRepository.Update(drone);
        }

        public void DetachDroneToPerson(string personId, string droneId)
        {
            var drone = GetDroneById(droneId);
            if (drone == null)
            {
                return;
            }

            drone.AttachedDateTime = null;
            drone.OwnerId = null;

            _droneRepository.Update(drone);
        }

        public void GenerateDronesPack()
        {
            var rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                var drone = new Drone()
                {
                    Code = Guid.NewGuid().ToString(),
                    MaxAvailableWeigth = rand.NextDouble() * 3,
                    MaxSpeed = rand.NextDouble() * 20 + 12,
                    Type = (DroneType)rand.Next(0, 3),
                    Weigth = rand.NextDouble() * 2 + 0.3
                };

                _droneRepository.Add(drone);
            }
        }
    }
}
