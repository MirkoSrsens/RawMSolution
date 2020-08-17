using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Scripts
{
    public enum GameState { SignIn, FindSession, CreateSession, Start, InGame, GameOver}

    public enum MessageType { StartGame,EndGame,RestartGame,RejoinLobby, UpdatePlayerPos}


    public class GameStateManager
    {
        public int Score { get; set; }

        private SpriteFont scoreFont;

        public GameState currentGameState = GameState.SignIn;

        public Vector2 chasingSpeed = new Vector2(4, 4);

        public Vector2 chasedSpeed = new Vector2(6, 6);
    }
}
