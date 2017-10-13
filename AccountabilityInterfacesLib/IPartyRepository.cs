using System;
using System.Collections.Generic;
using AccountabilityLib;

namespace AccountabilityInterfacesLib
{
    public interface IPartyRepository
    {
        Party Create(string name, string legalId);
        List<Party> Get(string term);
    }
}
