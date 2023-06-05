using AutomationCore.AssertAndErrorMsgs.UI;
using AutomationCore.Managers;
using OpenQA.Selenium;
using TestsConfigurator.Models.POM.HomePage.Components;

namespace TestsConfigurator.Models.POM.HomePage
{
    public class Home : PagesBase
    {
        //

        private By Icon_Logo => By.XPath("//img[@src='/assets/logo-ff4914e6.webp']");

        public PlatformsDropDownComponent Platforms => new PlatformsDropDownComponent(WebDriver);
        public GenresListComponent Genres => new GenresListComponent(WebDriver);

        //
        private By Header_Parent => By.XPath("//div[@class='jumbotron mainJumbotron']");
        private By Header_Logo => By.XPath(Header_Parent.Criteria + "/img");
        private By Header_Title => By.XPath(Header_Parent.Criteria + "/h1");
        private By Header_Signature => By.XPath(Header_Parent.Criteria + "/p");
        private By Container_Professions => By.XPath("//div[contains(@class, 'professionAlbum')]");
        private By Button_AllProfessions => By.XPath(Container_Professions.Criteria + "//div[contains(@class, 'moreInfoButtonCell')]/a");
        private By ContainerSection_Cook => By.XPath(Container_Professions.Criteria + "//h5[text()='Кухар']/ancestor::div[contains(@class, 'col')]");
        private By ContainerSection_Seamtress => By.XPath(Container_Professions.Criteria + "//h5[text()='Швачка, Кравець']/ancestor::div[contains(@class, 'col')]");
        private By ContainerSection_Seller => By.XPath(Container_Professions.Criteria + "//h5[text()='Продавець']/ancestor::div[contains(@class, 'col')]");

        public bool Is_Cook_ContainerSection_Displayed => WebDriver.FindElement(ContainerSection_Cook).Displayed;
        public bool Is_Seamtress_ContainerSection_Displayed => WebDriver.FindElement(ContainerSection_Seamtress).Displayed;
        public bool Is_Seller_ContainerSection_Displayed => WebDriver.FindElement(ContainerSection_Seller).Displayed;

        public Home(ManagersContainer managersContainer) : base(managersContainer)
        {

        }

        public void Open()
        {
            var url = RunSettings.InstanceUrl;
            if (url is null)
            {
                throw AutomationCore.AssertAndErrorMsgs.AEMessagesBase.GetException("Settings url is empty. Can not procceed.");
            }

            WebDriver.GoToUrl(url);
        }

        public override bool IsLoaded()
        {
            return WebDriver.FindElement(Icon_Logo).Displayed &
                Platforms.IsLoaded() &
                Genres.IsLoaded();
        }

        public Home ScrollTo_Professions_Container()
        {
            WebDriver.ScrollToElement(Button_AllProfessions);
            return this;
        }

        public ProfessionsPage Click_AllProfessions_Button()
        {
            WebDriver.ClickOnElement(Button_AllProfessions);
            return new ProfessionsPage(ManagersContainer);
        }
    }
}
