using TR.Recruitment.Core;

namespace TR.Recruitment.Tests;

public abstract class TestBase : PageTest
{

    private static readonly PlaywrightConfiguration _playwrightConfiguration;
    static TestBase() => _playwrightConfiguration = new PlaywrightConfiguration();

    [OneTimeSetUp]
    public void Setup()
    {        
        Environment.SetEnvironmentVariable("Browser", _playwrightConfiguration.browserOptions.Name);
        Environment.SetEnvironmentVariable("HEADED", _playwrightConfiguration.browserOptions.Headed);
    }

    public override BrowserNewContextOptions ContextOptions() => _playwrightConfiguration.browserNewContextOptions;
}
