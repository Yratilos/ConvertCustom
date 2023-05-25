// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Tool = {
    /**
     * 导出Excel
     * @param {string} methd 请求类型
     * @param {string} url 请求地址
     * @param {string} fileName 文件名
     */
    exportExcel: function (methd, url, fileName) {
        var oReq = new XMLHttpRequest()
        oReq.responseType = "blob";
        oReq.open(methd, url)
        oReq.send();
        // 请求已完成，且响应已就绪
        oReq.onload = function (oEvent) {
            if (oReq.status >= 200 && oReq.status < 300) {
                var blob = oReq.response;
                var objectUrl = URL.createObjectURL(blob);
                if (window.navigator.msSaveBlob) {
                    window.navigator.msSaveBlob(blob, fileName)
                } else {
                    var a = document.createElement('a');
                    document.body.appendChild(a);
                    a.style = 'display:none'
                    a.href = objectUrl
                    a.download = fileName
                    a.click();
                    URL.revokeObjectURL(objectUrl);
                }
            }
        };
    }
}