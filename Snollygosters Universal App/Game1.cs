using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Snollygosters_Universal_App
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //gameRunning Ticks
        public int ticks = 0;
        public static int taxAmount = -10;
        public int playerCash = 0;
        public Random rand = new Random(DateTime.Now.Millisecond);
        SpriteFont messageFont;

        //game states
        enum gameStateEnum
        {
            gameRunning,
            startMenu,
            gameOver,
            highScore
        }

        gameStateEnum gameState;

        //Lists for sprite objects.
        List<Cash> cashList;
        List<Tax> taxList;

        int maxSprites = 4000;
        int taxSpawnRate = 120;

        //Textures 
        Texture2D avatarTexture;
        Texture2D cashTexture;
        Texture2D taxTexture;
        Texture2D startButtonTexture;
        Texture2D highscoreButtonTexture;
        Texture2D endgameTexture;
        Texture2D backgroundTexture;

        int avatarXSpeed = 5;
        int taxSpeed = 4;
        Rectangle avatarRectangle;
        Rectangle startButtonRectangle;
        Rectangle highscoreButtonRectangle;
        Rectangle endgameRectangle;
        Rectangle backgroundRectangle;

        //Window size
        //int windowHeight = 1080;
        //int windowWidth = 900;
        int windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        int windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        MouseState currentMouseState;
        MouseState previousMouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferHeight = windowHeight;
            //graphics.PreferredBackBufferWidth = windowWidth;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            cashList = new List<Cash>();
            taxList = new List<Tax>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Declare Starting gamestate
            gameState = gameStateEnum.startMenu;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            messageFont = Content.Load<SpriteFont>("MessageFont");

            //Loading textures for sprites
            avatarTexture = Content.Load<Texture2D>("Cameron Sprite");
            cashTexture = Content.Load<Texture2D>("CashSprite");
            taxTexture = Content.Load<Texture2D>("Taxsprite");
            startButtonTexture = Content.Load<Texture2D>("StartButtonV3");
            highscoreButtonTexture = Content.Load<Texture2D>("HighScoreButton");
            endgameTexture = Content.Load<Texture2D>("EndGame");
            backgroundTexture = Content.Load<Texture2D>("10 downing street");

            endgameRectangle = new Rectangle(
                0, 0, windowWidth, windowHeight
                );

            backgroundRectangle = new Rectangle(
                0, 0, windowWidth, windowHeight
                );

            avatarRectangle = new Rectangle(
                Window.ClientBounds.Width / 2, windowHeight - (180) - (windowHeight / 10), // sets sprite to start visivble on the screen floor. Works for fullscreen
              120,
              180);

            startButtonRectangle = new Rectangle(((windowWidth / 2) -100), ((windowHeight / 2) -100), 200, 200);
            highscoreButtonRectangle = new Rectangle(((windowWidth / 2) - 100), ((windowHeight / 2) +100), 200, 200);
        }

        protected int randomNumber(int inclusiveLowerbound, int exclusiveUpperbound)
        {
            int output;

            output = rand.Next(inclusiveLowerbound, exclusiveUpperbound);
            return output;
        }

        protected void collisionDetection()
        {
            foreach (Cash cash in cashList)
            {
                if (cash.rectangle.Intersects(avatarRectangle))
                {
                    cash.rectangle.X = randomNumber(0, windowWidth);
                    cash.rectangle.Y = 0;
                    playerCash = playerCash + cash.amount;
                    taxAmount = taxAmount - 5;
                    taxSpawnRate = taxSpawnRate - 1;
                    if (taxSpeed < 10)
                    {
                        taxSpeed = taxSpeed + 1;
                    }
                }
            }
            foreach (Tax tax in taxList)
            {
                if (tax.rectangle.Intersects(avatarRectangle))
                {
                    tax.rectangle.X = randomNumber(0, windowWidth);
                    tax.rectangle.Y = 0;
                    playerCash = playerCash + tax.amount;
                    if (taxSpawnRate < 110)
                    {
                        taxSpawnRate = taxSpawnRate + 1;
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (gameState == gameStateEnum.startMenu)
            {
                startMenuUpdate();
            }
            else if (gameState == gameStateEnum.gameRunning)
            {
                gameRunningUpdate();
            }
            else if (gameState == gameStateEnum.gameOver)
            {
                gameOverUpdate();
            }

            base.Update(gameTime);
        }

        protected void startMenuUpdate()
        {
            MouseState state = Mouse.GetState();
            Point position = new Point(state.X, state.Y);
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;

            playerCash = 0;
            ticks = 0;
            taxAmount = 10;
            taxSpeed = 3;
            taxSpawnRate = 121;
            taxList.Clear();
            cashList.Clear();

            if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if(startButtonRectangle.Contains(position))
                {
                    gameState = gameStateEnum.gameRunning;
                }

                if (highscoreButtonRectangle.Contains(position))
                {
                    gameState = gameStateEnum.highScore;
                }
            }
        }

        protected void gameRunningUpdate()
        {
            ticks++;
            KeyboardState keystate = Keyboard.GetState();

            /////////// RANDOM GENERATION OF SPRITES IN PROGRESS //////////

            int randNum = randomNumber(0, 121);
            if (randNum == 30)
            {
                if (cashList.Count < maxSprites)
                    cashList.Add(new Cash(randomNumber(0, Window.ClientBounds.Width), 0));
            }
            if (randNum > taxSpawnRate)
            {
                if (taxList.Count < maxSprites)
                    taxList.Add(new Tax(randomNumber(0, Window.ClientBounds.Width), 0, taxAmount, taxSpeed));
            }


            if (keystate.IsKeyDown(Keys.A) && avatarRectangle.Left > 1)
            {
                avatarRectangle.X -= avatarXSpeed;
            }
            if (keystate.IsKeyDown(Keys.D) && avatarRectangle.Right < Window.ClientBounds.Width)
            {
                avatarRectangle.X += avatarXSpeed;
            }

            //resets y pos of drops
            List<Cash> cashRemove = new List<Cash>();

            foreach (Cash item in cashList)
            {
                if (item.rectangle.Y >= Window.ClientBounds.Bottom)
                {
                    cashRemove.Add(item);
                }
            }

            foreach (Cash item in cashRemove)
            {
                cashList.Remove(item);
            }

            cashRemove.Clear();

            List<Tax> taxRemove = new List<Tax>();

            foreach (Tax item in taxList)
            {
                if (item.rectangle.Y > windowHeight)
                {
                    taxRemove.Add(item);
                }
            }

            foreach (Tax item in taxRemove)
            {
                taxList.Remove(item);
            }


            foreach (Cash item in cashList)
            {
                item.rectangle.Y += item.speed;
            }
            foreach (Tax item in taxList)
            {
                item.rectangle.Y += item.speed;
            }

            collisionDetection();
            if (playerCash < 0)
            {
                gameState = gameStateEnum.gameOver;
            }
        }

        public void gameOverUpdate()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Enter))
            {
                gameState = gameStateEnum.startMenu;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(avatarTexture, avatarRectangle, Color.White);
            spriteBatch.DrawString(messageFont, playerCash.ToString(), new Vector2(10, 10), Color.Black);

            if (gameState == gameStateEnum.startMenu)
            {
                spriteBatch.Draw(startButtonTexture, startButtonRectangle, Color.White);
                spriteBatch.Draw(highscoreButtonTexture, highscoreButtonRectangle, Color.White);
            }
            if (gameState == gameStateEnum.gameOver)
            {
                spriteBatch.Draw(endgameTexture, endgameRectangle, Color.White);
                spriteBatch.DrawString(messageFont, "Press Enter", new Vector2(windowWidth / 2, (windowHeight / 3) * 2 - 60), Color.Black);
                spriteBatch.DrawString(messageFont, "You Scored " + (ticks / 60).ToString(), new Vector2(windowWidth / 2, (windowHeight / 3) * 2), Color.Black);
            }

            spriteBatch.End();

            if (gameState == gameStateEnum.gameRunning)
            {
                foreach (Cash item in cashList)
                {
                    item.Draw(spriteBatch, cashTexture);
                }
                foreach (Tax item in taxList)
                {
                    item.Draw(spriteBatch, taxTexture);
                }
            }
            base.Draw(gameTime);
        }
    }
}
