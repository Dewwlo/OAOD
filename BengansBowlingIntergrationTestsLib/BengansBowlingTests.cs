using System;
using BengansBowlingDbLib;
using BengansBowlingLib;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BengansBowlingIntergrationTestsLib
{
    public class BengansBowlingTests
    {
        private readonly BengansBowlingContext _context;
        public BengansBowlingTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BengansBowlingContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //optionsBuilder.UseSqlServer(
            //    "Server=(localdb)\\mssqllocaldb;Database=BengansBowling;Trusted_Connection=True;MultipleActiveResultSets=True");
            _context = new BengansBowlingContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void CreateParties()
        {
            var sut = new BowlingManager(new SqlPartyRepository(_context), new SqlCompetitionRepository(), new SqlMatchRepository());
            sut.CreateParty("Derp herp", "7701012345");
            sut.CreateParty("Herpus Derpus", "7801012345");
            Assert.Equal(2, sut.PartyCount);
        }
    }
}
