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
            throw new NotImplementedException();
        }

        public void IncreaseCombatExperience()
        {
            throw new NotImplementedException();
        }

        public string Report()
        {
            throw new NotImplementedException();
        }
    }
}
