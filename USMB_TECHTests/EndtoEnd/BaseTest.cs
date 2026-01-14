// Pour exécuter les tests End2End via Playwright, commencez par installer les navigateurs Playwright en exécutant les commandes suivantes dans le terminal :
// remplacer ... par le chemin cd \...\USMB_TECH\USMB_TECHTests
// bin/Debug/net8.0/playwright.ps1 install

// Ensuite, vous pouvez exécuter les tests avec la commande suivante :
// dotnet test --no-build     ou     dotnet test --filter "Category=E2E"
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using NUnit.Framework;
using Xunit;


namespace USMB_TECHTests;

[Trait("Category", "E2E")]
public class BaseTest : PageTest
{
    protected const string BaseUrl = "https://localhost:7264";

    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}
