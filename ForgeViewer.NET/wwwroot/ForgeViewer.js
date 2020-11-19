import 'https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.js';

let helper = null;

export async function ViewingInitializer(options, initializerHelper) {
    helper = initializerHelper;
    let id;
    if (options.getAccessToken !== "") {
        id = options.getAccessToken;
        options.getAccessToken = function () {
            return GetAccessToken(id)
        }
    }
    Autodesk.Viewing.Initializer(options, helper.invokeMethodAsync("InitializerCallback"));
}

function GetAccessToken(name) {
    return helper.invokeMethodAsync("Callback",name)
}