using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;

        public Vessel(string name, double mainWeaponCaliber, double speed, double armourThickness)
        {
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armourThickness;
        }

        public string Name { get; private set; }

        public ICaptain Captain { get; set; }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; private set; }

        public double Speed { get; private set; }

        public ICollection<string> Targets => throw new NotImplementedException();

        public void Attack(IVessel target)
        {
            throw new NotImplementedException();
        }

        public void RepairVessel()
        {
            throw new NotImplementedException();
        }
    }
}
