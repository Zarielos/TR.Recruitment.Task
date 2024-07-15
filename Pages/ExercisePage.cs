namespace TR.Recruitment.Pages
{
    public class ExercisePage : PageBase
    {
        private readonly ILocator _assignmentInput;
        private readonly ILocator _assignmentSubmitButton;
        private readonly ILocator _assignmentCorrectLocator;
        public ExercisePage(IPage page, IBrowserContext context) : base(page, context) 
        {
            _assignmentInput = page.Locator("//div[@id='assignmentcontainer']//input");
            _assignmentSubmitButton = page.Locator("[id=answerbutton]");
            _assignmentCorrectLocator = page.Locator("[id=assignmentCorrect]");
        }

        public async Task FillAssignmentInput(string value)
        {
            await _assignmentInput.FillAsync(value);
        }

        public async Task ClickSubmitAnswer()
        {
            await _assignmentSubmitButton.ClickAsync();
        }

        public async Task AssertCorrectAnswer()
        {
            var value = _assignmentCorrectLocator.Locator("h2").GetAttributeAsync("text");
            Assert.That(value.ToString(), Is.EqualTo("Correct!"));
        }
    }
}
