# Overlay.NET

## Synopsis

Overlay.NET aims to be An easy-to-use overlay library that allows you to render visuals to other windows, and offers lots of overlay options, from external WPF windows to shared process directx hooks to do the overlay rendering. 



## NuGet: https://www.nuget.org/packages/Overlay.NET/

## Features 
- WPF based overlay.
- Directx based overlay.
- DirectX code is 99% from this project, check it out if interested in dx based overlays - https://github.com/michel-pi/GameOverlay.Net
- Soon: Shared process, or 'in-process' directX engine to use the games directx device instead.
- **Opacity Control**: Set, hide, and show individual overlay elements with custom opacity values.

## Opacity Control

The library now supports opacity control for all overlay elements (shapes, text, images) through brush-based opacity management.

### Usage Examples

```csharp
// Create a brush
int myBrush = overlayWindow.Graphics.CreateBrush(Color.Red);

// Set custom opacity (0.0 = fully transparent, 1.0 = fully opaque)
overlayWindow.Graphics.SetOpacity(myBrush, 0.5f);
overlayWindow.Graphics.DrawRectangle(100, 100, 200, 100, 2, myBrush);

// Hide an element (sets opacity to 0)
overlayWindow.Graphics.Hide(myBrush);

// Show an element (restores default opacity)
overlayWindow.Graphics.Show(myBrush);

// Get current opacity
float currentOpacity = overlayWindow.Graphics.GetOpacity(myBrush);

// Animate opacity
for (float opacity = 0.0f; opacity <= 1.0f; opacity += 0.1f)
{
    overlayWindow.Graphics.SetOpacity(myBrush, opacity);
    overlayWindow.Graphics.DrawCircle(300, 300, 50, 2, myBrush);
}

// Images also support opacity through brushes
overlayWindow.Graphics.DrawImage("path/to/image.png", 100, 100, 200, 200, myBrush);
```

### API Reference

- `SetOpacity(int brush, float opacity)` - Sets the opacity for a brush element (0.0 to 1.0)
- `Hide(int brush)` - Hides a brush element by setting its opacity to 0
- `Show(int brush)` - Shows a brush element by restoring its default opacity
- `GetOpacity(int brush)` - Returns the current opacity value for a brush (0.0 to 1.0)

All drawing methods (DrawLine, DrawRectangle, DrawCircle, FillRectangle, FillCircle, DrawText, DrawImage, etc.) respect the opacity set for their brushes.

## Demos / Examples
You may find the demo code here: https://github.com/lolp1/Overlay.NET/tree/master/src/Overlay.NET.Demo

You may view a quick demo video of the WPF/directx overlay below.

<a href="https://www.youtube.com/watch?v=HN7cdjoMZxc
" target="_blank"><img src="http://img.youtube.com/vi/aq6LG3IML7s/0.jpg" 
alt="Overlay demo" width="300" height="200" border="10" /></a>

## Contributing (please do!)
- If you like it, star it. 
- If you love it, contribute. 
- If you want to add to the sample/demo apps, include a working example in your pull request, preferably added to the demo project.

## Credits
- Julian Forrester aka Arcanaeum for showing me how to make a basic wpf overlay.
- https://github.com/michel-pi/GameOverlay.Net - The external directx2D overlay implementation.
- xaviermonin (https://github.com/xaviermonin) - fixed the most annoying directx overlay bug for the creators window update.
