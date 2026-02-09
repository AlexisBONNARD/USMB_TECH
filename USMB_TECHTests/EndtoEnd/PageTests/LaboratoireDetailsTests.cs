using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.LaboratoireTests;

public class LaboratoireDetailsTests : BaseTest
{
    private const string LaboId = "LISTIC"; // un autre Nom_Court existant pour un des labos

    [Fact]
    public async Task Laboratoire_Page_Should_Display_Main_Information()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        await Expect(Page.GetByTestId("laboratoire-title"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("laboratoire-description"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("laboratoire-adresse"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Laboratoire_Page_Should_Display_Map_Container()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        await Expect(Page.GetByTestId("laboratoire-map"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Laboratoire_Page_Should_Display_Contacts_When_Exists()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        var contact = Page.GetByTestId("laboratoire-contact-item").First;
        await Expect(contact).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Laboratoire_Page_Should_Display_Contact_Form()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }

    // ---------------- ADMIN ----------------

    [Fact]
    public async Task Admin_Should_See_Edit_And_Delete_Buttons()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        await Page.ClickAsync("text=Admin");

        await Expect(Page.GetByTestId("laboratoire-edit-btn"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByTestId("laboratoire-delete-btn"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

        await Page.ClickAsync("text=Admin");

        await Page.GetByTestId("laboratoire-edit-btn").ClickAsync();

        await Page.WaitForURLAsync(
            new Regex("/laboratoire/edit/.+")
        );
    }

    //[Fact]
    //public async Task Deleting_Laboratoire_Should_Succeed()
    //{
    //    await Page.GotoAsync($"{BaseUrl}/laboratoire/{LaboId}");

    //    await Page.ClickAsync("text=Admin");

    //    Page.Dialog += async (_, dialog) =>
    //    {
    //        await dialog.AcceptAsync();
    //    };

    //    await Page.GetByTestId("laboratoire-delete-btn").ClickAsync();

    //    // Redirection + message
    //    await Page.WaitForURLAsync($"{BaseUrl}/");
    //}
}
