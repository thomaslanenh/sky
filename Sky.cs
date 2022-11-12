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

        private float angle = 0;


        private int score = 0;

        protected Song songNew;
        protected SpriteFont font;
        
        private string prevTime = "00:00";

        // load our Song
        private readonly InternalSong song = new();

      
        public Sky()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            soundEffects = new List<SoundEffect>();

        }


        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            songNew = Content.Load<Song>("fuckit");
            font = Content.Load<SpriteFont>("File");
            MediaPlayer.Play(songNew);
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
            object[] beatTF = new object[] {  };

            beatTF = sentSong.GrabNote(GetHumanReadableTime(time));

             // Check Note
            if ((int)beatTF[0] == 1)
            {
                _spriteBatch.Draw(gemLeft, new Vector2((int)beatTF[1], (int)beatTF[2]), Color.White);
                
            }
            if ((int)beatTF[0] == 2)
            {
                _spriteBatch.Draw(gemRight, new Vector2((int)beatTF[1], (int)beatTF[2]), Color.White);
                
            }


        }

        protected void CheckButtons(KeyboardState state)
        {
            bool leftKeyboardArrow = state.IsKeyDown(Keys.Left);

            bool rightKeyboardArrow = state.IsKeyDown(Keys.Right);

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

                leftSound.Play();
            }

            if (beatB)
            {
                rightSound.Play();
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

            // TODO: Add your update logic here

            score++;

        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);
            TimeSpan time = MediaPlayer.PlayPosition;
            TimeSpan songTime = songNew.Duration;

            _spriteBatch.Begin();
            Vector2 location = new Vector2(400, 240);
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);

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
    }
}