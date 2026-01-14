using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.EquipementTests;

public class EquipementDetailsTests : BaseTest
{
    private const int EquipementId = 1;

    [Fact]
    public async Task Equipement_Page_Should_Display_Title_And_Description()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Expect(Page.GetByTestId("equipement-title"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("equipement-description"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Equipement_Page_Should_Display_Photos_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        var photo = Page.GetByTestId("equipement-photo").First;
        await Expect(photo).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Equipement_Page_Should_Display_Keywords_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        var keyword = Page.GetByTestId("equipement-keyword-item").First;
        await Expect(keyword).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Domaine_Should_Navigate_To_Domaine_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Page.GetByTestId("equipement-domaine-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/DomaineExcellence/\\d+")
        );
    }

    [Fact]
    public async Task Clicking_Pole_Should_Navigate_To_Pole_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Page.GetByTestId("equipement-pole-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/pole_expertise/\\d+")
        );
    }

    [Fact]
    public async Task Equipement_Page_Should_Display_Contact_Form()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }

    // ---------------- ADMIN ----------------

    [Fact]
    public async Task Admin_Should_See_Edit_And_Delete_Buttons()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.GetByTestId("equipement-edit-btn"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("equipement-delete-btn"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Page.ClickAsync("text=Admin");

        await Page.GetByTestId("equipement-edit-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/equipement/edit/\\d+")
        );
    }

    [Fact]
    public async Task Deleting_Equipement_Should_Succeed()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/{EquipementId}");

        await Page.ClickAsync("text=Admin");

        Page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };

        await Page.GetByTestId("equipement-delete-btn").ClickAsync();

        await Expect(Page.GetByTestId("equipement-delete-alert"))
            .ToContainTextAsync("Équipement supprimé");
    }
}
