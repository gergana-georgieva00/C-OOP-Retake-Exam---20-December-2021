using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private List<IVessel> models;

        public VesselRepository()
        {
            this.models = new List<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => this.models.AsReadOnly();

        public void Add(IVessel model)
        {
            throw new NotImplementedException();
        }

        public IVessel FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IVessel model)
        {
            throw new NotImplementedException();
        }
    }
}
