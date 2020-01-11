using System;
using System.Collections.Generic;
using NintendoSnitch.Controllers;
using NintendoSnitch.Models;
using NintendoSnitch.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace NintendoSnitch.Tests
{
    public class PollTest
    {
        // Verify that the AddPoll HttpPost method adds a poll
        [Fact]
        public void AddPollTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            var postController = new HomeController(fakeRepo);

            //Act
            // Add the first poll
            Poll poll = new Poll()
            {
                MarioVotes = 0,
                MetroidVotes = 0,
                PikminVotes = 0,
                PokemonVotes = 0,
                ZeldaVotes = 0
            };

            //Assert
            fakeRepo.AddPoll(poll);
            Assert.Single(fakeRepo.pollList);  // Make sure that the poll gets to its spot
        }
        
        [Fact]
        public void AddVoteTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            var postController = new HomeController(fakeRepo);

            //Act
            // Add the first poll
            Poll poll = new Poll()
            {
                MarioVotes = 0,
                MetroidVotes = 0,
                PikminVotes = 0,
                PokemonVotes = 0,
                ZeldaVotes = 0
            };

            //Assert
            fakeRepo.AddPoll(poll);
            Assert.Single(fakeRepo.pollList);  // Make sure that the poll gets to its spot

            Assert.Equal(0, fakeRepo.pollList[0].MarioVotes);
            Assert.Equal(0, fakeRepo.pollList[0].MetroidVotes);
            Assert.Equal(0, fakeRepo.pollList[0].PikminVotes);
            Assert.Equal(0, fakeRepo.pollList[0].PokemonVotes);
            Assert.Equal(0, fakeRepo.pollList[0].ZeldaVotes);

            //------Vote Legend------
            //1 : Mario
            //2 : Metroid
            //3 : Pikmin
            //4 : Pokemon
            //5 : Zelda
            fakeRepo.Polls[0].AddVote(1); //Adds a vote for Mario
            fakeRepo.Polls[0].AddVote(1); //Adds a vote for Mario
            fakeRepo.Polls[0].AddVote(1); //Adds a vote for Mario

            fakeRepo.Polls[0].AddVote(2); //Adds a vote for Metroid

            fakeRepo.Polls[0].AddVote(3); //Adds a vote for Pikmin
            fakeRepo.Polls[0].AddVote(3); //Adds a vote for Pikmin
            fakeRepo.Polls[0].AddVote(3); //Adds a vote for Pikmin
            fakeRepo.Polls[0].AddVote(3); //Adds a vote for Pikmin

            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon
            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon
            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon
            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon
            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon
            fakeRepo.Polls[0].AddVote(4); //Adds a vote for Pokemon

            fakeRepo.Polls[0].AddVote(5); //Adds a vote for Zelda
            fakeRepo.Polls[0].AddVote(5); //Adds a vote for Zelda

            Assert.Equal(3, fakeRepo.pollList[0].MarioVotes);   //checks that 3 votes made it to the Mario section
            Assert.Equal(1, fakeRepo.pollList[0].MetroidVotes); //checks that 3 votes made it to the Metroid section 
            Assert.Equal(4, fakeRepo.pollList[0].PikminVotes);  //checks that 3 votes made it to the Pikmin section
            Assert.Equal(6, fakeRepo.pollList[0].PokemonVotes); //checks that 3 votes made it to the Pokemon section
            Assert.Equal(2, fakeRepo.pollList[0].ZeldaVotes);   //checks that 3 votes made it to the Zelda section
        }
    }
}
