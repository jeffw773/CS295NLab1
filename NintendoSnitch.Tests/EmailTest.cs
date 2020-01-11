using System;
using System.Collections.Generic;
using NintendoSnitch.Controllers;
using NintendoSnitch.Models;
using NintendoSnitch.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace NintendoSnitch.Tests
{
    public class EmailTest
    {
        // Verify that the AddEmail HttpPost method adds an email
        [Fact]
        public void AddEmailTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            var postController = new HomeController(fakeRepo);

            Assert.Equal(0, fakeRepo.emailList.Count);
            // Add the first email
            Email email = new Email()
            {
                EmailValue = "masterchief@gmail.com",
                UserNameValue = "Master Chief",
            };
            fakeRepo.AddEmail(email);
            Assert.Single(fakeRepo.emailList);
            Assert.Equal("Master Chief", fakeRepo.emailList[fakeRepo.emailList.Count - 1].UserNameValue);
            Assert.Equal("masterchief@gmail.com", fakeRepo.emailList[fakeRepo.emailList.Count - 1].EmailValue);


            // Add the second email
            email = new Email()
            {
                EmailValue = "RickAndMorty@gmail.com",
                UserNameValue = "Rick and Morty",
            };
            fakeRepo.AddEmail(email);
            Assert.Equal(2, fakeRepo.emailList.Count);
            Assert.Equal("Rick and Morty", fakeRepo.emailList[fakeRepo.emailList.Count - 1].UserNameValue);
            Assert.Equal("RickAndMorty@gmail.com", fakeRepo.emailList[fakeRepo.emailList.Count - 1].EmailValue);

            // Add the third email
            email = new Email()
            {
                EmailValue = "Halo@gmail.com",
                UserNameValue = "Halo",
            };
            fakeRepo.AddEmail(email);
            Assert.Equal(3, fakeRepo.emailList.Count);
            Assert.Equal("Halo", fakeRepo.emailList[fakeRepo.emailList.Count - 1].UserNameValue);
            Assert.Equal("Halo@gmail.com", fakeRepo.emailList[fakeRepo.emailList.Count - 1].EmailValue);
        }
    }
}
