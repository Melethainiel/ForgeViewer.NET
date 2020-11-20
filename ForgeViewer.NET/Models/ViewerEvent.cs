using System.ComponentModel;

namespace ForgeViewer.NET.Models
{
    public enum ViewerEvent
    {
        [Description("aggregateFitToView")]
        AggregateFitToViewEvent,
        [Description("aggregateIsolation")]
        AggregateIsolationChangedEvent,
        [Description("aggregateSelection")]
        AggregateSelectionChangedEvent,
        [Description("animationReady")]
        AnimationReadyEvent,
        [Description("cameraChanged")]
        CameraChangeEvent,
        [Description("cameraTransitionCompleted")]
        CameraTransitionCompleted,
        [Description("cutplanesChanged")]
        CutPlanesChangeEvent,
        [Description("cancelLeafletScreenshot")]
        CancelLeafletScreenshot,
        [Description("escape")]
        EscapeEvent,
        [Description("explodeChanged")]
        [EventResponse(typeof(Explode))]
        ExplodeChangeEvent,
        [Description("extensionLoaded")]
        [EventResponse(typeof(Extension))]
        ExtensionLoadedEvent,
        [Description("extensionUnloaded")]
        [EventResponse(typeof(Extension))]
        ExtensionUnloadedEvent,
        [Description("finalFrameRenderedChanged")]
        [EventResponse(typeof(FinalFrameRendered))]
        FinalFrameRenderedChangedEvent,
        [Description("fitToView")]
        [EventResponse(typeof(FitToView))]
        FitToViewEvent,
        [Description("fragmentsLoaded")]
        FragmentsLoadedEvent,
        [Description("fullscreenMode")]
        FullscreenModeEvent,
        [Description("geometryLoaded")]
        GeometryLoadedEvent,
        [Description("geometryDownloadComplete")]
        GeometryDownloadCompleteEvent,
        [Description("hide")]
        [EventResponse(typeof(Objects))]
        HideEvent,
        [Description("hyperlink")]
        HyperlinkEvent,
        [Description("isolate")]
        [EventResponse(typeof(Objects))]
        IsolateEvent,
        [Description("layerVisibilityChanged")]
        LayerVisibilityChangedEvent,
        [Description("loadGeometry")]
        LoadGeometryEvent,
        [Description("loadMissingGeometry")]
        [EventResponse(typeof(MissingGeometry))]
        LoadMissingGeometry,
        [Description("modelAdded")]
        ModelAddedEvent,
        [Description("modelRootLoaded")]
        ModelRootLoadedEvent,
        [Description("modelRemoved")]
        ModelRemovedEvent,
        [Description("modelLayersLoaded")]
        ModelLayersLoadedEvent,
        [Description("modelUnloaded")]
        ModelUnloadedEvent,
        [Description("navigationModeChanged")]
        [EventResponse(typeof(NavigationMode))]
        NavigationModeChangedEvent,
        [Description("objectTreeCreated")]
        ObjectTreeCreatedEvent,
        [Description("objectTreeUnavailable")]
        ObjectTreeUnavailableEvent,
        [Description("prefChanged")]
        [EventResponse(typeof(Preference))]
        PrefChangedEvent,
        [Description("prefReset")]
        [EventResponse(typeof(Preference))]
        PrefResetEvent,
        [Description("progressUpdate")]
        [EventResponse(typeof(ProgressUpdate))]
        ProgressUpdateEvent,
        [Description("renderFirstPixel")]
        RenderFirstPixel,
        [Description("renderOptionChanged")]
        RenderOptionChangedEvent,
        [Description("renderPresented")]
        RenderPresentedEvent,
        [Description("reset")]
        ResetEvent,
        [Description("restoreDefaultSettings")]
        RestoreDefaultSettingsEvent,
        [Description("selection")]
        [EventResponse(typeof(Selection))]
        SelectionChangedEvent,
        [Description("show")]
        [EventResponse(typeof(Objects))]
        ShowEvent,
        [Description("texturesLoaded")]
        TexturesLoadedEvent,
        [Description("toolChanged")]
        [EventResponse(typeof(Tool))]
        ToolChangeEvent,
        [Description("toolbarCreated")]
        ToolbarCreatedEvent,
        [Description("viewerInitialized")]
        ViewerInitialized,
        [Description("viewerResize")]
        [EventResponse(typeof(Dimension))]
        ViewerResizeEvent,
        [Description("viewerStateRestored")]
        [EventResponse(typeof(ViewerRestored))]
        ViewerStateRestoredEvent,
        [Description("viewerUninitialized")]
        ViewerUninitialized,
        [Description("webGlContextLost")]
        WebGlContextLostEvent
    }
}