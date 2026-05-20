using Google.Apis.Auth.AspNetCore3;

using Microsoft.AspNetCore.Mvc;

namespace QuizzesApp.Controllers
{
    public class ExternalController : Controller
    {
        // GET: External/Index
        [GoogleScopedAuthorize]
        private ActionResult Index()
        {
            // Return if there is no user identity
            if (User.Identity == null) return View("~/Views/Home/Index");
            
            // Only continue if user is authenticated
            if (!User.Identity.IsAuthenticated) return View("~/Views/Home/Index");

            // Get the data to populate the starting page
            var quizzesOverviews = QuizOperations.GetQuizzesOverviews();

            return View("~/Views/Home/Index.cshtml", quizzesOverviews);
        }

        [HttpGet]
        [GoogleScopedAuthorize]
        [Route("External/Index")]
        public async Task<IActionResult> Index([FromServices] IGoogleAuthProvider auth)
        {
            var cred = await auth.GetCredentialAsync();
            var cookies = Request.Cookies;
            
            return cookies.Any(item => item.Key == "Identity.External") ? Index() :
                // No authority cookie exists, so return
                Redirect("/");
        }

        // GET: External/Questions
        [GoogleScopedAuthorize]
        public ActionResult Questions(string quiz)
        {
            // Return if there is no user identity
            if (User.Identity == null) return View("~/Views/Home/Index");

            // Only continue if user is authenticated
            if (!User.Identity.IsAuthenticated) return View("~/Views/Home/Index");

            // If no quiz is selected, exit
            if (string.IsNullOrEmpty(quiz)) return Redirect("~/Views/Home/Index");

            // First, confirm the requested quiz name is legitimate/found
            var match = QuizOperations.GetQuizNameMatch(quiz) ?? throw new Exception("Quiz not found");

            // The quiz name is valid; get that data for the user to view
            var quizOverview = QuizOperations.GetQuizData(match);

            return View("~/Views/Quiz/Questions.cshtml", quizOverview);
        }

        // GET: External/Results
        [GoogleScopedAuthorize]
        public ActionResult Results()
        {
            return View("~/Views/Quiz/Results.cshtml");
        }
    }
}
