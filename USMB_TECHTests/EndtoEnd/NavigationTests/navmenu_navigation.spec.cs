using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.NavigationTests;

public class NavMenuNavigationTests : BaseTest
{
    //[Fact]
    //public async Task Clicking_On_Pole_Should_Navigate_To_Pole_Page()
    //{
    //    await Page.GotoAsync(BaseUrl);

    //    var polesMenu = Page.Locator("text=POLES D'EXPERTISE");
    //    await polesMenu.HoverAsync();

    //    await Page.WaitForSelectorAsync("[data-testid='nav-pole-item']");

    //    var firstPole = Page.GetByTestId("nav-pole-item").First;
    //    await firstPole.ClickAsync();

    //    await Page.WaitForURLAsync(new Regex("/pole_expertise/\\d+"));
    //}

    [Fact]
    public async Task Clicking_On_Equipement_Should_Navigate_To_Equipement_Page()
    {
        await Page.GotoAsync(BaseUrl);

        var equipementsMenu = Page.Locator("text=EQUIPEMENTS");
        await equipementsMenu.HoverAsync();

        await Page.WaitForSelectorAsync("[data-testid='nav-equipement-item']");

        var firstEquipement = Page.GetByTestId("nav-equipement-item").First;
        await firstEquipement.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/equipement/\\d+"));
    }

    [Fact]
    public async Task Clicking_On_Laboratoire_Should_Navigate_To_Laboratoire_Page()
    {
        await Page.GotoAsync(BaseUrl);

        var laboMenu = Page.Locator(".usmb-nav .dropdown")
            .Filter(new() { HasText = "LABORATOIRE" });

        await laboMenu.HoverAsync();

        await Page.WaitForSelectorAsync("[data-testid='nav-laboratoire-item']");

        var firstLab = Page.GetByTestId("nav-laboratoire-item").First;
        await firstLab.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/laboratoire/.+"));
    }

}
