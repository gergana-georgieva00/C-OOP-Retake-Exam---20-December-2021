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
        private List<string> targets;

        public Vessel(string name, double mainWeaponCaliber, double speed, double armourThickness)
        {
            this.targets = new List<string>();
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armourThickness;
        }

        public string Name 
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Vessel name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        public ICaptain Captain 
        {
            get => this.captain;
            set
            {
                if (value is null)
                {
                    throw new NullReferenceException("Captain cannot be null.");
                }

                this.captain = value;
            }
        }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets => this.targets.AsReadOnly();

        public void Attack(IVessel target)
        {
            if (target is null)
            {
                throw new NullReferenceException("Target cannot be null.");
            }

            target.ArmorThickness -= this.MainWeaponCaliber;

            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }

            this.targets.Add(target.Name);
        }

        public virtual void RepairVessel()
        {
        }

        public override string ToString()
        {
            return $"- {this.Name}" + Environment.NewLine
                + $" *Type: {this.GetType().Name}" + Environment.NewLine
                + $" *Armor thickness: {this.ArmorThickness}" + Environment.NewLine
                + $" *Main weapon caliber: {this.MainWeaponCaliber}" + Environment.NewLine
                + $" *Speed: {this.Speed} knots" + Environment.NewLine 
                + $" *Targets: {(this.targets.Count == 0 ? "None" : string.Join(", ", this.targets))}" + Environment.NewLine;
        }
    }
}
