using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;

namespace USMB_TECHTests.E2E.ContactTests;

public class ContactListTests : BaseTest
{
    [Fact]
    public async Task Contact_List_Page_Should_Display_Title()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Liste des contacts" }))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Contact_List_Should_Display_Table_When_Contacts_Exist()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        var table = Page.GetByTestId("contact-table");
        await table.WaitForAsync();

        var rows = Page.GetByTestId("contact-row");
        await Expect(rows.First).ToBeVisibleAsync();
    }

    // environement vide
    [Fact]
    public async Task Contact_List_Should_Show_Empty_Message_When_No_Contacts()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Expect(Page.GetByText("Aucune prise de contact trouvée"))
            .ToBeVisibleAsync();
    }

    // pas admin
    [Fact]
    public async Task Contact_List_Should_Not_Show_Admin_Buttons_When_Not_Admin()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Expect(Page.GetByTestId("contact-edit-btn"))
            .Not.ToBeVisibleAsync();

        await Expect(Page.GetByTestId("contact-delete-btn"))
            .Not.ToBeVisibleAsync();
    }

    // admin
    [Fact]
    public async Task Contact_List_Should_Show_Admin_Buttons_When_Admin()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Page.ClickAsync("text=Admin");

        var editBtn = Page.GetByTestId("contact-edit-btn").First;
        await Expect(editBtn).ToBeVisibleAsync();

        var deleteBtn = Page.GetByTestId("contact-delete-btn").First;
        await Expect(deleteBtn).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Clicking_Edit_Should_Navigate_To_Edit_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Page.ClickAsync("text=Admin");

        var editBtn = Page.GetByTestId("contact-edit-btn").First;
        await editBtn.ClickAsync();

        await Page.WaitForURLAsync(new Regex("/contact/edit/\\d+"));
    }

    [Fact]
    public async Task Deleting_Contact_Should_Show_Confirmation_And_Succeed()
    {
        await Page.GotoAsync($"{BaseUrl}/contact");

        await Page.ClickAsync("text=Admin");

        // Accepter automatiquement le confirm()
        Page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };

        var deleteBtn = Page.GetByTestId("contact-delete-btn").First;
        await deleteBtn.ClickAsync();

        await Expect(Page.Locator("body"))
            .ToContainTextAsync("Contact supprimé avec succès");
    }
}