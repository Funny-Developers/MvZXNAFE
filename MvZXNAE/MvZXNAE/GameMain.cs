using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MvZXNAE
{
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont defaultFont;
        SoundEffect menuBGM;
        SoundEffectInstance bgmInstance;
        Texture2D buttonNormal;
        Texture2D buttonMoveOver;
        Texture2D buttonPressed;
        Rectangle buttonRect;
        Texture2D button;
        bool debugScreenVisable;
        public static KeyboardState keyboardState;
        public KeyboardState keyCache;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = Content.Load<SpriteFont>("DebugScreenFont");
            buttonNormal = Content.Load<Texture2D>("ButtonNormal");//按钮正常
            buttonMoveOver = Content.Load<Texture2D>("ButtonMoveOver");//鼠标悬停到按钮
            buttonPressed = Content.Load<Texture2D>("ButtonPressed");//按钮按下
            button = buttonNormal;
            buttonRect = new Rectangle(300, 200, 128, 128);
            menuBGM = Content.Load<SoundEffect>("MainMenu");
            bgmInstance = menuBGM.CreateInstance();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState(PlayerIndex.One);
            if (keyboardState.IsKeyDown(Keys.F11))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                if (graphics.IsFullScreen == true)
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                }
                else
                {
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.PreferredBackBufferHeight = 600;
                }
                graphics.ApplyChanges();
            }
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)//判断是否按下了鼠标左键
            {
                if (buttonRect.Contains(mouseState.X, mouseState.Y))//判断鼠标是否移动到按钮上并且按下
                {
                    button = buttonPressed;//将按钮设置为按下状态
                    
                }
            }
            else
            {
                if (buttonRect.Contains(mouseState.X, mouseState.Y))//判断鼠标是否移动到按钮上
                {
                    button = buttonMoveOver;//将按钮设置为悬停状态
                }
                else//鼠标不在按钮上
                {
                    button = buttonNormal;//将按钮设置为正常状态
                }
            }
            if (bgmInstance.State == SoundState.Stopped) bgmInstance.Play();
            spriteBatch.Begin();
            IsF3KeyPressed(gameTime);
            DebugScreen(debugScreenVisable);
            spriteBatch.End();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(button, buttonRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void DebugScreen(bool e) // draw the debug screen
        {
            MouseState mouseState = Mouse.GetState();
            if (e) spriteBatch.DrawString(defaultFont, $"v1.0\nMouse position: x:{mouseState.X} y:{mouseState.Y}\nGraphic: {GraphicsAdapter.DefaultAdapter.Description}\nFPS:", Vector2.Zero, Color.Black);
            else spriteBatch.DrawString(defaultFont, "v1.0", Vector2.Zero, Color.Black);
        }

        void IsF3KeyPressed(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.F3) == true)
            {
                debugScreenVisable = !debugScreenVisable;
            }
        }
    }
}
