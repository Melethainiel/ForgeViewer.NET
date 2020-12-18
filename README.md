# About ForgeViewer.NET
* Bring the [Forge Viewer](https://forge.autodesk.com/api/viewer-cover-page/) JS library to c# using [JSInterop](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-javascript-from-dotnet) and [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* This library is under construction and is for now closer to a POC than production.
* You will need a Forge Application
* There is a live demo [here](https://forge.my-toolkit.ovh/viewer)


# How to install

### Add ForgeViewer.NET package :
```
PM> Install-Package ForgeViewer.NET
```

### Add Forge Viewer dependencies :
#### • Blazor Server
In `Pages\_Host.cshtml`, add the following code :

```html
<head>
    ...
    <link rel="stylesheet" href="https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style.css" type="text/css"/>
    <script src="https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.js"></script>
</head>
```
In `wwwroot\css\site.css`, add the following code :
```css
.content {
    ...
    height: calc(100% - 120px);    
}
```

#### • Blazor Web Assembly
In `wwwroot\index.html`, add the following code :

```html
<head>
    ...
    <link rel="stylesheet" href="https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style.css" type="text/css"/>
    <script src="https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.js"></script>
</head>
```
In `wwwroot\css\app.css`, add the following code :
```css
.content {
    ...
    height: calc(100% - 120px);    
}
```
# Example

* General implementation to display a model
```c#
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        //Instantiate on first rende the viewing class 
        Viewing = Viewing.Create(ServiceProvider);
        //get options such as Env, Api, GetAccessToken
        var opt = GetOptions();
        //Initialize the Viewing class
        await Viewing.Initializer(opt, OnInit);
    }
    await base.OnAfterRenderAsync(firstRender);
}

private async Task OnInit()
{
    GuiViewer3d = GuiViewer3d.Create(ServiceProvider);
    //get the element to hold the viewer
    await GuiViewer3d.Initializer("id");
    //subscribe to events
    await GuiViewer3d.AddEventListener(ViewerEvent.ExtensionLoadedEvent, OnExtLoaded);
    await GuiViewer3d.AddEventListener(ViewerEvent.SelectionChangedEvent, OnSelectionChanged);
    //Start the viewer
    await GuiViewer3d.Start();
    //load the provided document
    await Document.Load(ServiceProvider, Urn, OnSuccess);
}

private async Task OnSuccess(Document document)
{
    var bubbleNode = await document.GetRoot();
    var getDefaultGeometry = await bubbleNode.GetDefaultGeometry();
    var model = await GuiViewer3d.LoadDocumentNode(document, getDefaultGeometry, null);    
}
```
* Sample to unload an extension
```c#
private async Task OnExtLoaded(object? arg)
{
    var ext = new[] {"Autodesk.Explode"};
    //All event response has a corresponding class 
    if (arg is not ExtensionResponse extensionResponse || !ext.Contains(extensionResponse.ExtensionId))
        return;


    await GuiViewer3d.GetExtensions(extensionResponse.ExtensionId, Callback);
}

private async Task Callback(Extension extension)
{
    await extension.Unload();
}
```