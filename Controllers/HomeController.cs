using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizzesApp.Models;

namespace QuizzesApp.Controllers
{
    public static partial class QuizOperations
    {
        private static QuizData GetData()
        {
            const string quizDataFilePath = "./assets/data.json";

            //Can be any method of getting data
            var fetchData = new FetchData();
            var dataFetch = fetchData.GetFromFileToJson(quizDataFilePath) ?? throw new InvalidDataException("Invalid data. Check data fetch from file.");

            //Can validate quiz data, is TODO

            return dataFetch;
        }

        private static QuizzesOverview GetData(ref List<Quiz> quizzes)
        {
            if (quizzes == null)
            {
                throw new ArgumentNullException().ThrowIfNull(nameof(quizzes));
            }

            //Find and return the quiz model
            var quizzesOverviews = quizzes.Select(quiz => new QuizOverview(quiz)).ToList();
            var quizzesOverview = new QuizzesOverview(quizzesOverviews);

            return quizzesOverview;
        }

        public static QuizzesOverview GetQuizzesOverviews()
        {
            //Start by fetching asset data
            var data = QuizOperations.GetData();

            //Only the quiz overview information is needed, so refine it
            var quizData = data.Quizzes;

            var quizzesOverviews = QuizOperations.GetData(ref quizData);

            //Quiz asset data is not needed, so dispose of it
            data.Dispose();
            quizData.Clear();
            quizData.TrimExcess();
            return quizzesOverviews;
        }
    };

    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            // Return if there is no user identity
            if (User.Identity == null) return View();

            // Only continue if user is authenticated
            if (!User.Identity.IsAuthenticated) return View();

            // Send Google identities to external controller
            if(User.Identity.AuthenticationType == "AuthenticationTypes.Federation")
                return Redirect("/external/index");
            
            // If not Identity.Application, continued traffic is not authenticated
            if (!string.Equals(User.Identity.AuthenticationType,"Identity.Application"))
                return View();
                
            // Dotnet ID continue
            // Get the data to populate the starting page
            var quizzesOverviews = QuizOperations.GetQuizzesOverviews();

            return View(quizzesOverviews);
        }
    };
}
