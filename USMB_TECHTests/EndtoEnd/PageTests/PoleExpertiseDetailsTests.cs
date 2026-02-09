using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.PoleExpertiseTests;

public class PoleExpertiseDetailsTests : BaseTest
{
    private const int PoleId = 1;

    [Fact]
    public async Task Pole_Page_Should_Display_Title_And_Description()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        await Expect(Page.GetByTestId("pole-title"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("pole-description"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Pole_Page_Should_Display_Keywords_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        var keyword = Page.GetByTestId("pole-keyword-item").First;
        await Expect(keyword).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Equipement_Should_Navigate_To_Equipement_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        var equipement = Page.GetByTestId("pole-equipement-card").First;
        await equipement.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/equipement/\\d+"));
    }

    [Fact]
    public async Task Clicking_Prestation_Should_Navigate_To_Prestation_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        var prestation = Page.GetByTestId("pole-prestation-card").First;
        await prestation.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/prestation/\\d+"));
    }

    [Fact]
    public async Task Clicking_Domaine_Should_Navigate_To_Domaine_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        await Page.GetByTestId("pole-domaine-btn").ClickAsync();

        await Page.WaitForURLAsync(new Regex("/DomaineExcellence/\\d+"));
    }

    [Fact]
    public async Task Pole_Page_Should_Display_Contact_Form()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }

    // ---------------- ADMIN ----------------

    [Fact]
    public async Task Admin_Should_See_Edit_And_Delete_Buttons()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.GetByTestId("pole-edit-btn"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("pole-delete-btn"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

        await Page.ClickAsync("text=Admin");

        await Page.GetByTestId("pole-edit-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/pole_expertise/edit/\\d+")
        );
    }

    //[Fact]
    //public async Task Deleting_Pole_Should_Succeed()
    //{
    //    await Page.GotoAsync($"{BaseUrl}/pole_expertise/{PoleId}");

    //    await Page.ClickAsync("text=Admin");

    //    Page.Dialog += async (_, dialog) =>
    //    {
    //        await dialog.AcceptAsync();
    //    };

    //    await Page.GetByTestId("pole-delete-btn").ClickAsync();

    //    await Expect(Page.GetByTestId("pole-delete-alert"))
    //        .ToContainTextAsync("Pole d'expertise supprimé");
    //}
}
