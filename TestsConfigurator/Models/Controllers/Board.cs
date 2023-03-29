using AutomationCore.Managers;
using RestSharp;
using TestsConfigurator.Models.API.Trello.Board;

namespace TestsConfigurator.Models.Controllers
{
    public class Board : Base
    {
        public Board(RestApiManager apiManager) : base(apiManager)
        {
            
        }

        public RestResponse<List<ResponseBoardModel>> Get() => _apiManager.ExecuteAsync<List<ResponseBoardModel>>(endPoint: "members/me/boards", method: Method.Get).Result;

        public RestResponse<ResponseBoardModel> Post(string name) => _apiManager.ExecuteAsync<ResponseBoardModel>(endPoint: $"boards/?name={name}", method: Method.Post).Result;
    }
}
