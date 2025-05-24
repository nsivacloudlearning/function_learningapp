using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace learningapp.Pages;

public class IndexModel : PageModel
{
    public List<Course> Courses = new List<Course>();
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnGet()
    {

        string? functionURL = _configuration.GetValue<string>("MySettings:FunctionUrl");
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(functionURL);
            string content = await response.Content.ReadAsStringAsync();
            var deserializedCourses = JsonConvert.DeserializeObject<List<Course>>(content);

            if (deserializedCourses != null)
            {
                Courses = deserializedCourses;
            }

            return Page();
        }

    }
}
