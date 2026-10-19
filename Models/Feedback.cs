using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FeedbackManagementMVC.Models;

public class Feedback
{
    [ValidateNever]
    public int Id { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 60 characters.")]
    [Display(Name = "Student / User Name")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a feedback category.")]
    [Display(Name = "Feedback Category")]
    public string Category { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    [Display(Name = "Rating (1 to 5 Stars)")]
    public int Rating { get; set; } = 5;

    [Required(ErrorMessage = "Comments cannot be empty.")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Comments must be between 10 and 500 characters.")]
    [Display(Name = "Detailed Feedback / Comments")]
    public string Comments { get; set; } = string.Empty;

    [ValidateNever]
    public DateTime SubmissionDate { get; set; } = DateTime.Now;
}
