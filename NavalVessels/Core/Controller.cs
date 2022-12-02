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
            if (this.vessels.FindByName(attackingVesselName) is null || this.vessels.FindByName(defendingVesselName) is null)
            {
                string result = "";
                if (!(this.vessels.FindByName(attackingVesselName) is null))
                {
                    result = defendingVesselName;
                }
                else
                {
                    result = attackingVesselName;
                }

                return $"Vessel {result} could not be found.";
            }

            var attackingVessel = this.vessels.FindByName(attackingVesselName);
            var defendingVessel = this.vessels.FindByName(defendingVesselName);

            if (attackingVessel.ArmorThickness == 0 || defendingVessel.ArmorThickness == 0)
            {
                string result = "";
                if (!(attackingVessel.ArmorThickness == 0))
                {
                    result = defendingVesselName;
                }
                else
                {
                    result = attackingVesselName;
                }

                return $"Unarmored vessel {result} cannot attack or be attacked.";
            }

            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();

            return $"Vessel {defendingVesselName} was attacked by vessel {attackingVesselName} - current armor thickness: {defendingVessel.ArmorThickness}.";
        }

        public string CaptainReport(string captainFullName)
            => this.captains.Find(c => c.FullName == captainFullName).Report();

        public string HireCaptain(string fullName)
        {
            if (this.captains.Any(c => c.FullName == fullName))
            {
                return $"Captain {fullName} is already hired.";
            }

            var captain = new Captain(fullName);
            this.captains.Add(captain);
            return $"Captain {fullName} is hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (!(this.vessels.FindByName(name) is null))
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }
            if (vesselType != "Submarine" && vesselType != "Battleship")
            {
                return "Invalid vessel type.";
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
            if (this.vessels.FindByName(vesselName) is null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            this.vessels.FindByName(vesselName).RepairVessel();
            return $"Vessel {vesselName} was repaired.";
        }

        public string ToggleSpecialMode(string vesselName)
        {
            if (this.vessels.FindByName(vesselName) is null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            var vessel = this.vessels.FindByName(vesselName);
            switch (vessel.GetType().Name)
            {
                case "Battleship":
                    Battleship battleship = (Battleship)vessel;
                    battleship.ToggleSonarMode();
                    return $"Battleship {vesselName} toggled sonar mode.";
                default:
                    Submarine submarine = (Submarine)vessel;
                    submarine.ToggleSubmergeMode();
                    return $"Submarine {vesselName} toggled submerge mode.";
            }
        }

        public string VesselReport(string vesselName)
            => this.vessels.FindByName(vesselName).ToString().Trim();
    }
}
