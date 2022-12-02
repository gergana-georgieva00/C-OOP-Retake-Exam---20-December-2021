using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const int initialArmorThickness = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed, double armourThickness) : base(name, mainWeaponCaliber, speed, armourThickness)
        {
            this.SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public void ToggleSubmergeMode()
        {
            this.SubmergeMode = !this.SubmergeMode;

            switch (SubmergeMode)
            {
                case true:
                    this.MainWeaponCaliber += 40;
                    this.Speed -= 4;
                    break;
                case false:
                    this.MainWeaponCaliber -= 40;
                    this.Speed += 4;
                    break;
            }
        }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < 200)
            {
                this.ArmorThickness = initialArmorThickness;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" *Submerge mode: {(this.SubmergeMode == true ? "ON" : "OFF")}";
        }
    }
}
