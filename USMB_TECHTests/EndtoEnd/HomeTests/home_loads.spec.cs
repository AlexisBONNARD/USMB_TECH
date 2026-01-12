using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace USMB_TECHTests.E2E.HomeTests;

public class HomeLoadsTests : BaseTest
{
    [Fact]
    public async Task Home_Page_Should_Load_And_Display_Hero_Text()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        var heroText = Page.Locator("#text-home");

        await Expect(heroText).ToBeVisibleAsync();
        await Expect(heroText).ToContainTextAsync("Accédez à des équipements technologiques");
    }

    [Fact]
    public async Task Home_Page_Should_Display_Domaines_Section()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Expect(Page.Locator("text=NOS DOMAINES D’EXCELLENCE"))
            .ToBeVisibleAsync();
    }
}
