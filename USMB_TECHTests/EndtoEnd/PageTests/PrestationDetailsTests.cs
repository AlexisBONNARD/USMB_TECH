using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.PrestationTests;

public class PrestationDetailsTests : BaseTest
{
    private const int PrestationId = 1;

    [Fact]
    public async Task Prestation_Page_Should_Display_Main_Information()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Expect(Page.GetByTestId("prestation-title"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("prestation-description"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("prestation-laboratoire"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Prestation_Page_Should_Display_Poles_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        var pole = Page.GetByTestId("prestation-pole-item").First;
        await Expect(pole).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Prestation_Page_Should_Display_Equipements_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        var equipement = Page.GetByTestId("prestation-equipement-item").First;
        await Expect(equipement).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_On_Pole_Should_Navigate_To_Pole_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Page.GetByTestId("prestation-pole-item").First.ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/pole_expertise/\\d+")
        );
    }

    [Fact]
    public async Task Clicking_On_Equipement_Should_Navigate_To_Equipement_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Page.GetByTestId("prestation-equipement-item").First.ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/equipement/\\d+")
        );
    }

    [Fact]
    public async Task Prestation_Page_Should_Display_Contact_Form()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }

    // ---------------- ADMIN ----------------

    [Fact]
    public async Task Admin_Should_See_Edit_And_Delete_Buttons()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.GetByTestId("prestation-edit-btn"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("prestation-delete-btn"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

        await Page.ClickAsync("text=Admin");

        await Page.GetByTestId("prestation-edit-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/prestation/edit/\\d+")
        );
    }

    //[Fact]
    //public async Task Deleting_Prestation_Should_Succeed()
    //{
    //    await Page.GotoAsync($"{BaseUrl}/prestation/{PrestationId}");

    //    await Page.ClickAsync("text=Admin");

    //    Page.Dialog += async (_, dialog) =>
    //    {
    //        await dialog.AcceptAsync();
    //    };

    //    await Page.GetByTestId("prestation-delete-btn").ClickAsync();

    //    await Expect(Page.GetByTestId("prestation-delete-alert"))
    //        .ToContainTextAsync("Prestation supprimée");
    //}
}
