using System;
using System.Collections.Generic;
using NintendoSnitch.Controllers;
using NintendoSnitch.Models;
using NintendoSnitch.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace NintendoSnitch.Tests
{
    public class PostTest
    {
        // Verify that the AddPost HttpPost method puts a new post in the post fakeRepository
        [Fact]
        public void AddPostTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            var postController = new HomeController(fakeRepo);

            // Act
            //               Post(string thepost, string username, string email, string title, bool ismember)
            postController.Post("I was saved by him!", "Keltoi", "keltoi@gmail.com", "I was saved", true);

            // Assert
            Assert.Equal("I was saved",
                fakeRepo.postList[fakeRepo.postList.Count - 1].Title);
        }
        

        // Verify that the Index HttpGet method returns a sorted list of posts.
        [Fact]
        public void IndexTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            AddTestPosts(fakeRepo);
            var postController = new HomeController(fakeRepo);

            // Act - get a list of posts sorted by title in ascending order
            var result = (ViewResult)postController.PostPage();
            var posts = (List<Post>)result.Model;


            // Assert that post titles are in ascending order.
            // This implicitly checks that there are three posts in the list as well.
            Assert.True(string.Compare(posts[0].Title, posts[1].Title) < 0 &&
                        string.Compare(posts[1].Title, posts[2].Title) < 0);
        }

        // Verify that the AddComment HttpPost method adds a comm for a specific post.
        [Fact]
        public void AddCommentTest()
        {
            // Arrange
            var fakeRepo = new FakePostRepository();
            AddTestPosts(fakeRepo);
            var postController = new HomeController(fakeRepo);

            //public NewComment(string title, string commentText, string username)
            // Act
            postController.NewComment("I Played as Master Chief",
                                       "This post is simply the best.", 
                                       "Jeff Walters");
            // Assert
            Assert.Equal("This post is simply the best.",
                fakeRepo.GetPostByTitle("I Played as Master Chief").comments[0].CommentText);
            //Assert
            Assert.Equal(3, fakeRepo.postList.Count); //Tests that three posts were added to the FakeRepo
            Assert.Equal(3, fakeRepo.usersList.Count); //Tests that 3 users were added to the usersList
            Assert.True(fakeRepo.postList[0].comments.Count == 1); //Checks and confirms that post number 1 has 1 comment from the NewComment call above^
            Assert.True(fakeRepo.postList[1].comments.Count == 0);
            Assert.True(fakeRepo.postList[2].comments.Count == 1); //Tests that post number 3 has 1 comment

        }

        
        // This method adds three posts and one comment to the fakeRepository.
        private void AddTestPosts(FakePostRepository fakeRepo)
        {
            string author1 = "Jeff Walters";
            string author2 = "Keltoi";
            string author3 = "Plastered_Crab";

            //Arange
            // Add the first post
            Post post = new Post()
            {
                Title = "I Played as Master Chief",
                ThePost = "I loved playing as my hero John 117. They captured his likeness perfectly. 10/10 the best game ever!",
                IsMember = true,
                Username = author1,
                Date = new DateTime(2007, 7, 7)
            };

            User user = new User()
            {
                UserName = author1,
                EMAIL = "jeffw773@gmail.com",
                IsMember = true,
            };
            fakeRepo.AddUser(user); //adds a single user to the list
            fakeRepo.AddPost(post);


            // Add the second post
            post = new Post()
            {
                Title = "I saw him once!",
                ThePost = "I really did! Why don't any of you believe me??",
                IsMember = true,
                Username = author2,
                Date = new DateTime(2007, 7, 8)
            };

            user = new User()
            {
                UserName = author2,
                EMAIL = "keltoi@gmail.com",
                IsMember = true,
            };
            fakeRepo.AddUser(user);  //adds a second user to the list
            fakeRepo.AddPost(post);


            // Add the third post and a comment
            post = new Post()
            {
                Title = "I saw him twice!",
                ThePost = "I really did! Please believe me!!",
                IsMember = true,
                Username = author3,
                Date = new DateTime(2007, 7, 9)
            };

            user = new User()
            {
                UserName = author3,
                EMAIL = "plastered_crab@gmail.com",
                IsMember = true,
            };
            Comment testComment = new Comment()
            {
                CommentText = "This is a test comment!",
                Username = "Keltoi",
                PostTitle = "I saw him twice!",
                Date = new DateTime(2019, 7, 7)
            };
            post.comments.Add(testComment);  //adds a comment to the post
            fakeRepo.AddUser(user); //adds a third user to the list
            fakeRepo.AddPost(post); //adds the post with the comment to the list of posts
        }
    }
}
