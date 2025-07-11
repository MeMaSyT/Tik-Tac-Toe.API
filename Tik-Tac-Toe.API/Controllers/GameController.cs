using Microsoft.AspNetCore.Mvc;
using Tik_Tac_Toe.API.Requests;
using Tik_Tac_Toe.Core.Abstractions;

namespace Tik_Tac_Toe.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetGame")]
        public ActionResult GetGame()
        {
            string result = _gameService.GetFieldFromDatabase().Result;
            if(result == "GameNotFound") return NotFound(result);
            if (HttpContext.Request.Cookies["PlayerType"] == null) HttpContext.Response.Cookies.Append("PlayerType", "o");

            return Ok(result);
        }
        [HttpPost("CreateGame")]
        public ActionResult CreateGame()
        {
            var game = _gameService.CreateNewGame().Result;
            if (game == null) return BadRequest();
            HttpContext.Response.Cookies.Append("PlayerType", "x");

            return Created($"/api/game/{game}", game);
        }
        [HttpDelete("EndGame")]
        public ActionResult EndGame(string endMessage = "")
        {
            if (HttpContext.Request.Cookies["PlayerType"] == null) HttpContext.Response.Cookies.Delete("PlayerType");
            return Ok(endMessage == "" ? "" : endMessage);
        }
        [HttpPut("Move")]
        public ActionResult Move([FromBody] MoveRequest request)
        {
            if (HttpContext.Request.Cookies["PlayerType"] != null) {
                char playerType = Convert.ToChar(HttpContext.Request.Cookies["PlayerType"]);
                string callback = _gameService.Move(request.x, request.y, playerType).Result;
                if (callback.Contains("Win") || callback == "_Draw") return RedirectToAction("EndGame", callback);
                return Ok(callback);
            }
            else return Forbid("Game already has a two players");
        }
    }
}
