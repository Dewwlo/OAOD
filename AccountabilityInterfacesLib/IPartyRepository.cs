using System;
using System.Collections.Generic;
using AccountabilityLib;

namespace AccountabilityInterfacesLib
{
    public interface IPartyRepository
    {
        void Create(string name, string legalId);
        List<Party> All();
        List<Party> Get(string term);
    }
}
