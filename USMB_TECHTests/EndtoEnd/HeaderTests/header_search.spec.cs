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
        await Page.GotoAsync(BaseUrl);

        // Ouvrir options
        var toggle = Page.GetByTestId("header-search-toggle");
        await Expect(toggle).ToBeVisibleAsync();
        await toggle.ClickAsync();

        // Cocher options
        var motClefCheckbox = Page.GetByTestId("search-mode-motclef");
        await Expect(motClefCheckbox).ToBeVisibleAsync();
        await motClefCheckbox.CheckAsync();

        // Remplir champ
        var searchInput = Page.GetByTestId("header-search-input");
        await searchInput.FillAsync("optique");

        // Lancer recherche
        var searchButton = Page.GetByTestId("header-search-button");
        await searchButton.ClickAsync();

        // Vérifier navigation
        await Page.WaitForURLAsync(new Regex("/search/motclef/optique"));
    }
}
