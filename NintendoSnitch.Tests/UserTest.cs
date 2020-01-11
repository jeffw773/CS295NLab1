using System;
using System.Collections.Generic;
using NintendoSnitch.Controllers;
using NintendoSnitch.Models;
using NintendoSnitch.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace NintendoSnitch.Tests
{
    public class UserTest
    {
        // Verify that the AddUser HttpPost method adds a user
        [Fact]
        public void AddUserTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            var postController = new HomeController(fakeRepo);

            // Act & Assert
            User user = new User()
            {
                UserName = "jeffw773",
                EMAIL = "jeffw773@gmail.com",
                IsMember = true,
            };
            fakeRepo.AddUser(user);
            Assert.Single(fakeRepo.UserNames);
            Assert.Equal("jeffw773", fakeRepo.UserNames[0].UserName);
            Assert.Equal("jeffw773@gmail.com", fakeRepo.UserNames[0].EMAIL);

            user = new User()
            {
                UserName = "Mashter Chef",
                EMAIL = "mashterchef@gmail.com",
                IsMember = true,
            };
            fakeRepo.AddUser(user);
            Assert.Equal(2, fakeRepo.UserNames.Count);
            Assert.Equal("Mashter Chef", fakeRepo.UserNames[1].UserName);
            Assert.Equal("mashterchef@gmail.com", fakeRepo.UserNames[1].EMAIL);
        }
    }
}
