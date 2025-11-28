using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Models;

public class CreatePostModel
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; } = "";

    [Required]
    [MinLength(5)]
    public string Body { get; set; } = "";
}
