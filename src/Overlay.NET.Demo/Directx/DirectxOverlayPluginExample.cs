using Overlay.NET.Common;
using Overlay.NET.Demo.Internals;
using Overlay.NET.Directx;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Overlay.NET.Demo.Directx
{
    [RegisterPlugin("DirectXverlayDemo-1", "Jacob Kemple", "DirectXOverlayDemo", "0.0",
        "A basic demo of the DirectXoverlay.")]
    public class DirectxOverlayPluginExample : DirectXOverlayPlugin
    {
        private readonly TickEngine _tickEngine = new();
        public readonly ISettings<DemoOverlaySettings> Settings = new SerializableSettings<DemoOverlaySettings>();
        private int _displayFps;
        private int _font;
        private int _hugeFont;
        private int _fps;
        private int _interiorBrush;
        private int _redBrush;
        private int _redOpacityBrush;
        private int _greenBrush;
        private int _blueBrush;
        private float _rotation;
        private Stopwatch _watch;
        private float _opacityDemo = 1.0f;
        private bool _opacityIncreasing = false;

        public override void Initialize(IntPtr targetWindowHandle)
        {
            // Set target window by calling the base method
            base.Initialize(targetWindowHandle);

            // For demo, show how to use settings
            var current = Settings.Current;
            var type = GetType();

            if (current.UpdateRate == 0)
                current.UpdateRate = 1000 / 60;

            current.Author = GetAuthor(type);
            current.Description = GetDescription(type);
            current.Identifier = GetIdentifier(type);
            current.Name = GetName(type);
            current.Version = GetVersion(type);

            // File is made from above info
            Settings.Save();
            Settings.Load();
            Console.Title = @"OverlayExample";

            OverlayWindow = new DirectXOverlayWindow(targetWindowHandle, false);
            _watch = Stopwatch.StartNew();

            _redBrush = OverlayWindow.Graphics.CreateBrush(0x7FFF0000);
            _redOpacityBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(80, 255, 0, 0));
            _interiorBrush = OverlayWindow.Graphics.CreateBrush(0x7FFFFF00);
            _greenBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(255, 0, 255, 0));
            _blueBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(255, 0, 0, 255));

            _font = OverlayWindow.Graphics.CreateFont("Arial", 20);
            _hugeFont = OverlayWindow.Graphics.CreateFont("Arial", 50, true);

            _rotation = 0.0f;
            _displayFps = 0;
            _fps = 0;
            // Set up update interval and register events for the tick engine.

            _tickEngine.PreTick += OnPreTick;
            _tickEngine.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!OverlayWindow.IsVisible)
                return;

            OverlayWindow.Update();
            InternalRender();
        }

        private void OnPreTick(object sender, EventArgs e)
        {
            var targetWindowIsActivated = IsActive();

            if (!targetWindowIsActivated && OverlayWindow.IsVisible)
            {
                _watch.Stop();
                ClearScreen();
                OverlayWindow.Hide();
            }
            else if (targetWindowIsActivated && !OverlayWindow.IsVisible)
                OverlayWindow.Show();
        }

        // ReSharper disable once RedundantOverriddenMember
        public override void Enable()
        {
            _tickEngine.Interval = Settings.Current.UpdateRate.Milliseconds();
            _tickEngine.IsTicking = true;
            base.Enable();
        }

        // ReSharper disable once RedundantOverriddenMember
        public override void Disable()
        {
            _tickEngine.IsTicking = false;
            base.Disable();
        }

        public override void Update() => _tickEngine.Pulse();

        protected void InternalRender()
        {
            if (!_watch.IsRunning)
            {
                _watch.Start();
            }

            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();

            //first row
            OverlayWindow.Graphics.DrawText("DrawBarH", _font, _redBrush, 50, 40);
            OverlayWindow.Graphics.DrawBarH(50, 70, 20, 100, 80, 2, _redBrush, _interiorBrush);

            OverlayWindow.Graphics.DrawText("DrawBarV", _font, _redBrush, 200, 40);
            OverlayWindow.Graphics.DrawBarV(200, 120, 100, 20, 80, 2, _redBrush, _interiorBrush);

            OverlayWindow.Graphics.DrawText("DrawBox2D", _font, _redBrush, 350, 40);
            OverlayWindow.Graphics.DrawBox2D(350, 70, 50, 100, 2, _redBrush, _redOpacityBrush);

            OverlayWindow.Graphics.DrawText("DrawBox3D", _font, _redBrush, 500, 40);
            OverlayWindow.Graphics.DrawBox3D(500, 80, 50, 100, 10, 2, _redBrush, _redOpacityBrush);

            OverlayWindow.Graphics.DrawText("DrawCircle3D", _font, _redBrush, 650, 40);
            OverlayWindow.Graphics.DrawCircle(700, 120, 35, 2, _redBrush);

            OverlayWindow.Graphics.DrawText("DrawEdge", _font, _redBrush, 800, 40);
            OverlayWindow.Graphics.DrawEdge(800, 70, 50, 100, 10, 2, _redBrush);

            OverlayWindow.Graphics.DrawText("DrawLine", _font, _redBrush, 950, 40);
            OverlayWindow.Graphics.DrawLine(950, 70, 1000, 200, 2, _redBrush);

            //second row
            OverlayWindow.Graphics.DrawText("DrawPlus", _font, _redBrush, 50, 250);
            OverlayWindow.Graphics.DrawPlus(70, 300, 15, 2, _redBrush);

            OverlayWindow.Graphics.DrawText("DrawRectangle", _font, _redBrush, 200, 250);
            OverlayWindow.Graphics.DrawRectangle(200, 300, 50, 100, 2, _redBrush);

            OverlayWindow.Graphics.DrawText("DrawRectangle3D", _font, _redBrush, 350, 250);
            OverlayWindow.Graphics.DrawRectangle3D(350, 320, 50, 100, 10, 2, _redBrush);

            OverlayWindow.Graphics.DrawText("FillCircle", _font, _redBrush, 800, 250);
            OverlayWindow.Graphics.FillCircle(850, 350, 50, _redBrush);

            OverlayWindow.Graphics.DrawText("FillRectangle", _font, _redBrush, 950, 250);
            OverlayWindow.Graphics.FillRectangle(950, 300, 50, 100, _redBrush);

            OverlayWindow.Graphics.DrawText("DrawTexture", _font, _redBrush, 1100, 250);
            OverlayWindow.Graphics.DrawImage(@"C:\TextureTest\av.png", 1100, 300, 100, 100);

            _rotation += 0.03f; //related to speed

            if (_rotation > 50.0f)
            {
                _rotation = -50.0f;
            }

            if (_watch.ElapsedMilliseconds > 1000)
            {
                _fps = _displayFps;
                _displayFps = 0;
                _watch.Restart();
            }
            else
                _displayFps++;

            OverlayWindow.Graphics.DrawText("FPS: " + _fps, _hugeFont, _redBrush, 400, 600, false);

            // Opacity demonstration - third row
            OverlayWindow.Graphics.DrawText("Opacity Demo (Animated)", _font, _redBrush, 50, 450);
            
            // Animate opacity
            if (_opacityIncreasing)
            {
                _opacityDemo += 0.01f;
                if (_opacityDemo >= 1.0f)
                {
                    _opacityDemo = 1.0f;
                    _opacityIncreasing = false;
                }
            }
            else
            {
                _opacityDemo -= 0.01f;
                if (_opacityDemo <= 0.0f)
                {
                    _opacityDemo = 0.0f;
                    _opacityIncreasing = true;
                }
            }

            // Set opacity and draw shapes
            OverlayWindow.Graphics.SetOpacity(_greenBrush, _opacityDemo);
            OverlayWindow.Graphics.DrawText($"Opacity: {_opacityDemo:F2}", _font, _greenBrush, 50, 480);
            OverlayWindow.Graphics.FillRectangle(50, 510, 100, 50, _greenBrush);

            // Hide/Show demo
            OverlayWindow.Graphics.DrawText("Hide/Show Demo", _font, _redBrush, 250, 450);
            if (_fps % 2 == 0)
            {
                OverlayWindow.Graphics.Hide(_blueBrush);
            }
            else
            {
                OverlayWindow.Graphics.Show(_blueBrush);
            }
            OverlayWindow.Graphics.DrawText("Blinking", _font, _blueBrush, 250, 480);
            OverlayWindow.Graphics.FillCircle(300, 535, 25, _blueBrush);

            OverlayWindow.Graphics.EndScene();
        }

        public override void Dispose()
        {
            OverlayWindow.Dispose();
            base.Dispose();
        }

        private void ClearScreen()
        {
            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();
            OverlayWindow.Graphics.EndScene();
        }
    }
}
