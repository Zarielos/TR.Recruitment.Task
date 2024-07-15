using System.Text.RegularExpressions;

namespace TR.Recruitment.Pages
{
    public class ActionPage : PageBase
    {
        private readonly ILocator _textLocator;
        public ActionPage(IPage page, IBrowserContext context) : base(page, context) 
        {
            _textLocator = page.Locator("div.w3-container.w3-large.w3-border");
        }

        public async Task<string> GetFirstName()
        {
            var text = await _textLocator.InnerTextAsync();
            return Regex.Match(text, @"fname=(\w+)").Groups[1].Value; // Pobierz wartość po "fname="
        }

        public async Task<string> GetLastName()
        {
            var text = await _textLocator.InnerTextAsync();
            return Regex.Match(text, @"lname=(\w+)").Groups[1].Value; // Pobierz wartość po "lname="
        }
    }
}
