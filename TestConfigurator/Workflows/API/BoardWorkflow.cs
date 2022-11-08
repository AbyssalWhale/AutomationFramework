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

        public async Task<RestResponse<ResponseBoardModel>> CreateBoard(PostBoardModel modelForPost)
        {
            var result = await _toolsManager._api.RestResponseAsync<ResponseBoardModel>(_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.PostBoard), Method.Post, restObject: modelForPost);

            return result;
        }

        public async Task<RestResponse<ResponseBoardModel>> CreateBoard(string boardName)
        {
            var url = _toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.PostBoard);
            url += $"?name={boardName}"; 
            var result = await _toolsManager._api.RestResponseAsync<ResponseBoardModel>(url, Method.Post);

            return result;
        }

        public async Task<RestResponse<object>> RemoveBoardAsync(string id)
        {
            var url = $"{_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.RemoveBoard)}{id}";
            var result = await _toolsManager._api.RestResponseAsync<object>(url, Method.Delete);

            return result;
        }

        public void RemoveAllBaordsAsync()
        {
            var allBoardsGetResponse = _toolsManager._api.RestResponseAsync<List<ResponseBoardModel>>(_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.MyAllBoards), Method.Get);
            Parallel.ForEach(allBoardsGetResponse.Result.Data, async board => { await RemoveBoardAsync(board.Id); });
        }
    }
}
