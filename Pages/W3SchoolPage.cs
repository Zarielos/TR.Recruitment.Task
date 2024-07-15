namespace TR.Recruitment.Pages
{
    public class W3SchoolPage : PageBase
    {
        private readonly ILocator _firstNameInput;
        private readonly ILocator _lastNameInput;
        private readonly ILocator _nameSubmitButton;
        private readonly ILocator _startExerciseButton;

        public W3SchoolPage(IPage page, IBrowserContext context) : base(page, context)
        {
            _firstNameInput = page.Locator("[id=fname]");
            _lastNameInput = page.Locator("[id=lname]");
            _nameSubmitButton = page.Locator("//div[@class='w3-example']//input[@type='submit']");
            _startExerciseButton = page.Locator("//a[contains(text(),\"Start the Exercise\")]");
        }

        public async Task Goto()
        {
            await page.GotoAsync("/html/html_forms.asp");
        }

        public async Task FillNameForm(string firstName, string lastName)
        {
            await _firstNameInput.FillAsync(firstName);
            await _lastNameInput.FillAsync(lastName);
        }

        public async Task <IPage> StartExercise()
        {
            var pageBeforeClick = Context.Pages.ToList();
            await _startExerciseButton.ClickAsync();

            while (Context.Pages.Count == pageBeforeClick.Count)
            {
                await Task.Delay(100);
            }

            return Context.Pages.Last();
        }

        public async Task<IPage> SumbitForm()
        {
            var pageBeforeClick = Context.Pages.ToList(); // Utwórz kopię listy
            await _nameSubmitButton.ClickAsync();

            while (Context.Pages.Count == pageBeforeClick.Count)
            {
                await Task.Delay(100);
            }

            return Context.Pages.Last();
        }
    }
}
