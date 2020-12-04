using AutomationFramework.Models;
using TestConfigurator.Models.API.Board.Enums;
using TestConfigurator.Models.UI;

namespace RegressionApiTests.Models.Board
{
    public class PostBoardModel : IRestObject
    {
        public string name { get; set; }
        public bool defaultLabels { get; set; }
        public bool defaultLists { get; set; }
        public string desc { get; set; }
        public string idOrganization { get; set; }
        public string idBoardSource { get; set; }
        public string keepFromSource { get; set; }
        public string powerUps { get; set; }
        public string prefs_permissionLevel { get; set; }
        public string prefs_voting { get; set; }
        public string prefs_comments { get; set; }
        public string prefs_invitations { get; set; }
        public bool prefs_selfJoin { get; set; }
        public bool prefs_cardCovers { get; set; }
        public string prefs_background { get; set; }
        public string prefs_cardAging { get; set; }

        public PostBoardModel(
            string name,
            bool defaultLabels,
            bool defaultLists,
            string desc,
            string idOrganization,
            string idBoardSource,
            BoardKeepFromSource keepFromSource,
            BoardPowerUps powerUps,
            BoardPrefsPermissionLevel prefs_permissionLevel,
            BoardPrefsVoting prefs_voting,
            BoardPrefsComments prefs_comments,
            BoardsPrefsInvitations prefs_invitations,
            bool prefs_selfJoin,
            bool prefs_cardCovers,
            BoardsPrefsBackground prefs_background,
            BoardsPrefsCardAging prefs_cardAging)
        {
            this.name = name;
            this.defaultLabels = defaultLabels;
            this.defaultLists = defaultLists;
            this.desc = desc;
            this.idOrganization = idOrganization;
            this.idBoardSource = idBoardSource;
            this.keepFromSource = keepFromSource.ToString();
            this.powerUps = powerUps.ToString();
            this.prefs_permissionLevel = prefs_permissionLevel.ToString();
            this.prefs_voting = prefs_voting.ToString();
            this.prefs_comments = prefs_comments.ToString();
            this.prefs_invitations = prefs_invitations.ToString();
            this.prefs_selfJoin = prefs_selfJoin;
            this.prefs_background = prefs_background.ToString();
            this.prefs_cardAging = prefs_cardAging.ToString();
        }
    }
}
