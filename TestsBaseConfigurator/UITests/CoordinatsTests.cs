using NUnit.Framework;
using Tests;
using TestsBaseConfigurator.Enums;
using static TestsBaseConfigurator.POM.GoogleMapsPage;

namespace RegressionTests.UITests
{
    class CoordinatsTests : RegressionTestBase
    {
        private const string expectedGigaBerlinCoordinates = "52.4°N 13.8°E";
        private const string expectedGigaBerlinAddress = "Grünheide, 15537 Grünheide (Mark)";
        private const string expectedGigaBerlinPlusCodes = "CR22+22 Grünheide (Mark)";
        private const string expectedGigaBerlinHeaderPhoto = "https://lh5.googleusercontent.com/p/AF1QipNElNemO0juJzP9RvAUmNxOO7ztyD0XW-oW-HZE=w426-h240-k-no";

        [Test]
        public void CheckArticleCoordinates()
        {
            var wikipediaPage = googlePage.GoToWikipedia();
            var gigaBerlinPage = wikipediaPage.OpenGigaBerlinArticle(WikiArticles.GigaBerlin);
            Assert.IsNotNull(gigaBerlinPage, "It's expected page not be NULL after moving to this page from the 'Home' page");

            //Get Coordinats
            var actualGigaBerlinCoordinates = gigaBerlinPage.GetCoordinates();
            Assert.AreEqual(expectedGigaBerlinCoordinates, actualGigaBerlinCoordinates, "Assert is failed because actual Giga Berlin coordinates are not match with expected");

            //Go to maps
            var googleMapsPage = googlePage.GoToGoogleMaps();
            googleMapsPage.Search(actualGigaBerlinCoordinates);
            Assert.IsTrue(googleMapsPage.IsSearchResultHasExpectedDemographicInfo(GoogleMapDemographicInfo.Address, expectedGigaBerlinAddress), $"It's expected the the result search has ADDRESS: {expectedGigaBerlinAddress}. BUT it was NOT");
            Assert.IsTrue(googleMapsPage.IsSearchResultHasExpectedDemographicInfo(GoogleMapDemographicInfo.PlusCodes, expectedGigaBerlinPlusCodes), $"It's expected the the result search has PLUS CODES: {expectedGigaBerlinPlusCodes}. BUT it was NOT");
            Assert.IsTrue(googleMapsPage.IsSearchResultHasExpectedDemographicInfo(GoogleMapDemographicInfo.HeaderPhoto, expectedGigaBerlinHeaderPhoto), $"It's expected the the result search has header picture has url: {expectedGigaBerlinHeaderPhoto}. BUT it was another");
        }
    }
}
