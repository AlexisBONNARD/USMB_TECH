using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.DomaineExcellenceTests;

public class DomaineExcellenceDetailsTests : BaseTest
{
    private const int DomaineId = 1;

    [Fact]
    public async Task Domaine_Page_Should_Display_Title_And_Description()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        await Expect(Page.GetByTestId("domaine-title"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("domaine-description"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Domaine_Page_Should_Display_Pole_Count()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        var count = Page.GetByTestId("domaine-pole-count");
        await Expect(count).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_On_Pole_Card_Should_Navigate_To_Pole_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        var firstPole = Page.GetByTestId("domaine-pole-card").First;
        await Expect(firstPole).ToBeVisibleAsync();

        await firstPole.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/pole_expertise/\\d+"));
    }

    [Fact]
    public async Task Domaine_Page_Should_Display_Contact_Form()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        await Expect(Page.GetByTestId("contact-nom"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }

    // ---------------- ADMIN ----------------

    [Fact]
    public async Task Admin_Should_See_Edit_And_Delete_Buttons()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.GetByTestId("domaine-edit-btn"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("domaine-delete-btn"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        await Page.ClickAsync("text=Admin");

        await Page.GetByTestId("domaine-edit-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/domaine_excellence/edit/\\d+")
        );
    }

    [Fact]
    public async Task Deleting_Domaine_Should_Succeed()
    {
        await Page.GotoAsync($"{BaseUrl}/DomaineExcellence/{DomaineId}");

        await Page.ClickAsync("text=Admin");

        Page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };

        await Page.GetByTestId("domaine-delete-btn").ClickAsync();

        await Expect(Page.GetByTestId("domaine-delete-alert"))
            .ToContainTextAsync("Domaine supprimé avec succès");
    }
}
