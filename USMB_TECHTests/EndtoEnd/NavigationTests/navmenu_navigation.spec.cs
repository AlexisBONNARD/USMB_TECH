using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.NavigationTests;

public class NavMenuNavigationTests : BaseTest
{
    [Fact]
    public async Task Clicking_On_Pole_Should_Navigate_To_Pole_Page()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.Locator("text=POLES D'EXPERTISE").HoverAsync();

        var firstPole = Page.GetByTestId("nav-pole-item").First;
        await Expect(firstPole).ToBeVisibleAsync();

        await firstPole.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/pole_expertise/\\d+"));
    }

    [Fact]
    public async Task Clicking_On_Equipement_Should_Navigate_To_Equipement_Page()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.Locator("text=EQUIPEMENTS").HoverAsync();

        var firstEquipement = Page.GetByTestId("nav-equipement-item").First;
        await Expect(firstEquipement).ToBeVisibleAsync();

        await firstEquipement.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/equipement/\\d+"));
    }

    [Fact]
    public async Task Clicking_On_Laboratoire_Should_Navigate_To_Laboratoire_Page()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.Locator("text=LABORATOIRE").HoverAsync();

        var firstLab = Page.GetByTestId("nav-laboratoire-item").First;
        await Expect(firstLab).ToBeVisibleAsync();

        await firstLab.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/laboratoire/.+"));
    }
}
