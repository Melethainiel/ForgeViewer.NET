export function ViewingInitializer(options, helper) {
    if (options.getAccessToken) {
        options.getAccessToken = async function (onSuccess) {
            let accessToken = await new Promise((resolve => resolve(helper.invokeMethodAsync("GetAccessToken"))));
            let expirationTimeSeconds = 5 * 60; // 5 minutes
            onSuccess(accessToken, expirationTimeSeconds);
        }
    }
    Autodesk.Viewing.Initializer(options, () => {
        helper.invokeMethodAsync("InitializerCallback")
    });
}

export function Viewer3dInitializer(id) {
    return new Autodesk.Viewing.Viewer3D(document.getElementById(id));
}

export function AddViewEventListener(view, eventName, helper) {
    view.addEventListener(eventName, (obj) => {
        OnEventRaised(eventName, obj, helper);
    })
}

function OnEventRaised(eventName, obj, helper) {
    switch (eventName) {
        case "explodeChanged":
            helper.invokeMethodAsync("EventListener", eventName, {
                scale: obj.scale
            });
            break;
        case "extensionLoaded":
            helper.invokeMethodAsync("EventListener", eventName, {
                extensionId: obj.extensionId
            });
            break;
        case "extensionUnloaded":
            helper.invokeMethodAsync("EventListener", eventName, {
                extensionId: obj.extensionId
            });
            break;
        case "finalFrameRenderedChanged":
            helper.invokeMethodAsync("EventListener", eventName, {
                finalFrame: obj.finalFrame
            });
            break;
        case "fitToView":
            helper.invokeMethodAsync("EventListener", eventName, {
                immediate: obj.immediate,
                nodeIdArray: obj.nodeIdArray
            });
            break;
        case "hide":
            helper.invokeMethodAsync("EventListener", eventName, {
                nodeIdArray: obj.nodeIdArray
            });
            break;
        case "isolate":
            helper.invokeMethodAsync("EventListener", eventName, {
                nodeIdArray: obj.nodeIdArray
            });
            break;
        case "loadMissingGeometry":
            helper.invokeMethodAsync("EventListener", eventName, {
                delay: obj.delay
            });
            break;
        case "navigationModeChanged":
            helper.invokeMethodAsync("EventListener", eventName, {
                id: obj.id
            });
            break;
        case "progressUpdate":
            helper.invokeMethodAsync("EventListener", eventName, {
                percent: obj.percent,
                state: obj.state
            });
            break;
        case "prefChanged":
            helper.invokeMethodAsync("EventListener", eventName, {
                name: obj.name,
                value: obj.value
            });
            break;
        case "prefReset":
            helper.invokeMethodAsync("EventListener", eventName, {
                name: obj.name,
                value: obj.value
            });
            break;
        case "selection" :
            helper.invokeMethodAsync("EventListener", eventName, {
                fragIdsArray: obj.fragIdsArray,
                dbIdArray: obj.dbIdArray,
                nodeArray : obj.nodeArray
            });
            break;
        case "show" :
            helper.invokeMethodAsync("EventListener", eventName, {     
                nodeIdArray: obj.nodeIdArray
            });
            break;
        case "toolChanged":
            helper.invokeMethodAsync("EventListener", eventName, {
                toolName: obj.toolName,
                active: obj.active
            });
            break;
        case "viewerResize":
            helper.invokeMethodAsync("EventListener", eventName, {
                width: obj.width,
                height: obj.height
            });
            break;
        case "viewerStateRestored" :
            helper.invokeMethodAsync("EventListener", eventName, {
                value: obj.value
            });
            break;
        default:
            helper.invokeMethodAsync("EventListener", eventName, null);
    }
}

export function GetProperty(view, propertyName) {
    return view[propertyName];
}

export function GuiViewer3dInitializer(id) {
    return new Autodesk.Viewing.GuiViewer3D(document.getElementById(id));
}

export async function loadDocumentNode(viewer, document, manifestNode) {
    return await viewer.loadDocumentNode(document, manifestNode);
}

export function loadDocument(urn) {
    return new Promise((resolve, reject) => {
        Autodesk.Viewing.Document.load(urn, (doc) => {
            resolve(doc)
        }, (error, message) => {
            reject(message)
        })
    })
}
