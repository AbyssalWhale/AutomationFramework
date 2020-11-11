using AutomationFramework.Entities;
using RegressionApiTests.Enums;
using RegressionApiTests.Models.Board;
using RegressionApiTests.Models.Board.Enums;
using RestSharp;
using System.Threading.Tasks;

namespace RegressionApiTests.Workflows
{
    public class BoardWorkflow : ApiWorkflowBase
    {
        public BoardWorkflow(UtilsManager utilsManager) : base(utilsManager) { }

        public PostBoardModel GenerateSimpleBoardForPOST()
        {
            return new PostBoardModel(
                name: _utilsManager._getFakeData.Random.String2(10),
                defaultLabels: _utilsManager._getFakeData.PickRandom(true, false),
                defaultLists: _utilsManager._getFakeData.PickRandom(true, false),
                desc: _utilsManager._getFakeData.Random.String2(10),
                idOrganization: null,
                idBoardSource: null,
                keepFromSource: BoardKeepFromSource.cards,
                powerUps: _utilsManager._getFakeData.PickRandom(BoardPowerUps.all, BoardPowerUps.calendar, BoardPowerUps.cardAging, BoardPowerUps.recap, BoardPowerUps.voting),
                prefs_permissionLevel: BoardPrefsPermissionLevel.Public,
                prefs_voting: BoardPrefsVoting.Public,
                prefs_comments: BoardPrefsComments.Public,
                prefs_invitations: _utilsManager._getFakeData.PickRandom(BoardsPrefsInvitations.admins, BoardsPrefsInvitations.members),
                prefs_selfJoin: _utilsManager._getFakeData.PickRandom(true, false),
                prefs_cardCovers: _utilsManager._getFakeData.PickRandom(true, false),
                prefs_background: _utilsManager._getFakeData.PickRandom(BoardsPrefsBackground.blue, BoardsPrefsBackground.green, BoardsPrefsBackground.grey, BoardsPrefsBackground.lime, BoardsPrefsBackground.orange, BoardsPrefsBackground.pink, BoardsPrefsBackground.purple, BoardsPrefsBackground.red, BoardsPrefsBackground.sky),
                prefs_cardAging: _utilsManager._getFakeData.PickRandom(BoardsPrefsCardAging.Pirate, BoardsPrefsCardAging.Regular)

                );
        }

        public async Task<IRestResponse<ResponseBoardModel>> CreateBoard(PostBoardModel modelForPost)
        {
            var result = await _utilsManager._api.RestResponseAsync<ResponseBoardModel>(_utilsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.PostBoard), Method.POST, restObject: modelForPost);

            return result;
        }

        public async Task<IRestResponse<object>> RemoveBoardAsync(string id)
        {
            var result = await _utilsManager._api.RestResponseAsync<object>($"{_utilsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.RemoveBoard)}{id}", Method.DELETE);
            return result;
        }
    }
}
