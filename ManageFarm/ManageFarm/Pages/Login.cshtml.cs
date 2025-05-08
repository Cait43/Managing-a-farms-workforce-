using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

public class LoginModel : PageModel
{
    [BindProperty]  // This binds the password entered in the form
    public string Password { get; set; }

    public string ErrorMessage { get; set; }  // To hold the error message if login fails

    private static string sitePassword = "$2b$12$g8HZPevkl0LNpvqgFEwP1urx1dHt/IbnMn5NlwNjH4SBWitxZ/9I6"; // The hashed and salted password for the site.
    // On POST request (when user submits the form)
    public IActionResult OnPost()
    {
        // Check if the entered password matches the predefined password
        if (BCrypt.Net.BCrypt.Verify(Password, sitePassword))
        {
            // Set session variable to indicate user is logged in
            HttpContext.Session.SetString("IsLoggedIn", "true");

            // Redirect to Index page (home page)
            return RedirectToPage("/Index");
        }
        else
        {
            // If password is incorrect, show error message
            ErrorMessage = "Invalid password!";
            return Page();  // Re-render the login page with error message
        }
    }

    // On GET request (when the page is loaded for the first time)
    public void OnGet()
    {
        // If the user is already logged in (session is set), redirect to the homepage
        if (HttpContext.Session.GetString("IsLoggedIn") == "true")
        {
            Response.Redirect("/Index");  // Redirect to Index.cshtml if already logged in
        }
    }
}