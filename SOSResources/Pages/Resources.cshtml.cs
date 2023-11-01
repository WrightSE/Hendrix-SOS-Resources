using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SOSResources.Pages;

public class ResourceModel : PageModel
{
     private readonly ILogger<PrivacyModel> _logger;

    public ResourceModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

}