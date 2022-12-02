using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captains;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new List<ICaptain>();
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            if (!this.captains.Any(c => c.FullName == selectedCaptainName))
            {
                return $"Captain {selectedCaptainName} could not be found.";
            }
            if (this.vessels.FindByName(selectedVesselName) is null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
            }
            if (!(this.vessels.FindByName(selectedVesselName).Captain is null))
            {
                return $"Vessel {selectedVesselName} is already occupied.";
            }

            this.captains.Find(c => c.FullName == selectedCaptainName).AddVessel(this.vessels.FindByName(selectedVesselName));
            this.vessels.FindByName(selectedVesselName).Captain = this.captains.Find(c => c.FullName == selectedCaptainName);

            return $"Captain {selectedCaptainName} command vessel {selectedVesselName}.";
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            throw new NotImplementedException();
        }

        public string CaptainReport(string captainFullName)
            => this.captains.Find(c => c.FullName == captainFullName).Report();

        public string HireCaptain(string fullName)
        {
            var captain = new Captain(fullName);

            if (this.captains.Any(c => c.FullName == fullName))
            {
                return $"Captain {fullName} is already hired.";
            }

            this.captains.Add(captain);
            return $"Captain {fullName} is hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vesselType != "Submarine" && vesselType != "Battleship")
            {
                return "Invalid vessel type.";
            }
            if (!(this.vessels.FindByName(name) is null))
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }

            IVessel vessel;
            switch (vesselType)
            {
                case "Submarine":
                    vessel = new Submarine(name, mainWeaponCaliber, speed);
                    break;
                default:
                    vessel = new Battleship(name, mainWeaponCaliber, speed);
                    break;
            }

            this.vessels.Add(vessel);
            return $"{vesselType} {name} is manufactured with the main weapon caliber of {mainWeaponCaliber} inches and a maximum speed of {speed} knots.";
        }

        public string ServiceVessel(string vesselName)
        {
            throw new NotImplementedException();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            throw new NotImplementedException();
        }

        public string VesselReport(string vesselName)
        {
            throw new NotImplementedException();
        }
    }
}
