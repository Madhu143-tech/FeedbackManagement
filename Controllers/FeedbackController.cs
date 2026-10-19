using FeedbackManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackManagementMVC.Controllers;

public class FeedbackController : Controller
{
    // Sample in-memory data. Entries reset when the application restarts.
    private static readonly List<Feedback> Feedbacks =
    [
        new Feedback
        {
            Id = 1, StudentName = "Rahul Sharma",
            Email = "rahul@example.com", Category = "Academics", Rating = 5,
            Comments = "Excellent coverage of ASP.NET Core MVC concepts and practical sessions.",
            SubmissionDate = DateTime.Now.AddDays(-2)
        },
        new Feedback
        {
            Id = 2, StudentName = "Pooja Patel",
            Email = "pooja@example.com", Category = "Laboratory", Rating = 4,
            Comments = "Lab computers are well maintained. More practical exercises would help.",
            SubmissionDate = DateTime.Now.AddDays(-1)
        },
        new Feedback
        {
            Id = 3, StudentName = "Amit Verma",
            Email = "amit@example.com", Category = "Infrastructure", Rating = 5,
            Comments = "The internet connection and classroom facilities are very good.",
            SubmissionDate = DateTime.Now
        }
    ];

    private static readonly object _lock = new();

    public IActionResult Index()
    {
        lock (_lock)
        {
            return View(Feedbacks.OrderBy(f => f.Id).ToList());
        }
    }

    public IActionResult Details(int id)
    {
        Feedback? feedback;
        lock (_lock)
        {
            feedback = Feedbacks.FirstOrDefault(f => f.Id == id);
        }
        return feedback is null ? NotFound() : View(feedback);
    }

    [HttpGet]
    public IActionResult Create() => View(new Feedback());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Feedback feedback)
    {
        if (!ModelState.IsValid)
            return View(feedback);

        lock (_lock)
        {
            feedback.Id = Feedbacks.Count == 0 ? 1 : Feedbacks.Max(f => f.Id) + 1;
            feedback.SubmissionDate = DateTime.Now;
            Feedbacks.Add(feedback);
        }

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
