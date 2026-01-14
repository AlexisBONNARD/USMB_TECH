using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Xunit;

namespace USMB_TECHTests.E2E.ContactTests;

public class ContactFormTests : BaseTest
{
    [Fact]
    public async Task Contact_Form_Should_Display_All_Main_Fields()
    {
        await Page.GotoAsync(BaseUrl);

        await Expect(Page.GetByTestId("contact-nom")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-prenom")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-email")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-entreprise")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-description")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-type")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("contact-submit")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Contact_Form_Should_Show_Error_When_No_Target_Selected()
    {
        await Page.GotoAsync(BaseUrl);

        await FillBaseForm();

        await Page.GetByTestId("contact-submit").ClickAsync();

        var alert = Page.GetByTestId("contact-alert");
        await Expect(alert).ToBeVisibleAsync();
        await Expect(alert).ToContainTextAsync("Veuillez sélectionner");
    }

    [Fact]
    public async Task Contact_Form_With_Equipement_Should_Submit_Successfully()
    {
        await Page.GotoAsync(BaseUrl);

        await FillBaseForm();

        await Page.GetByTestId("contact-type")
                  .SelectOptionAsync("Equipement");

        var equipementSelect = Page.GetByTestId("contact-equipement-select");
        await Expect(equipementSelect).ToBeVisibleAsync();

        await equipementSelect.SelectOptionAsync(
            new SelectOptionValue { Index = 1 }
        );

        await Page.GetByTestId("contact-submit").ClickAsync();

        var alert = Page.GetByTestId("contact-alert");
        await Expect(alert).ToBeVisibleAsync();
        await Expect(alert).ToContainTextAsync("Votre demande a bien été envoyée");
    }


    // Helper commun
    private async Task FillBaseForm()
    {
        await Expect(Page.GetByTestId("contact-nom")).ToBeVisibleAsync();

        await Page.GetByTestId("contact-nom").FillAsync("Dupont");
        await Page.GetByTestId("contact-prenom").FillAsync("Jean");
        await Page.GetByTestId("contact-email").FillAsync("jean.dupont@test.fr");
        await Page.GetByTestId("contact-entreprise").FillAsync("Entreprise Test");
        await Page.GetByTestId("contact-description")
                  .FillAsync("Besoin de renseignements");
    }
}