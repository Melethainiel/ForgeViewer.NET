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
        case "viewerInitialized" :
            helper.invokeMethodAsync("EventListener", eventName, null);
            break;
        case "viewerResize":
            helper.invokeMethodAsync("EventListener", eventName, [obj.width, obj.height]);
            break;
        case "geometryLoaded":
            helper.invokeMethodAsync("EventListener", eventName, null);
            break;
        default:
            break;
    }
}


export function GetProperty(view, propertyName) {
    let prop = view[propertyName];
    return prop;
}

export function GuiViewer3dInitializer(id) {
    return new Autodesk.Viewing.GuiViewer3D(document.getElementById(id));
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