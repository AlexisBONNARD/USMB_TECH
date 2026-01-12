using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace USMB_TECHTests.E2E.HeaderTests;

public class AdminModeTests : BaseTest
{
    [Fact]
    public async Task Clicking_Admin_Button_Should_Enable_Admin_Mode()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.Locator("text=Mode admin"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Admin_Mode_Should_Persist_Using_LocalStorage()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.ClickAsync("text=Admin");
        await Page.ReloadAsync();

        await Expect(Page.Locator("text=Mode admin"))
            .ToBeVisibleAsync();
    }
}
