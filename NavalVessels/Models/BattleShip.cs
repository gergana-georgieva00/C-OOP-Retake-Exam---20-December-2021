using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const int initialArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
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
            if (this.ArmorThickness < initialArmorThickness)
            {
                this.ArmorThickness = initialArmorThickness;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + $" *Sonar mode: {(this.SonarMode == true ? "ON" : "OFF")}";
        }
    }
}
