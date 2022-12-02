using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const int initialArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed, double armourThickness) : base(name, mainWeaponCaliber, speed, armourThickness)
        {
            this.SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public void ToggleSonarMode()
        {
            this.SonarMode = !this.SonarMode;

            switch (SonarMode)
            {
                case true:
                    this.MainWeaponCaliber += 40;
                    this.Speed -= 5;
                    break;
                case false:
                    this.MainWeaponCaliber -= 40;
                    this.Speed += 5;
                    break;
            }
        }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < 300)
            {
                this.ArmorThickness = initialArmorThickness;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" *Sonar mode: {(this.SonarMode == true ? "ON" : "OFF")}";
        }
    }
}
