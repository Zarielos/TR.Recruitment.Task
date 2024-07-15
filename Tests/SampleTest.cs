namespace TR.Recruitment.Tests;

[Parallelizable(ParallelScope.Self)]
public class SampleTest : TestBase
{
    private readonly string _firstName = "Joe";
    private readonly string _lastName = "Doe";
    private readonly string _exeAnswer = "input type=\"button\" value=\"OK\"";

    [Test]
    public async Task HTMLFormTest()
    {
        using (var w3SchoolPage = new W3SchoolPage(Page, Context))
        {
            await w3SchoolPage.Goto();
            await w3SchoolPage.AcceptConsent();
            await w3SchoolPage.FillNameForm(_firstName, _lastName);

            var newPage = await w3SchoolPage.SumbitForm();
            await newPage.WaitForLoadStateAsync();
            
            using (var actionPage = new ActionPage(newPage, Context)) // Użyj ActionPage
            {

                // Sprawdź poprawność danych na actionPage
                var displayedFirstName = await actionPage.GetFirstName();
                var displayedLastName = await actionPage.GetLastName();

                Assert.That(displayedFirstName, Is.EqualTo(_firstName));
                Assert.That(displayedLastName, Is.EqualTo(_lastName));
            }
        }
    }

    [Test]
    public async Task OkButtonExcerciseTest()
    {
        using (var w3SchoolPage = new W3SchoolPage(Page, Context))
        {
            await w3SchoolPage.Goto();
            await w3SchoolPage.AcceptConsent();
            var newPage  = await w3SchoolPage.StartExercise();
            await newPage.WaitForLoadStateAsync();

            using (var exePage =  new ExercisePage(newPage, Context))
            {
                await exePage.FillAssignmentInput(_exeAnswer);
                await exePage.ClickSubmitAnswer();
            }
        }
    }
}
