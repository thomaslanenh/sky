using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Diagnostics;
using SKY.Content.Songs;

namespace SKY
{
    public class Sky : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<SoundEffect> soundEffects;

        private Texture2D gemLeft;
        private Texture2D gemRight;
        private Texture2D arrow;
        private Texture2D ghost;

        private float angle = 0;

        private int score = 0;

        protected Song songNew;
        protected Song menuSong;
        protected SpriteFont font;
        
        private string prevTime = "00:00";

        private enum GameStates {
                START,
                OPTIONS,
                LOAD_SONG,
                SONG_IN_GAME
            };

        GameStates CurrentState = GameStates.START;

        // load our Song
        private readonly InternalSong song = new();

        private MainMenu menu = new();

        private int CurrentNote;

        private bool isPlaying;

        public Sky()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            soundEffects = new List<SoundEffect>();
            isPlaying = false;
        }

        // Test Event Function
    
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            arrow = Content.Load<Texture2D>("arrow");
            Texture2D texture = Content.Load<Texture2D>("SmileyWalk");

            // Gem Left
            gemLeft = Content.Load<Texture2D>("Coin");

            // Gem Right
            gemRight = Content.Load<Texture2D>("Tyrano Lair");

            soundEffects.Add(Content.Load<SoundEffect>("leftsound"));
            soundEffects.Add(Content.Load<SoundEffect>("rightsound"));
            soundEffects.Add(Content.Load<SoundEffect>("./SoundEffects/soundselect"));

            // main menu song
            menuSong = Content.Load<Song>("./SongFiles/And Then - Old Iron");

            // in game test song
            songNew = Content.Load<Song>("fuckit");

            font = Content.Load<SpriteFont>("File");


            // Load Menu Icons
            ghost = Content.Load<Texture2D>("pngwing.com");
        }

        public static void ChangeGameState()
        {

            Debug.WriteLine("New Menu State");

        }

        public string GetHumanReadableTime(TimeSpan time)
        {
            int minutes = time.Minutes;
            int seconds = time.Seconds;

            if (seconds < 10)
            {
                return minutes + ":0" + seconds;
            }else
            {
                return minutes + ":" + seconds;
            }
        }

        protected void CheckMouse(MouseState mouseState)
        {
            int x = mouseState.X;
            int y = mouseState.Y;

            
            if (x <= 380)
            {
                angle -= 0.06f;
            }
            if (x >= 381)
            {
                angle += 0.06f;
            }
        }

        protected void CheckPad(GamePadState padState)
        {
            if (padState.IsConnected)
            {
                float maxSpeed = 0.1f;
                float changeInAngleLeft = padState.ThumbSticks.Left.X * maxSpeed;
                float changeInAngleRight = padState.ThumbSticks.Right.X * maxSpeed;

                angle -= changeInAngleLeft;
                angle += changeInAngleRight;

            }
        }

        protected int GetRandomNum(int min, int max)
        {
            Random r = new Random();

            int rInt = r.Next(min, max);

            return rInt;
        }

        protected void CheckBeat(InternalSong sentSong, TimeSpan time)
        {
         
            var beatTF = sentSong.GrabNote(GetHumanReadableTime(time));

             // Check Note
            if (beatTF.Note == 1)
            {
                CurrentNote = 1;
                _spriteBatch.Draw(gemLeft, new Vector2(beatTF.X, beatTF.Y), Color.White);
                
            }
            if (beatTF.Note == 2)
            {
                CurrentNote = 2;
                _spriteBatch.Draw(gemRight, new Vector2(beatTF.X, beatTF.Y), Color.White);
                
            }


        }

        protected void CheckButtons(KeyboardState state)
        {
            bool leftKeyboardArrow = state.IsKeyDown(Keys.Left);

            bool rightKeyboardArrow = state.IsKeyDown(Keys.Right);

            bool upKeyboardArrow = state.IsKeyDown(Keys.Up);

            bool downKeyboardArrow = state.IsKeyDown(Keys.Down);

            bool enterKeyboardButton = state.IsKeyDown(Keys.Enter);

            // If current state is in game
            if (CurrentState == GameStates.SONG_IN_GAME)
            {
                bool beatA = state.IsKeyDown(Keys.LeftShift);
                bool beatB = state.IsKeyDown(Keys.Space);

                var leftSound = soundEffects[0].CreateInstance();
                leftSound.IsLooped = false;

                var rightSound = soundEffects[1].CreateInstance();
                rightSound.IsLooped = false;

                // movement
                if (leftKeyboardArrow)
                {
                    angle -= 0.1f;
                }
                if (rightKeyboardArrow)
                {
                    angle += 0.1f;
                }

                // beats
                // Left Beat
                if (beatA)
                {
                    if (CurrentNote == 1)
                    {
                        leftSound.Play();
                        score = score + 10;
                        CurrentNote = 0;
                        // flag beat for delete 
                        return;
                    }

                }

                if (beatB)
                {
                    if (CurrentNote == 2)
                    {
                        rightSound.Play();
                        score = score + 10;
                        CurrentNote = 0;
                        return;
                    }
                }
            }


            // If current state is main menu
            if (CurrentState == GameStates.START)
            {
                var selectSound = soundEffects[2].CreateInstance();


                static void StartGameIcon()
                {
                    Debug.WriteLine("Moving icon to Start Game");
                }

                static void OptionsIcon()
                {
                    Debug.WriteLine("Moving icon to Options Icon");
                }

                if (enterKeyboardButton)
                {
                    menu.ChangeMenu += ChangeGameState;
                    menu.SelectOption();
                    menu.ChangeMenu -= ChangeGameState;
                    switch (menu.CurrentItem)
                    {
                        case "START":
                            CurrentState = GameStates.SONG_IN_GAME;
                            break;
                        case "OPTIONS":
                            CurrentState = GameStates.OPTIONS;
                            break;
                    }
                    selectSound.Play();
                    isPlaying = false;

                }
                if (upKeyboardArrow)
                {
                    menu.ChangeCursorPositionUp += StartGameIcon;
                    menu.ChangeCursorPosition("up");
                    menu.ChangeCursorPositionUp -= StartGameIcon;
                }
                if (downKeyboardArrow)
                {
                    menu.ChangeCursorPositionDown += OptionsIcon;
                    menu.ChangeCursorPosition("down");
                    menu.ChangeCursorPositionDown -= OptionsIcon;
                }

            }
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            GamePadState padState = GamePad.GetState(PlayerIndex.One);

            // CheckMouse(mouseState);
            CheckButtons(state);
            CheckPad(padState);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        private void RenderSong(GameTime gameTime)
        {

            if (!isPlaying)
            {
                MediaPlayer.Play(songNew);
                isPlaying = true;
            }

            GraphicsDevice.Clear(Color.White);
            TimeSpan time = MediaPlayer.PlayPosition;
            TimeSpan songTime = songNew.Duration;

            _spriteBatch.Begin();
            Vector2 location = new Vector2(400, 240);
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);

            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(100, 100), Color.Black);
            _spriteBatch.DrawString(font, GetHumanReadableTime(time) + " / " + GetHumanReadableTime(songTime), new Vector2(100, 150), Color.Black);

            prevTime = Truncate(prevTime, 4);

            if (prevTime != time.ToString())
            {
                CheckBeat(song, time);
                prevTime = time.ToString();
            }

            _spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

            base.Draw(gameTime);

            _spriteBatch.End();
        }

        private void StartMenu(GameTime gameTime)
        {
            var selectSound = soundEffects[2].CreateInstance();

            if (!isPlaying)
            {
                MediaPlayer.Play(menuSong);
                isPlaying = true;
            }


            GraphicsDevice.Clear(Color.AliceBlue);

            Vector2 location = new Vector2(250, menu.CursorY);
            Rectangle sourceRectangle = new Rectangle(0, 0, ghost.Width, ghost.Height);
            Vector2 origin = new Vector2(ghost.Width / 2, ghost.Height);

            // Draw Main Menu
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "SKY", new Vector2(100, 100), Color.Black);
            _spriteBatch.DrawString(font, "START GAME", new Vector2(320, 220), Color.Black);
            _spriteBatch.DrawString(font, "OPTIONS", new Vector2(320, 275), Color.Black);
            _spriteBatch.Draw(ghost, location, sourceRectangle, Color.White, 0f, new Vector2(0,0), .1f, SpriteEffects.None, 1);
            _spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (CurrentState == GameStates.START)
            {
                StartMenu(gameTime);
            }
            if (CurrentState == GameStates.SONG_IN_GAME)
            {
                RenderSong(gameTime);
            }
        }
    }
}