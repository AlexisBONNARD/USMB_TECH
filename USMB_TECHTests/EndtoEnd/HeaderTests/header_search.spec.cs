using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.Xunit;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.HeaderTests;

public class HeaderSearchTests : BaseTest
{
    [Fact]
    public async Task Header_Search_With_No_Options_Should_Use_Full_Mode()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        var headerSearch = Page.Locator("header input[placeholder='Rechercher...']");

        await headerSearch.FillAsync("energie");
        await Page.Keyboard.PressAsync("Enter");

        await Expect(Page).ToHaveURLAsync(
            new Regex("/search/full/energie"));
    }

    [Fact]
    public async Task Header_Search_With_MotClef_Checked_Should_Work()
    {
        await Page.GotoAsync($"{BaseUrl}/");

        await Page.CheckAsync("text=Par mot-clé >> input");
        await Page.FillAsync("header input[placeholder='Rechercher...']", "optique");
        await Page.ClickAsync("header button.search-btn");

        await Expect(Page).ToHaveURLAsync(
            new Regex("/search/motclef/optique"));
    }
}
