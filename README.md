# PagerViewController

A basic pager ViewController for Xamarin.iOS.

![Documentation/PagerAnimation.gif](Documentation/PagerAnimation.gi)

## Getting started

```csharp
var colors = new[]
{
	UIColor.FromRGB(76,217,100),
	UIColor.FromRGB(52,170,220),
	UIColor.FromRGB(88,86,214),
 	UIColor.FromRGB(255,45,85),
};

var pages = new[]
{
	new UIViewController() { Title = "First" },
	new UIViewController() { Title = "Second" },
	new UIViewController() { Title = "Third" },
	new UIViewController() { Title = "Last" },
};

var style = new PagerStyle()
{
    SelectedStripColors = colors,
};

var pager = new PagerViewController(style, pages);

var nav = new UINavigationController(pager);
nav.NavigationBar.Translucent = false;
```

## Roadmap / Ideas

- [X] ViewController
- [ ] Setup NuGet
- [ ] More customization
- [ ] Dynamic tabs modification
- [ ] Storyboard integration


## About

This view controller did the trick but is far from a real polished software and needs some work for real customization. Meanwhile, it can save some time to someone, maybe ... And I'll not lost it!

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

### License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)