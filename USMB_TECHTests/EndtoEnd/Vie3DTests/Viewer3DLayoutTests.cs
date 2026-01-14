using Microsoft.Playwright;
using Xunit;

namespace USMB_TECHTests.E2E.Layouts;

public class Viewer3DLayoutTests : BaseTest
{
    [Fact]
    public async Task Viewer3D_Container_Should_Be_Visible()
    {
        // page équipement avec modèle 3D
        await Page.GotoAsync($"{BaseUrl}/equipement/1");

        await Expect(Page.GetByTestId("viewer3d-container"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Viewer3D_Should_Not_Crash_Page()
    {
        await Page.GotoAsync($"{BaseUrl}/equipement/1");

        // Si la page est encore interactive après le chargement, c'est OK
        await Expect(Page.GetByTestId("contact-submit"))
            .ToBeVisibleAsync();
    }
}
