import 'https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.js';

export function GetViewer(options){
    Autodesk.Viewing.Initializer(options, initializerHelper.invokeMethodAsync("InitializerCallback"));
}