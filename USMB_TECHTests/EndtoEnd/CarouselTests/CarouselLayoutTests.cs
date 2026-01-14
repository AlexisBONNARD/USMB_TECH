using Microsoft.Playwright;
using Xunit;

namespace USMB_TECHTests.E2E.Layouts;

public class CarouselLayoutTests : BaseTest
{
    [Fact]
    public async Task Carousel_Should_Be_Visible()
    {
        // page qui contient un carousel (ex: /prestation/1)
        await Page.GotoAsync($"{BaseUrl}/prestation/1");

        await Expect(Page.GetByTestId("carousel"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Carousel_Should_Display_Images()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/1");

        var images = Page.GetByTestId("carousel-image");
        await Expect(images.First).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Carousel_Next_Button_Should_Work()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/1");

        var nextBtn = Page.GetByTestId("carousel-next");
        if (await nextBtn.IsVisibleAsync())
        {
            await nextBtn.ClickAsync();
        }

        await Expect(Page.GetByTestId("carousel-track"))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task Carousel_Prev_Button_Should_Work()
    {
        await Page.GotoAsync($"{BaseUrl}/prestation/1");

        var prevBtn = Page.GetByTestId("carousel-prev");
        if (await prevBtn.IsVisibleAsync())
        {
            await prevBtn.ClickAsync();
        }

        await Expect(Page.GetByTestId("carousel-track"))
            .ToBeVisibleAsync();
    }
}
