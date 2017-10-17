using AccountabilityLib;

namespace BengansBowlingInterfaceLib
{
    public interface IMatchRepository
    {
        Party GetMatchWinner(int matchId);

    }
}
