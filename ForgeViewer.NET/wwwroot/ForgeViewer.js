export function ViewingInitializer(options, helper) {
    let id;
    if (options.getAccessToken) {
        options.getAccessToken = function () {
            return helper.invokeMethodAsync("GetAccessToken")
        }
    }
    Autodesk.Viewing.Initializer(options, helper.invokeMethodAsync("InitializerCallback"));
}

export function Viewer3dInitializer(id) {
    return new Autodesk.Viewing.Viewer3D(document.getElementById(id));

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