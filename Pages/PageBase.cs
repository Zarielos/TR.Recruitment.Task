using NLog;
namespace TR.Recruitment.Pages;

public abstract class PageBase: IDisposable, IAsyncDisposable
{

    protected readonly IPage page;
    protected IBrowserContext Context { get; }

    protected static readonly ILogger log;
    private readonly ILocator _consentAcceptButton;

    public PageBase(IPage page, IBrowserContext context)
    {
        this.page = page;
        this.Context = context;
       
        this.page.Load += Page_Load;
        this.page.Close += Page_Close;
        this.page.Console += Page_Console;
        this.page.PageError += Page_PageError;
        this.page.Crash += Page_Crash;
        _consentAcceptButton = page.Locator("[id=accept-choices]");
    }

    static PageBase()
    {
        LogManager.LoadConfiguration("nlog.config");
        log = LogManager.GetCurrentClassLogger();
    }
    
    private void Page_Crash(object? sender, IPage e)
    {
        log.Debug($"Crashed page URL is {e.Url}");
    }

    private void Page_PageError(object? sender, string e)
    {
        log.Error(e);        
    }

    private void Page_Console(object? sender, IConsoleMessage e)
    {
        log.Debug(e.ToString());        
    }

    private void Page_Load(object? sender, IPage e)
    {
        log.Debug($"Loaded page URL is {e.Url}");        
    }
    private void Page_Close(object? sender, IPage e)
    {
        log.Debug($"Closed page URL is {e.Url}");        
    }

    public async Task AcceptConsent()
    {
        await _consentAcceptButton.ClickAsync();
    }

    public void Dispose()
    {
        this.page.Load -= Page_Load;
        this.page.Close -= Page_Close;
        this.page.Console -= Page_Console;
        this.page.PageError -= Page_PageError;
        this.page.Crash -= Page_Crash;
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}

