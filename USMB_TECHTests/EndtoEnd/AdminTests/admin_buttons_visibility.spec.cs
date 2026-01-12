using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace USMB_TECHTests.E2E.AdminTests;

public class AdminButtonsVisibilityTests : BaseTest
{
    [Fact]
    public async Task Admin_Buttons_Should_Not_Be_Visible_When_Not_Admin()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Expect(Page.Locator("text=Ajouter un équipement"))
            .Not.ToBeVisibleAsync();
    }

    [Fact]
    public async Task Admin_Buttons_Should_Be_Visible_When_Admin()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.Locator("text=Ajouter un équipement"))
            .ToBeVisibleAsync();

        await Expect(Page.Locator("text=Ajouter un laboratoire"))
            .ToBeVisibleAsync();
    }
}
