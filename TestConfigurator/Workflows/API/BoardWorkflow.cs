using AutomationFramework.Managers;
using RegressionApiTests.Models.Board;
using RestSharp;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestConfigurator.Enums.API;
using TestConfigurator.Models.API.Board.Enums;
using TestConfigurator.Models.UI;

namespace TestConfigurator.Workflows.API
{
    public class BoardWorkflow : ApiWorkflowBase
    {
        public BoardWorkflow(ToolsManager toolsManager) : base(toolsManager) {}

        public PostBoardModel GenerateSimpleBoardForPOST()
        {
            return new PostBoardModel(
                name: _toolsManager._getFakeData.Random.String2(10),
                defaultLabels: _toolsManager._getFakeData.PickRandom(true, false),
                defaultLists: _toolsManager._getFakeData.PickRandom(true, false),
                desc: _toolsManager._getFakeData.Random.String2(10),
                idOrganization: null,
                idBoardSource: null,
                keepFromSource: BoardKeepFromSource.cards,
                powerUps: _toolsManager._getFakeData.PickRandom(BoardPowerUps.all, BoardPowerUps.calendar, BoardPowerUps.cardAging, BoardPowerUps.recap, BoardPowerUps.voting),
                prefs_permissionLevel: BoardPrefsPermissionLevel.Public,
                prefs_voting: BoardPrefsVoting.Public,
                prefs_comments: BoardPrefsComments.Public,
                prefs_invitations: _toolsManager._getFakeData.PickRandom(BoardsPrefsInvitations.admins, BoardsPrefsInvitations.members),
                prefs_selfJoin: _toolsManager._getFakeData.PickRandom(true, false),
                prefs_cardCovers: _toolsManager._getFakeData.PickRandom(true, false),
                prefs_background: _toolsManager._getFakeData.PickRandom(BoardsPrefsBackground.blue, BoardsPrefsBackground.green, BoardsPrefsBackground.grey, BoardsPrefsBackground.lime, BoardsPrefsBackground.orange, BoardsPrefsBackground.pink, BoardsPrefsBackground.purple, BoardsPrefsBackground.red, BoardsPrefsBackground.sky),
                prefs_cardAging: _toolsManager._getFakeData.PickRandom(BoardsPrefsCardAging.Pirate, BoardsPrefsCardAging.Regular)

                );
        }

        public async Task<RestResponse<ResponseBoardModel>> CreateBoard(PostBoardModel modelForPost)
        {
            var result = await _toolsManager._api.RestResponseAsync<ResponseBoardModel>(_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.PostBoard), Method.Post, restObject: modelForPost);

            return result;
        }

        public async Task<RestResponse<object>> RemoveBoardAsync(string id)
        {
            var result = await _toolsManager._api.RestResponseAsync<object>($"{_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.RemoveBoard)}{id}", Method.Post);
            return result;
        }

        public void RemoveAllBaordsAsync()
        {
            var allBoardsGetResponse = _toolsManager._api.RestResponseAsync<List<ResponseBoardModel>>(_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.MyAllBoards), Method.Get);
            Parallel.ForEach(allBoardsGetResponse.Result.Data, async board => { await RemoveBoardAsync(board.id); });
        }
    }
}
