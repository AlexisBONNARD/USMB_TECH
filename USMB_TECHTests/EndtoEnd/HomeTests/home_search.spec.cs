using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.HomeTests;

public class HomeSearchTests : BaseTest
{
    [Fact]
    public async Task Home_Search_By_Enter_Should_Navigate_To_Search_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        var searchInput = Page.Locator("nav#search-box-nav input");

        await searchInput.FillAsync("robotique");
        await Page.Keyboard.PressAsync("Enter");

        await Expect(Page).ToHaveURLAsync(
            new Regex("/search/motclef/robotique"));
    }

    [Fact]
    public async Task Home_Search_By_Button_Click_Should_Work()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.FillAsync("nav#search-box-nav input", "laser");
        await Page.ClickAsync("nav#search-box-nav button");

        await Expect(Page).ToHaveURLAsync(
            new Regex("/search/motclef/laser"));
    }
}
