using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace USMB_TECHTests.E2E.NavigationTests;

public class NavMenuDropdownTests : BaseTest
{
    [Fact]
    public async Task Pole_Expertise_Dropdown_Should_Open_On_Hover()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        var menu = Page.Locator("text=POLES D'EXPERTISE");
        await menu.HoverAsync();

        await Expect(Page.Locator(".dropdown-menu.show"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Equipements_Dropdown_Should_Open_On_Hover()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.Locator("text=EQUIPEMENTS").HoverAsync();

        await Expect(Page.Locator(".dropdown-menu.show"))
            .ToBeVisibleAsync();
    }
}
