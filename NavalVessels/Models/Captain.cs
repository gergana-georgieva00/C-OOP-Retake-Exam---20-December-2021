using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperience;
        private List<IVessel> vessels;

        public Captain(string fullName)
        {
            this.vessels = new List<IVessel>();
            this.FullName = fullName;
            this.CombatExperience = 0;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Captain full name cannot be null or empty string.");
                }

                this.fullName = value;
            }
        }

        public int CombatExperience { get; private set; }

        public ICollection<IVessel> Vessels => this.vessels.AsReadOnly();

        public void AddVessel(IVessel vessel)
        {
            if (vessel is null)
            {
                throw new NullReferenceException("Null vessel cannot be added to the captain.");
            }

            this.vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {vessels.Count} vessels.");

            if (this.vessels.Count > 0)
            {
                this.vessels.ForEach(v => sb.AppendLine(v.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
