using Microsoft.VisualStudio.TestTools.UnitTesting;
using  Quiz.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz.Models;
using Quiz.Services;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;

namespace TestQuiz
{
    [TestClass]
    public class UnitTest1
    {
        private ApplicationDbContext _context;
        private QuizService quizService;

        [TestInitialize]
        public void Setup()
        {
          
        }


        [TestMethod]
        public void TestMethod1()
        {
            using (_context)
            {
                int quizInstanceid = quizService.Start();
                var quizTest = _context.QuizInstances.Find(quizInstanceid);
                Assert.IsNotNull(quizTest);
                Assert.AreEqual(1, quizTest.Id);

                quizService.Next(quizInstanceid);
                Assert.AreEqual(QuizState.Showquestion, quizTest.State);

                quizService.SetAnswer(quizInstanceid, 1,1,new User {UserId = 1,Name ="Adam1" });
                quizService.SetAnswer(quizInstanceid, 1,2,new User {UserId = 2,Name ="Vincent Vega" });
                quizService.SetAnswer(quizInstanceid, 1,3,new User {UserId = 3,Name ="Mia Wallace" });

                var answerIns = _context.AnswerInstances.Where(a => a.Score == 1).SingleOrDefault();
                Assert.AreEqual("Adam", answerIns.User.Name);

            }

        }
        [TestMethod]
        public void TestMethod2()
        {

            using (var db = createDbContext())
            {
                    

            }

        }

        private ApplicationDbContext createDbContext()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            contextOptionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = aspnet" +
                " - Quiz - 53bc9b9d - 9d6a - 45d4 - 8429 - 2a2761773502; Trusted_Connection = True; MultipleActiveResultSets = true");
            return new ApplicationDbContext(contextOptionsBuilder.Options, null);
        }
    }

}
