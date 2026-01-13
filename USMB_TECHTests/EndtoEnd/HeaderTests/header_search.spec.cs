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
        // 1️⃣ Aller sur la page
        await Page.GotoAsync(BaseUrl);

        // 2️⃣ Ouvrir les options de recherche
        var toggle = Page.GetByTestId("header-search-toggle");
        await Expect(toggle).ToBeVisibleAsync();
        await toggle.ClickAsync();

        // 3️⃣ Cocher "Par mot-clé"
        var motClefCheckbox = Page.GetByTestId("search-mode-motclef");
        await Expect(motClefCheckbox).ToBeVisibleAsync();
        await motClefCheckbox.CheckAsync();

        // 4️⃣ Remplir le champ de recherche
        var searchInput = Page.GetByTestId("header-search-input");
        await searchInput.FillAsync("optique");

        // 5️⃣ Lancer la recherche
        var searchButton = Page.GetByTestId("header-search-button");
        await searchButton.ClickAsync();

        // 6️⃣ Vérifier la navigation
        await Page.WaitForURLAsync(new Regex("/search/motclef/optique"));
    }

}
